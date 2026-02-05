using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeperDisplay : MonoBehaviour
{
    [SerializeField] private ShopSlot_UI shopSlotPrefab;
    [SerializeField] private ShoppingCartItem_UI shoppingCartItemPrefab;

    [SerializeField] private Button buyTab;
    [SerializeField] private Button sellTab;

    [Header("Shopping Cart")]
    [SerializeField] private TextMeshProUGUI basketTotalText;
    [SerializeField] private TextMeshProUGUI playerGoldText;
    [SerializeField] private TextMeshProUGUI shopGoldText;
    [SerializeField] private Button buyBtn;
    [SerializeField] private TextMeshProUGUI buyBtnText;

    [Header("Item Preview Section")]
    [SerializeField] private Image itemPreviewSprite;
    [SerializeField] private TextMeshProUGUI itemPreviewName;
    [SerializeField] private TextMeshProUGUI itemPreviewDescription;

    [SerializeField] private GameObject itemListContentPanel;
    [SerializeField] private GameObject shoppingCartContentPanel;

    private ShopSystem shopSystem;
    private PlayerInventoryHolder playerInventoryHolder;

    private Dictionary<ItemData, int> shoppingCart = new Dictionary<ItemData, int>();
    private Dictionary<ItemData, ShoppingCartItem_UI> shoppingCartUI = new Dictionary<ItemData, ShoppingCartItem_UI>();

    private int basketTotal;
    private bool isSelling;

    public void DIsplayShopWindow(ShopSystem _shopSystem, PlayerInventoryHolder _playerInv)
    {
        shopSystem = _shopSystem;
        playerInventoryHolder = _playerInv;

        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        if (buyBtn != null)
        {
            buyBtnText.text = isSelling ? "Sell Items" : "Buy Items";
            buyBtn.onClick.RemoveAllListeners();

            if (isSelling)
                buyBtn.onClick.AddListener(SellItems);
            else
                buyBtn.onClick.AddListener(BuyItems);
        }

        ClearSlots();

        basketTotalText.enabled = false;
        buyBtn.gameObject.SetActive(false);

        basketTotal = 0;

        playerGoldText.text = $"Player Gold :{playerInventoryHolder.PrimaryInventorySystem.Gold}";
        shopGoldText.text = $"Shop Gold :{shopSystem.AvailableGold}";

        DisplayShopInventory();
    }

    private void SellItems()
    {
    }

    private void BuyItems()
    {
        if (playerInventoryHolder.PrimaryInventorySystem.Gold < basketTotal)
            return;

        if (!playerInventoryHolder.PrimaryInventorySystem.CheckInventoryRemaining(shoppingCart))
            return;

        foreach (var kvp in shoppingCart)
        {
            shopSystem.PurchaseItem(kvp.Key, kvp.Value);

            for (int i = 0; i < kvp.Value; i++)
            {
                playerInventoryHolder.PrimaryInventorySystem.AddToInventory(kvp.Key, 1);
            }
        }

        shopSystem.GainGold(basketTotal);
        playerInventoryHolder.PrimaryInventorySystem.SpendGold(basketTotal);

        RefreshDisplay();
    }


    private void ClearSlots()
    {
        shoppingCart = new Dictionary<ItemData, int>();
        shoppingCartUI = new Dictionary<ItemData, ShoppingCartItem_UI>();

        foreach (var item in itemListContentPanel.transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        foreach (var item in shoppingCartContentPanel.transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }
    }

    private void DisplayShopInventory()
    {
        foreach (var item in shopSystem.ShopInventory)
        {
            if (item.ItemData == null)
                continue;

            var shopSlot = Instantiate(shopSlotPrefab, itemListContentPanel.transform);
            shopSlot.Init(item, shopSystem.BuyMarkUp);
        }
    }

    private void DisplayPlayerInventory()
    {

    }

    public void AddItemToCart(ShopSlot_UI shopSlot_UI)
    {
        var data = shopSlot_UI.AssignedItemSlot.ItemData;

        UpdateItemPreview(shopSlot_UI);

        var price = GetModifiedPrice(data, 1, shopSlot_UI.MarkUp);


        if (shoppingCart.ContainsKey(data))
        {
            shoppingCart[data]++;
            var newString = $"{data.DisplayName} ({price}G) x {shoppingCart[data]}";
            shoppingCartUI[data].SetItemText(newString);
        }
        else
        {
            shoppingCart.Add(data, 1);

            var shoppingCartTextObj = Instantiate(shoppingCartItemPrefab, shoppingCartContentPanel.transform);
            var newString = $"{data.DisplayName} ({price}G) x 1";
            shoppingCartTextObj.SetItemText(newString);
            shoppingCartUI.Add(data, shoppingCartTextObj);
        }


        basketTotal += price;
        basketTotalText.text = $"Total: {basketTotal}G";

        if (basketTotal > 0 && !basketTotalText.IsActive())
        {
            basketTotalText.enabled = true;
            buyBtn.gameObject.SetActive(true);
        }

        CheckCartVsAvailableGold();
    }

    private void CheckCartVsAvailableGold()
    {
        var goldToCheck = isSelling ? shopSystem.AvailableGold : playerInventoryHolder.PrimaryInventorySystem.Gold;

        basketTotalText.color = basketTotal > goldToCheck ? Color.red : Color.white;

        if (isSelling || playerInventoryHolder.PrimaryInventorySystem.CheckInventoryRemaining(shoppingCart))
            return;

        basketTotalText.text = "Not enough room in inventory.";
        basketTotalText.color = Color.red;
    }

    public static int GetModifiedPrice(ItemData data, int amount, float markUp)
    {
        var baseValue = data.GoldValue * amount;

        return Mathf.RoundToInt(baseValue + baseValue * markUp);
    }

    public void RemoveItemFromCart(ShopSlot_UI shopSlot_UI)
    {
        var data = shopSlot_UI.AssignedItemSlot.ItemData;
        var price = GetModifiedPrice(data, 1, shopSlot_UI.MarkUp);

        if (shoppingCart.ContainsKey(data))
        {
            shoppingCart[data]--;
            var newString = $"{data.DisplayName} ({price}G) x {shoppingCart[data]}";
            shoppingCartUI[data].SetItemText(newString);

            if (shoppingCart[data] <= 0)
            {
                shoppingCart.Remove(data);
                var tempObj = shoppingCartUI[data].gameObject;
                shoppingCartUI.Remove(data);
                Destroy(tempObj);
            }
        }

        basketTotal -= price;
        basketTotalText.text = $"Total: {basketTotal}G";

        if (basketTotal <= 0 && basketTotalText.IsActive())
        {
            basketTotalText.enabled = false;
            buyBtn.gameObject.SetActive(false);
            ClearItemPreview();
        }

        CheckCartVsAvailableGold();
    }

    private void ClearItemPreview()
    {

    }

    private void UpdateItemPreview(ShopSlot_UI shopSlot_UI)
    {

    }


}
