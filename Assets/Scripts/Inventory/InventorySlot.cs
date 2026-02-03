using System;
using UnityEngine;

[System.Serializable]
public class InventorySlot : ISerializationCallbackReceiver
{
    [NonSerialized] private ItemData itemData; // Reference to the data
    [SerializeField] private int itemID = -1;
    [SerializeField] private int stackSize;     // Current stack size - how many of the data do we have ?

    public ItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(ItemData itemData, int amount) // Constructor to make a occupied inventory slot
    {
        this.itemData = itemData;
        this.itemID = itemData.ID;
        this.stackSize = amount;
    }

    public InventorySlot() // Constructor to make an empty inventory slot
    {
        ClearSlot();
    }

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
    public void UpdateInventorySlot(ItemData data, int amount) // Update slot directly
    {
        itemData = data;
        itemID = itemData.ID;
        stackSize = amount;
    }

    public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining) // Would there be enough room in the stack for the amount we're trying to add 
    {
        amountRemaining = ItemData.MaxStackSize - stackSize;

        return EnoughRoomLeftInStack(amountToAdd);
    }


    public bool EnoughRoomLeftInStack(int amountToAdd) // 
    {
        if (itemData == null || (itemData != null && stackSize + amountToAdd <= itemData.MaxStackSize))
            return true;

        else
            return false;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if (stackSize <= 1) // Is there enough to actually split ? If not return false
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2); // Get half the stack
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(itemData, halfStack); // Creates a copy of this slot with half the stack size
        return true;
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        if (itemID == -1)
            return;

        var db = Resources.Load<Database>("Database");

        itemData = db.GetItem(itemID);
    }
}