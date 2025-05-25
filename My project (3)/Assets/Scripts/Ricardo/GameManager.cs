using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static List<ItemData> collectedItems = new List<ItemData>();

    [Header("Setup")]
    public RectTransform nameTag, hintBox;
    public ItemData currentItemShown;

    [Header("Local Scenes")]
    public Image blockingImage;
    public GameObject[] localScenes;
    int activeLocalScene = 0;

    private bool isChangingScene = false;



    [Header("Equipment")]
    public GameObject equipmentCanvas;
    public Image[] equipmentSlots, equipmentImages;
    public Sprite EmptyItemSlotSprite;
    public Color selectedItemColor;
    public int SelectedCanvasSlotID = 0, SelectedItemID;

    [Header("Dialogue UI")]
    public List<string> currentLines; // en lugar de DialogueData
    public DialogueData currentDialogue;
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    int currentLineIndex = 0;
    public bool isDialogueActive = false;


    [Header("Cambio de objetos permanentes")]
    public bool puzzleBotonesResuelto = false;

    public bool puzzleNumerosResuelto = false;






    private void Awake()
    {
        // Asegura que solo haya una instancia del GameManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SelectItem(int equipmentCanvasID)
    {
        // Quita color de selección anterior
        Color c = Color.white;
        c.a = 0;
        equipmentSlots[SelectedCanvasSlotID].GetComponent<Image>().color = c;

        // Si el índice es inválido, limpia selección
        if (equipmentCanvasID >= collectedItems.Count || equipmentCanvasID < 0)
        {
            SelectedItemID = -1;
            SelectedCanvasSlotID = 0;
            return;
        }

        // Aplica color de selección al nuevo slot
        equipmentSlots[equipmentCanvasID].GetComponent<Image>().color = selectedItemColor;

        SelectedCanvasSlotID = equipmentCanvasID;
        SelectedItemID = collectedItems[SelectedCanvasSlotID].itemID;
    }

    public void ShowItemName(int equipmentCanvasID)
    {
        if (equipmentCanvasID < collectedItems.Count)
            UpdateNameTag(collectedItems[equipmentCanvasID]);
    }

    public void UpdateEquipmentCanvas()
    {
        int itemsAmount = collectedItems.Count;

        for (int i = 0; i < equipmentImages.Length; i++)
        {
            if (i < itemsAmount && collectedItems[i].itemSlotSprite != null)
                equipmentImages[i].sprite = collectedItems[i].itemSlotSprite;
            else
                equipmentImages[i].sprite = EmptyItemSlotSprite;
        }
    }


    public void UpdateNameTag(ItemData item)
    {
        Debug.Log($"UpdateNameTag llamado con: {(item != null ? item.name : "null")}");

        // Evita actualizar si ya está mostrando lo mismo
        if (currentItemShown == item && nameTag.parent.gameObject.activeSelf)
            return;

        if (item == null)
        {
            nameTag.parent.gameObject.SetActive(false);
            return;
        }

        nameTag.parent.gameObject.SetActive(true);

        string nameText = item.objectName;
        Vector2 size = item.nameTagSize;

        // Cambia a nombre recogido si ya está en la mochila
        if (collectedItems.Contains(item))
        {
            nameText = item.itemName;
            size = item.ItemNameTagSize;
        }

        currentItemShown = item;
        nameTag.gameObject.SetActive(true);

        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = nameText;

        nameTag.sizeDelta = size;
        nameTag.localPosition = new Vector2(size.x / 2, -0.6f); // Posición ajustada con offset
    }

    public void UpdateHintBox(ItemData item)
    {
        if (item == null)
        {
            hintBox.gameObject.SetActive(false);
            return;
        }

        hintBox.gameObject.SetActive(true);
        hintBox.GetComponentInChildren<TextMeshProUGUI>().text = item.hintMessage;
        hintBox.sizeDelta = item.hintBoxSize;
        hintBox.parent.localPosition = new Vector2(0, 0); // Posición por defecto
    }

    public void CheckSpecialConditions(ItemData item, bool canGetItem)
    {
        switch (item.itemID)
        {
            case -11:
                // Cambia a escena 1
                StartCoroutine(ChangeScene(0, 0));
                break;
            case -22:
                // Cambia a escena 2
                StartCoroutine(ChangeScene(1, 0));
                break;

            case -33:
                // Cambia a escena 3
                StartCoroutine(ChangeScene(2, 0));
                break;
            case -44:
                // Cambia a escena 4
                StartCoroutine(ChangeScene(3, 0));
                break;

            case -55:
                // Cambia a escena 4
                StartCoroutine(ChangeScene(4, 0));
                break;

            case -66:
                // Cambia a escena 4
                StartCoroutine(ChangeScene(5, 0));
                break;
            case -77:
                // Cambia a escena 4
                StartCoroutine(ChangeScene(6, 0));
                break;
            case -88:
                // Cambia a escena 4
                StartCoroutine(ChangeScene(7, 0));
                break;
            case -99:
                // Cambia a escena 4
                StartCoroutine(ChangeScene(8, 0));
                break;
            case -1000:
                // Cambia a escena 4
                StartCoroutine(ChangeScene(9, 0));
                break;
            case -1100:
                // Cambia a escena 4
                StartCoroutine(ChangeScene(10, 0));
                break;
            case -1200:
                // Cambia a escena 4
                StartCoroutine(ChangeScene(11, 0));
                break;
            case -1300:
                // Cambia a escena 4
                StartCoroutine(ChangeScene(12, 0));
                break;
            case -1400:
                // Cambia a escena 4
                StartCoroutine(ChangeScene(13, 0));
                break;

            case -1:
                // Gana el juego si se puede recoger
                if (canGetItem)
                {
                    StartCoroutine(ChangeScene(2, 1));
                }
                break;
        }
    }

public IEnumerator ChangeScene(int sceneNumber, float delay)
{
    if (isChangingScene) yield break;
    isChangingScene = true;

    yield return new WaitForSeconds(delay);

    // Desactiva TODAS las escenas por seguridad
    for (int i = 0; i < localScenes.Length; i++)
        localScenes[i].SetActive(false);

    localScenes[sceneNumber].SetActive(true);
    activeLocalScene = sceneNumber;

    // Oculta elementos UI
    UpdateHintBox(null);
    UpdateNameTag(null);
    equipmentCanvas.SetActive(true);

    isChangingScene = false;
}



    public void StartDialogue(List<string> lines)
    {
        if (isDialogueActive) return; // evitar iniciar dos veces

        if (lines == null || lines.Count == 0)
        {
            Debug.LogWarning("Diálogo vacío o nulo.");
            return;
        }

        currentLines = lines;
        currentLineIndex = 0;
        isDialogueActive = true;
        dialogueBox.SetActive(true);
        ShowCurrentLine();
    }


    void ShowCurrentLine()
    {
        if (currentLines != null && currentLineIndex < currentLines.Count)
        {
            dialogueText.text = currentLines[currentLineIndex];
        }
    }

    void AdvanceDialogue()
    {
        currentLineIndex++;

        if (currentLines != null && currentLineIndex < currentLines.Count)
        {
            ShowCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBox.SetActive(false);
        currentLines = null;
    }


    private void Update()
    {

        if (isDialogueActive && Input.GetMouseButtonDown(0)) // clic izquierdo
        {
            AdvanceDialogue();
        }
    }

    public static bool PlayerHasItem(int itemID)
    {
        foreach (ItemData item in collectedItems)
        {
            if (item.itemID == itemID)
                return true;
        }
        return false;
    }


}
