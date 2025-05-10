using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemData : MonoBehaviour
{
    [Header("Setup")]
    public int itemID, requiredItemID; // ID del �tem y ID requerido para recogerlo

    [Header("Exito")]

    // Objetos que se deben desactivar o activar si el �tem es recogido exitosamente
    public GameObject[] objectsToRemove;
    public GameObject[] objectsToSetActive;

    // Sprite que representa este �tem en el equipo
    public Sprite itemSlotSprite;

    // Nombre que aparece cuando el �tem a�n no ha sido recogido
    public string objectName;

    // Tama�o del nombre que aparece antes de ser recogido
    public Vector2 nameTagSize = new Vector2(3, 0.65f);

    // Nombre del �tem cuando ya fue recogido
    public string itemName;

    // Tama�o del nombre una vez recogido
    public Vector2 ItemNameTagSize = new Vector2(3, 0.65f);

    [Header("Failure")]

    // Mensaje que se muestra si no se puede recoger el �tem
    [TextArea(3, 3)]
    public string hintMessage;

    // Tama�o del cuadro de texto del hint
    public Vector2 hintBoxSize = new Vector2(4, 0.65f);
}
