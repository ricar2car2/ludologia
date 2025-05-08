using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemData : MonoBehaviour
{
    [Header("Setup")]
    public int itemID, requiredItemID;

    [Header("Exito")]

    public GameObject[] objectsToRemove;
    public GameObject[] objectsToSetActive;

    public Sprite itemSlotSprite;
    public string objectName;
    public Vector2 nameTagSize= new Vector2(3,0.65f);
    public string itemName;
    public Vector2 ItemNameTagSize= new Vector2(3,0.65f);

    [Header("Failure")]

    [TextArea(3,3)]
    public string hintMessage;
        
    public Vector2 hintBoxSize= new Vector2(4,0.65f);
}
