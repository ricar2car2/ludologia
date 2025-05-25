using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoItemDialogue : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Mostrar este diálogo solo una vez durante la ejecución del juego.")]
    public bool triggerOncePerSession = true;

    [Tooltip("Recordar este diálogo incluso entre sesiones del juego.")]
    public bool persistTrigger = false;

    [Tooltip("ItemData que contiene el diálogo a mostrar.")]
    public ItemData itemData;

    // Cache de los diálogos que ya fueron activados en esta sesión
    private static HashSet<string> triggeredSessionKeys = new HashSet<string>();

    private void OnEnable()
    {
        StartCoroutine(TriggerDialogueWhenReady());
    }

    private IEnumerator TriggerDialogueWhenReady()
    {
        // Esperar a que GameManager esté disponible
        while (GameManager.Instance == null)
            yield return null;

        if (itemData == null || string.IsNullOrEmpty(itemData.allDialoguesInPhases))
        {
            Debug.LogWarning($"[AutoItemDialogue] No hay diálogo válido en '{gameObject.name}'.");
            yield break;
        }

        string uniqueKey = "AutoDialogue_" + gameObject.name;

        // Ya fue activado (persistente)
        if (persistTrigger && PlayerPrefs.GetInt(uniqueKey, 0) == 1)
            yield break;

        // Ya fue activado (en esta sesión)
        if (triggerOncePerSession && triggeredSessionKeys.Contains(uniqueKey))
            yield break;

        // Separar en fases
        string[] dialogueSections = itemData.allDialoguesInPhases.Split(new string[] { "---" }, System.StringSplitOptions.RemoveEmptyEntries);
        int phaseIndex = Mathf.Clamp(itemData.currentDialoguePhase, 0, dialogueSections.Length - 1);
        List<string> lines = new List<string>(dialogueSections[phaseIndex].Trim().Split('\n'));

        // Iniciar diálogo
        GameManager.Instance.StartDialogue(lines, () =>
        {
            itemData.currentDialoguePhase++;

            if (itemData.changeSceneAfterDialogue && itemData.targetSceneIndex >= 0)
                GameManager.Instance.StartCoroutine(GameManager.Instance.ChangeScene(itemData.targetSceneIndex, 0.3f));

            foreach (GameObject obj in itemData.objectsToSetActive)
                if (obj != null) obj.SetActive(true);

            foreach (GameObject obj in itemData.objectsToRemove)
                if (obj != null) obj.SetActive(false);

            foreach (GameObject obj in itemData.objectsToInactivate)
                if (obj != null) obj.SetActive(false);
        });

        Debug.Log($"[AutoItemDialogue] Diálogo activado en '{gameObject.name}'");

        // Guardar estado
        if (persistTrigger)
            PlayerPrefs.SetInt(uniqueKey, 1);

        if (triggerOncePerSession)
            triggeredSessionKeys.Add(uniqueKey);
    }
}
