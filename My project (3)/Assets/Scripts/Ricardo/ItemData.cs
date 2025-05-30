using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemData : MonoBehaviour
{
    [Header("Setup")]
    public int itemID, requiredItemID; // ID del item y ID requerido para recogerlo

    [Header("Exito")]

    // Objetos que se deben desactivar o activar si el item es recogido exitosamente
    public GameObject[] objectsToRemove = new GameObject[0];
    public GameObject[] objectsToSetActive = new GameObject[0];

    [Header("Inactivar objetos después del diálogo (solo ocultar visualmente)")]
    public GameObject[] objectsToInactivate = new GameObject[0];

    [Header("Cambio de escena después del diálogo")]
    public bool changeSceneAfterDialogue = false;
    public int targetSceneIndex = -1;  // Usa -1 para indicar que no se quiere cambiar escena





    // Sprite que representa este item en el equipo
    public Sprite itemSlotSprite;

    [Header("Tipo de objeto")]
    public bool isNPC = false;


    // Nombre que aparece cuando el item aun no ha sido recogido
    public string objectName;

    // Tamano del nombre que aparece antes de ser recogido
    public Vector2 nameTagSize = new Vector2(3, 0.65f);

    // Nombre del item cuando ya fue recogido
    public string itemName;

    // Tamano del nombre una vez recogido
    public Vector2 ItemNameTagSize = new Vector2(3, 0.65f);

    [Header("Failure")]

    // Mensaje que se muestra si no se puede recoger el item
    [TextArea(3, 3)]
    public string hintMessage;

   [Header("Diálogo")]
    [TextArea(4, 10)]
    public string allDialoguesInPhases;

    [HideInInspector]
    public int currentDialoguePhase = 0;



    // Tamano del cuadro de texto del hint
    public Vector2 hintBoxSize = new Vector2(4, 0.65f);


}