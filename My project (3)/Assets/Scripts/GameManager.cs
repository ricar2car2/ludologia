using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static List<int> collectedItems = new List<int>();
    public RectTransform nameTag, hintBox;
    public ItemData currentItemShown;

    public Image blockingImage;
    public GameObject[] localScenes;

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

    public void CheckSpecialConditions(ItemData item)
    {
        switch(item.itemID){
            case -12:
            //de escena 1 a escena 2

            StartCoroutine(ChangeScene(1,0));
                break;
            case -21:
            //de escena 2 a escena 1
            StartCoroutine(ChangeScene(2,0));
                break;  
            case -1:
            //victoria magistral. Final del juego ganaste
            StartCoroutine(ChangeScene(3,1));

                break;
        }
    }

    public IEnumerator ChangeScene (int sceneNumber, float Delay){
        Color c=blockingImage.color;
        while(blockingImage.color.a<1)
        {
            c.a+=Time.deltaTime;
            blockingImage.color =c;
            
        }
        Debug.Log("test");

        while(blockingImage.color.a<0)
        {
            c.a-=Time.deltaTime;
            blockingImage.color =c;            
        }
        yield return null;
    }
}


