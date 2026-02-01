using UnityEngine;

/// <summary>
/// This is a scriptable object, that defines what an item is in our game.
/// It could be inherited from to have branched version of items, for example potions and equipment.
/// </summary>

[CreateAssetMenu(menuName = "Inventory System/Item")]
public class ItemData : ScriptableObject
{
    public int id;
    public string displayName;

    public Sprite icon;
    public int maxStackSize;

    [TextArea(4, 4)]
    public string description;

}
