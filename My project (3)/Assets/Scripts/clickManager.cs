using System.Collections;
using UnityEngine;

public class clickManager : MonoBehaviour
{
   public void ClickReaction(ItemData item)
{
    if (item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID))
    {
        StartCoroutine(HandleItemClick(item));
    }
}

private IEnumerator HandleItemClick(ItemData item)
{
    GameManager.collectedItems.Add(item.itemID);

    // Espera 1 segundo antes de continuar
    yield return new WaitForSeconds(0.03f);

    yield return StartCoroutine(UpdateSceneAfterAction(item));
}

private IEnumerator UpdateSceneAfterAction(ItemData item)
{
    foreach (GameObject g in item.objectsToRemove)
        Destroy(g);

    Debug.Log("Coleccionado");

    yield return null;
}

}
