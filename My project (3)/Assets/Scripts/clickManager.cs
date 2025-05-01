using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class clickManager : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Si no hay objeto debajo del mouse, resetea el estado
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider == null)
            {
                gameManager.currentItemShown = null;
                gameManager.hintBox.gameObject.SetActive(false);
            }
        }
    }

    public void ClickReaction(ItemData item)
    {
        bool canGetItem = item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID);
        if (canGetItem)
        {
            StartCoroutine(HandleItemClick(item));
        }
        else
        {
            gameManager.UpdateHintBox(item);
        }
    }

    private IEnumerator HandleItemClick(ItemData item)
    {
        GameManager.collectedItems.Add(item.itemID);

        // Espera un pequeño tiempo antes de actualizar la escena
        yield return new WaitForSeconds(0.03f);

        yield return StartCoroutine(UpdateSceneAfterAction(item));
    }

    private IEnumerator UpdateSceneAfterAction(ItemData item)
    {
        // Oculta la nametag si corresponde
        if (gameManager.currentItemShown == item)
        {
            gameManager.nameTag.gameObject.SetActive(false);
        }

        foreach (GameObject g in item.objectsToRemove)
        {
            Destroy(g);
        }

        Debug.Log("Coleccionado");
        yield return null;
    }
}
