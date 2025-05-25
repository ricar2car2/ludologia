using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel.Design;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class clickManager : MonoBehaviour
{
    GameManager gameManager;
    SceneManager scene;
    


    private void Start()
    {
        // Busca una instancia del GameManager en la escena
        gameManager = FindFirstObjectByType<GameManager>();

    }

    

    private void Update()
    {
        if (gameManager.isDialogueActive)
            return;
        // Detecta clic izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Si no hay ningún objeto debajo del mouse, resetea el estado
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider == null)
            {
                gameManager.currentItemShown = null; // Quita referencia al item mostrado
                gameManager.hintBox.gameObject.SetActive(false); // Oculta el hint
            }
        }
    }

    

    public void ClickReaction(ItemData item)
{
    // Verifica si el item puede recogerse (si no requiere otro objeto, o si se tiene el requerido)
    bool canGetItem = item.requiredItemID == -1 || GameManager.PlayerHasItem(item.requiredItemID);

    if (item.isNPC && !string.IsNullOrEmpty(item.allDialoguesInPhases))
{
    // Separar fases por "---" o línea vacía
    string[] dialoguePhases = item.allDialoguesInPhases.Split(new string[] { "---" }, System.StringSplitOptions.RemoveEmptyEntries);
int phaseIndex = Mathf.Clamp(item.currentDialoguePhase, 0, dialoguePhases.Length - 1);
string currentPhaseText = dialoguePhases[phaseIndex].Trim();

// Parsear bloques según cambio de personaje
List<string> blocks = ParseDialogueBlocks(currentPhaseText);

// Mostrar bloques tal como están escritos
GameManager.Instance.StartDialogue(blocks, () =>
{
    item.currentDialoguePhase++;

    // Acciones después del diálogo si quieres...



        // Activar, desactivar o cambiar escena si aplica
        foreach (GameObject obj in item.objectsToSetActive)
            if (obj != null) obj.SetActive(true);

        foreach (GameObject obj in item.objectsToRemove)
            if (obj != null) obj.SetActive(false);

        foreach (GameObject obj in item.objectsToInactivate)
            if (obj != null) obj.SetActive(false);

        if (item.changeSceneAfterDialogue && item.targetSceneIndex >= 0)
        {
            gameManager.StartCoroutine(gameManager.ChangeScene(item.targetSceneIndex, 0.3f));
        }
    });

    return;
}

List<string> ParseDialogueBlocks(string rawText)
{
    List<string> blocks = new List<string>();

    string[] lines = rawText.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
    if (lines.Length == 0) return blocks;

    string currentSpeaker = null;
    string currentBlock = "";

    foreach (string line in lines)
    {
        string trimmed = line.Trim();
        if (string.IsNullOrEmpty(trimmed)) continue;

        // Detecta cambio de hablante si hay ":"
        int colonIndex = trimmed.IndexOf(':');
        if (colonIndex > 0)
        {
            string speaker = trimmed.Substring(0, colonIndex).Trim();

            if (speaker != currentSpeaker)
            {
                // Si cambia el hablante, guarda bloque anterior
                if (!string.IsNullOrEmpty(currentBlock))
                {
                    blocks.Add(currentBlock.Trim());
                    currentBlock = "";
                }

                currentSpeaker = speaker;
            }
        }

        currentBlock += trimmed + "\n";
    }

    // Agrega el último bloque
    if (!string.IsNullOrEmpty(currentBlock))
        blocks.Add(currentBlock.Trim());

    return blocks;
}

        if (canGetItem)
        {
            // Oculta el nameTag antes de recoger el objeto
            gameManager.nameTag.gameObject.SetActive(false);
            gameManager.currentItemShown = null;

            StartCoroutine(HandleItemClick(item));
        }
        else
        {
            // Si no se puede recoger, muestra hint explicativo y nombre
            gameManager.UpdateHintBox(item);
            gameManager.UpdateNameTag(item);
            gameManager.currentItemShown = item;
        }

    // Verifica condiciones especiales (como cambio de escena)
    gameManager.CheckSpecialConditions(item, canGetItem);
}


    private IEnumerator HandleItemClick(ItemData item)
    {
        // Añade el ítem a la lista de recogidos
        GameManager.collectedItems.Add(item);

        // Espera un pequeño delay antes de actualizar la escena
        yield return new WaitForSeconds(0.03f);

        // Llama la rutina para actualizar la escena
        yield return StartCoroutine(UpdateSceneAfterAction(item));
    }

    private IEnumerator UpdateSceneAfterAction(ItemData item)
    {
        // Oculta el nombre si aún está mostrándose ese objeto
        if (gameManager.currentItemShown == item)
        {
            gameManager.nameTag.gameObject.SetActive(false);
        }

        // Oculta los objetos relacionados al item recogido
        foreach (GameObject g in item.objectsToRemove)
        {
            g.SetActive(false);
        }

        // Actualiza el canvas de equipo
        gameManager.UpdateEquipmentCanvas();
        yield return null;
    }
    
    
}
