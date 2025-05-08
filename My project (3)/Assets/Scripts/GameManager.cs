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
    int activeLocalScene=0;

    [Header("Equipment")]
    public GameObject equipmentCanvas;
    public Image[] equipmentSlots, equipmentImages;
    public Sprite EmptyItemSlotSprite;
    public Color selectedItemColor;
    int SelectedCanvasSlotID =0, SelectedItemID;



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

    public void SelectItem (int equipmentCanvasID)
    {
        Color c= Color.white;
        c.a=0;
        equipmentSlots[SelectedCanvasSlotID].GetComponent<Image>().color=c;

        if(equipmentCanvasID>= collectedItems.Count)
        {
            SelectedItemID = -1;
            SelectedCanvasSlotID = 0;
            return;
        }

        equipmentSlots[equipmentCanvasID].GetComponent<Image>().color= selectedItemColor;

        SelectedCanvasSlotID = equipmentCanvasID;
        SelectedItemID= collectedItems[SelectedCanvasSlotID].itemID;
    }

    public void ShowItemName (int equipmentCanvasID)
    {
        
    }


   public void UpdateNameTag(ItemData item)
    {
        if (currentItemShown == item && nameTag.gameObject.activeSelf)
            return; // Evita recalcular si ya está activo con el mismo item

        if(item==null)
        {
            nameTag.parent.gameObject.SetActive(false);
            return;
        }

        currentItemShown = item;
        nameTag.gameObject.SetActive(true);

        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = item.objectName;

        nameTag.sizeDelta = item.namTagSize; // Esto solo si el tamaño es fijo por item
        nameTag.localPosition = new Vector2(item.namTagSize.x / 2, -0.6f); // Este offset puede causar salto si es relativo al tamaño
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

    public void CheckSpecialConditions(ItemData item, bool canGetItem)
    {
        switch(item.itemID){
            case -12:
            //de escena 1 a escena 2

            StartCoroutine(ChangeScene(1,0));
                break;
            case -21:
            //de escena 2 a escena 1
            StartCoroutine(ChangeScene(0,0));
                break;  
            case -1:
            if(canGetItem)
            {
            StartCoroutine(ChangeScene(2,1));
            }
            //victoria magistral. Final del juego ganaste
                break;
        }
    }

    public IEnumerator ChangeScene (int sceneNumber, float delay){

        yield return new WaitForSeconds(delay);
        Color c=blockingImage.color;
        blockingImage.enabled = true;
        while(blockingImage.color.a<1)
        {
            c.a+=Time.deltaTime;
            blockingImage.color =c;
            
        }
        Debug.Log("test");

        localScenes[activeLocalScene].SetActive(false);

        localScenes[sceneNumber].SetActive(true);

        activeLocalScene= sceneNumber;

        UpdateHintBox(null);
        UpdateNameTag(null);

        while(blockingImage.color.a<0)
        {
            c.a-=Time.deltaTime;
            blockingImage.color =c;            
        }
                
        blockingImage.enabled = false;

        yield return null;
    }
}


