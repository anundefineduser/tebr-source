using UnityEngine;

[System.Serializable]
public struct ItemInfo
{
    public string name;

    public UnityEngine.Object itemScript;

    public Texture2D inventoryAsset;
    public Texture2D pickupAsset;
}

public class ItemBase : MonoBehaviour
{
    public virtual void Pickup() { }
    public virtual void Use()
    {
        ItemManager.current.ClearItem();
    }
}
