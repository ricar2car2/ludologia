using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryHoverDetector : MonoBehaviour
{
    public GraphicRaycaster raycaster;
    public GameManager gameManager;

    PointerEventData pointerEventData;
    EventSystem eventSystem;

    void Awake()
    {
        eventSystem = EventSystem.current;
    }

    void Update()
    {
        pointerEventData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            // Si el GameObject del resultado tiene un ItemData, mostrar el nameTag
            ItemData item = result.gameObject.GetComponent<ItemData>();
            if (item != null)
            {
                gameManager.UpdateNameTag(item);
                return;
            }
        }

        // Si no encontró ningún item
        gameManager.UpdateNameTag(null);
    }
}
