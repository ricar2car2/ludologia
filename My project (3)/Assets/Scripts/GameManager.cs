using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static List<int> collectedItems = new List<int>();
    public RectTransform nameTag, hintBox;
    public ItemData currentItemShown;

    private void Awake()
    {
        // Asegura que solo exista una instancia
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

   public void UpdateNameTag(ItemData item)
    {
        if (currentItemShown == item && nameTag.gameObject.activeSelf)
            return; // Evita recalcular si ya está activo con el mismo item

        currentItemShown = item;
        nameTag.gameObject.SetActive(true);

        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = item.objectName;

        nameTag.sizeDelta = item.namTagSize; // Esto solo si el tamaño es fijo por item
        nameTag.localPosition = new Vector2(item.namTagSize.x / 2, -0.4f); // Este offset puede causar salto si es relativo al tamaño
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
        hintBox.parent.localPosition = new Vector2(0, 0);
    }
}


