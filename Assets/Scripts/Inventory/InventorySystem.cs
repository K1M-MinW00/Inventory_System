using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;
    [SerializeField] private int gold;

    public int Gold => gold;
    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;


    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size) // Constructor that sets the amount of slots
    {
        gold = 0;
        CreateInventory(size);
    }

    public InventorySystem(int _size, int _gold)
    {
        gold = _gold;
        CreateInventory(_size);
    }

    private void CreateInventory(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }


    public bool AddToInventory(ItemData itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) // Check whether item exists in inventory
        {
            foreach (var slot in invSlot)
            {
                if (slot.EnoughRoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }

        if (HasFreeSlot(out InventorySlot freeSlot)) // Get the first available slot
        {
            if (freeSlot.EnoughRoomLeftInStack(amountToAdd))
            {
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnInventorySlotChanged?.Invoke(freeSlot);
                return true;
            }
        }

        return false;
    }

    public bool ContainsItem(ItemData itemToAdd, out List<InventorySlot> invSlot) // Do any of out slots have the item to add in them ?
    {
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList(); // If they do, get a list of all of them

        return invSlot == null ? false : true; // If they do return true, else return false
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null); // Get the first free slot
        return freeSlot == null ? false : true;
    }

    public bool CheckInventoryRemaining(Dictionary<ItemData, int> shoppingCart)
    {
        var cloneSystem = new InventorySystem(InventorySize);

        for (int i = 0; i < InventorySize; i++)
        {
            cloneSystem.InventorySlots[i].AssignItem(this.inventorySlots[i]);
        }

        foreach (var kvp in shoppingCart)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                if (!cloneSystem.AddToInventory(kvp.Key, 1))
                    return false;
            }
        }
        return true;
    }

    public void SpendGold(int basketTotal)
    {
        gold -= basketTotal;
    }
}
