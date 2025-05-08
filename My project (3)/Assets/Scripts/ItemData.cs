using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemData : MonoBehaviour
{
    [Header("Setup")]
    public int itemID, requiredItemID;

    [Header("Exito")]

    public GameObject[] objectsToRemove;
    public string objectName;
    public Vector2 namTagSize= new Vector2(3,0.65f);

    [Header("Failure")]

    [TextArea(3,3)]
    public string hintMessage;
        
    public Vector2 hintBoxSize= new Vector2(4,0.65f);
}
