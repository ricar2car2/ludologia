using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemData : MonoBehaviour
{
    [Header("Setup")]
    public int itemID, requiredItemID; // ID del ítem y ID requerido para recogerlo

    [Header("Exito")]

    // Objetos que se deben desactivar o activar si el ítem es recogido exitosamente
    public GameObject[] objectsToRemove;
    public GameObject[] objectsToSetActive;

    // Sprite que representa este ítem en el equipo
    public Sprite itemSlotSprite;

    // Nombre que aparece cuando el ítem aún no ha sido recogido
    public string objectName;

    // Tamaño del nombre que aparece antes de ser recogido
    public Vector2 nameTagSize = new Vector2(3, 0.65f);

    // Nombre del ítem cuando ya fue recogido
    public string itemName;

    // Tamaño del nombre una vez recogido
    public Vector2 ItemNameTagSize = new Vector2(3, 0.65f);

    [Header("Failure")]

    // Mensaje que se muestra si no se puede recoger el ítem
    [TextArea(3, 3)]
    public string hintMessage;

    // Tamaño del cuadro de texto del hint
    public Vector2 hintBoxSize = new Vector2(4, 0.65f);
}
