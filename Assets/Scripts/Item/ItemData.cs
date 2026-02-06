using UnityEngine;

/// <summary>
/// This is a scriptable object, that defines what an item is in our game.
/// It could be inherited from to have branched version of items, for example potions and equipment.
/// </summary>

[CreateAssetMenu(menuName = "Inventory System/Item")]
public class ItemData : ScriptableObject
{
    public int ID = -1;
    public string DisplayName;

    public Sprite Icon;
    public int MaxStackSize;

    public int GoldValue;
    public GameObject ItemPrefab;

    [TextArea(4, 4)]
    public string Description;

    public void UseItem()
    {
        Debug.Log($"Using {DisplayName}.");
    }
}
