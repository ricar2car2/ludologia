using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemData : MonoBehaviour
{
    public int itemID, requiredItemID;
    public GameObject[] objectsToRemove;
    public string objectName;
    public Vector2 namTagSize= new Vector2(3,0.65f);
}
