using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ShopSystem
{
    [SerializeField] private List<ShopSlot> shopInventory;
    [SerializeField] private int availableGold;
    [SerializeField] private float buyMarkUp;
    [SerializeField] private float sellMarkUp;

    public List<ShopSlot> ShopInventory => shopInventory;
    public int AvailableGold => availableGold;
    public float BuyMarkUp => buyMarkUp;
    public float SellMarkUp => sellMarkUp;

    public ShopSystem(int _size, int _gold, float _buyMarkUp, float _sellMarkUp)
    {
        availableGold = _gold;
        buyMarkUp = _buyMarkUp;
        sellMarkUp = _sellMarkUp;

        SetShopSize(_size);
    }

    private void SetShopSize(int _size)
    {
        shopInventory = new List<ShopSlot>();

        for (int i = 0; i < _size; ++i)
        {
            shopInventory.Add(new ShopSlot());
        }
    }

    public void AddToShop(ItemData itemData, int amount)
    {
        if (ContainsItem(itemData, out ShopSlot shopSlot))
        {
            shopSlot.AddToStack(amount);
        }

        var freeSlot = GetFreeSlot();
        freeSlot.AssignItem(itemData, amount);
    }

    public bool ContainsItem(ItemData itemToAdd, out ShopSlot shopSlot)
    {
        shopSlot = shopInventory.Find(i => i.ItemData == itemToAdd);

        return shopSlot != null;
    }

    private ShopSlot GetFreeSlot()
    {
        var freeSlot = shopInventory.FirstOrDefault(i => i.ItemData == null);

        if (freeSlot == null)
        {
            freeSlot = new ShopSlot();
            shopInventory.Add(freeSlot);
        }

        return freeSlot;
    }

    public void PurchaseItem(ItemData data, int amount)
    {
        if (!ContainsItem(data, out ShopSlot shopSlot))
            return;

        shopSlot.RemoveFromStack(amount);
    }

    public void GainGold(int basketTotal)
    {
        availableGold += basketTotal;
    }
}
