using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] protected int secondaryInventorySize;
    [SerializeField] protected InventorySystem secondaryInventorySystem;

    public InventorySystem SecondaryInventorySystem => secondaryInventorySystem;

    public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;

    protected override void Awake()
    {
        base.Awake();

        secondaryInventorySystem = new InventorySystem(secondaryInventorySize);
    }


    private void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            OnPlayerBackpackDisplayRequested?.Invoke(secondaryInventorySystem);
        }
    }

    public bool AddToInventory(ItemData itemData, int amount)
    {
        if (primaryInventorySystem.AddToInventory(itemData, amount))
        {
            return true;
        }
        else if (secondaryInventorySystem.AddToInventory(itemData, amount))
        {
            return true;
        }

        return false;
    }
}
