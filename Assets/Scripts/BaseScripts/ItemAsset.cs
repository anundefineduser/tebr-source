using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Baldi/Items/New Item", order = 1)]
public class ItemAsset : ScriptableObject
{
    [NaughtyAttributes.Label("Item Info")] public ItemInfo info;
}