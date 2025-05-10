using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static List<ItemData> collectedItems = new List<ItemData>();

    [Header("Setup")]
    public RectTransform nameTag, hintBox;
    public ItemData currentItemShown;

    [Header("Local Scenes")]
    public Image blockingImage;
    public GameObject[] localScenes;
    int activeLocalScene = 0;

    [Header("Equipment")]
    public GameObject equipmentCanvas;
    public Image[] equipmentSlots, equipmentImages;
    public Sprite EmptyItemSlotSprite;
    public Color selectedItemColor;
    public int SelectedCanvasSlotID = 0, SelectedItemID;

    private void Awake()
    {
        // Asegura que solo haya una instancia del GameManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SelectItem(int equipmentCanvasID)
    {
        // Quita color de selección anterior
        Color c = Color.white;
        c.a = 0;
        equipmentSlots[SelectedCanvasSlotID].GetComponent<Image>().color = c;

        // Si el índice es inválido, limpia selección
        if (equipmentCanvasID >= collectedItems.Count || equipmentCanvasID < 0)
        {
            SelectedItemID = -1;
            SelectedCanvasSlotID = 0;
            return;
        }

        // Aplica color de selección al nuevo slot
        equipmentSlots[equipmentCanvasID].GetComponent<Image>().color = selectedItemColor;

        SelectedCanvasSlotID = equipmentCanvasID;
        SelectedItemID = collectedItems[SelectedCanvasSlotID].itemID;
    }

    public void ShowItemName(int equipmentCanvasID)
    {
        if (equipmentCanvasID < collectedItems.Count)
            UpdateNameTag(collectedItems[equipmentCanvasID]);
    }

    public void UpdateEquipmentCanvas()
    {
        int itemsAmount = collectedItems.Count, itemSlotsAmount = equipmentSlots.Length;

        // Actualiza los sprites de los slots de equipo
        for (int i = 0; i < itemSlotsAmount; i++)
        {
            if (i < itemsAmount && collectedItems[i].itemSlotSprite != null)
                equipmentImages[i].sprite = collectedItems[i].itemSlotSprite;
            else
                equipmentImages[i].sprite = EmptyItemSlotSprite;
        }

        // Ajusta selección inicial si hay solo un objeto
        if (itemsAmount == 0)
            SelectItem(-1);
        else if (itemsAmount == 1)
            SelectItem(0);
    }

    public void UpdateNameTag(ItemData item)
    {
        Debug.Log($"UpdateNameTag llamado con: {(item != null ? item.name : "null")}");

        // Evita actualizar si ya está mostrando lo mismo
        if (currentItemShown == item && nameTag.parent.gameObject.activeSelf)
            return;

        if (item == null)
        {
            nameTag.parent.gameObject.SetActive(false);
            return;
        }

        nameTag.parent.gameObject.SetActive(true);

        string nameText = item.objectName;
        Vector2 size = item.nameTagSize;

        // Cambia a nombre recogido si ya está en la mochila
        if (collectedItems.Contains(item))
        {
            nameText = item.itemName;
            size = item.ItemNameTagSize;
        }

        currentItemShown = item;
        nameTag.gameObject.SetActive(true);

        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = nameText;

        nameTag.sizeDelta = size;
        nameTag.localPosition = new Vector2(size.x / 2, -0.6f); // Posición ajustada con offset
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
        hintBox.parent.localPosition = new Vector2(0, 0); // Posición por defecto
    }

    public void CheckSpecialConditions(ItemData item, bool canGetItem)
    {
        switch (item.itemID)
        {
            case -12:
                // Cambia de escena 1 a escena 2
                StartCoroutine(ChangeScene(1, 0));
                break;
            case -21:
                // Cambia de escena 2 a escena 1
                StartCoroutine(ChangeScene(0, 0));
                break;
            case -1:
                // Gana el juego si se puede recoger
                if (canGetItem)
                {
                    StartCoroutine(ChangeScene(2, 1));
                }
                break;
        }
    }

    public IEnumerator ChangeScene(int sceneNumber, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Fade in: oscurece la pantalla
        Color c = blockingImage.color;
        blockingImage.enabled = true;
        while (blockingImage.color.a < 1)
        {
            c.a += Time.deltaTime;
            blockingImage.color = c;
        }

        Debug.Log("test");

        // Desactiva escena actual y activa la nueva
        localScenes[activeLocalScene].SetActive(false);
        localScenes[sceneNumber].SetActive(true);
        activeLocalScene = sceneNumber;

        // Oculta elementos UI
        UpdateHintBox(null);
        UpdateNameTag(null);

        // Reactiva canvas de equipo
        equipmentCanvas.SetActive(true);

        // Fade out: vuelve a mostrar la escena
        while (blockingImage.color.a > 0)
        {
            c.a -= Time.deltaTime;
            blockingImage.color = c;
        }

        blockingImage.enabled = false;
        yield return null;
    }
}
