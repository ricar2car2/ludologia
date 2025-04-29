using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
   public static List<int> collectedItems = new List <int>();
   public RectTransform nameTag;


   public void UpdateNameTag(ItemData item)
{
    nameTag.GetComponentInChildren<TextMeshProUGUI>().text =item.objectName;

    nameTag.sizeDelta = item.namTagSize;

    nameTag.localPosition= new Vector2(item.namTagSize.x/2,-0.5f);
}
}

