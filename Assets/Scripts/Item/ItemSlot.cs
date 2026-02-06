using System;
using UnityEngine;

public abstract class ItemSlot : ISerializationCallbackReceiver
{
    [NonSerialized] protected ItemData itemData; // Reference to the data
    [SerializeField] protected int itemID = -1;
    [SerializeField] protected int stackSize;     // Current stack size - how many of the data do we have ?

    public ItemData ItemData => itemData;
    public int StackSize => stackSize;

    public void ClearSlot() // Clears the slot.
    {
        itemData = null;
        itemID = -1;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot) // Assigns an item to the slot
    {
        if (itemData == invSlot.itemData) // Does the slot contain the same item ? => Add to stack if so
        {
            AddToStack(invSlot.StackSize);
        }

        else // Override slot with the inventory slot that we're passing in
        {
            itemData = invSlot.itemData;
            itemID = itemData.ID;
            stackSize = 0;
            AddToStack(invSlot.StackSize);
        }
    }

    public void AssignItem(ItemData data, int amount)
    {
        if (itemData == data)
        {
            AddToStack(amount);
        }
        else
        {
            itemData = data;
            itemID = data.ID;
            stackSize = 0;
            AddToStack(amount);
        }
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;

        if (stackSize <= 0)
            ClearSlot();
    }



    public void OnAfterDeserialize()
    {
        if (itemID == -1)
            return;

        var db = Resources.Load<Database>("Database");

        itemData = db.GetItem(itemID);
    }

    public void OnBeforeSerialize() { }
}
