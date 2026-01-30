using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Item")]
public class ItemData : ScriptableObject
{
    public int id;
    public string displayName;

    public Sprite icon;
    public int maxStackSize;

    [TextArea(4,4)]
    public string description;
    
}
