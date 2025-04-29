using UnityEngine;

public class clickManager : MonoBehaviour
{
    public void ClickReaction(ItemData item)
    {
        if(item.requiredItemID==-1)
            Debug.Log("test");
    }
}
