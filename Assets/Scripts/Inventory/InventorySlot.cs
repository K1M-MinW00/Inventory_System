using System;
using UnityEngine;

[System.Serializable]
public class InventorySlot : ItemSlot
{
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
}