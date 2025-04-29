using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class clickManager : MonoBehaviour
{

    
    public void ClickReaction(ItemData item)
    {
        bool canGetItem = item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID);
        if (canGetItem)
        {
            StartCoroutine(HandleItemClick(item));
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
    if (GameManager.Instance.currentItemShown == item)
    {
        GameManager.Instance.nameTag.gameObject.SetActive(false);
    }

    foreach (GameObject g in item.objectsToRemove)
    {
        Destroy(g);
    }

    Debug.Log("Coleccionado");
    yield return null;
}
}

