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
        // Espera a que el GameManager esté listo
        while (GameManager.Instance == null)
            yield return null;

        if (itemData == null || itemData.dialogue == null || itemData.dialogue.Count == 0)
        {
            Debug.LogWarning($"[AutoItemDialogue] No hay diálogo asignado en {gameObject.name}");
            yield break;
        }

        string uniqueKey = "AutoDialogue_" + gameObject.name;

        // Verificar si ya se activó este diálogo
        if (persistTrigger && PlayerPrefs.GetInt(uniqueKey, 0) == 1)
            yield break;

        if (triggerOncePerSession && triggeredSessionKeys.Contains(uniqueKey))
            yield break;

        // Iniciar diálogo
        GameManager.Instance.StartDialogue(itemData.dialogue);
        Debug.Log($"[AutoItemDialogue] Diálogo activado en {gameObject.name}");

        // Marcar como ya mostrado
        if (persistTrigger)
            PlayerPrefs.SetInt(uniqueKey, 1);

        if (triggerOncePerSession)
            triggeredSessionKeys.Add(uniqueKey);
    }
}
