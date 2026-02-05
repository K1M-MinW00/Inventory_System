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
    private PlayerInventoryHolder playerInv;

    private Dictionary<ItemData, int> shoppingCart = new Dictionary<ItemData, int>();
    private Dictionary<ItemData, ShoppingCartItem_UI> shoppingCartUI = new Dictionary<ItemData, ShoppingCartItem_UI>();

    private int basketTotal;

    public void DIsplayShopWindow(ShopSystem _shopSystem, PlayerInventoryHolder _playerInv)
    {
        shopSystem = _shopSystem;
        playerInv = _playerInv;

        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        ClearSlots();

        basketTotalText.enabled = false;
        buyBtn.gameObject.SetActive(false);

        basketTotal = 0;

        playerGoldText.text = $"Player Gold :{playerInv.PrimaryInventorySystem.Gold}";
        shopGoldText.text = $"Shop Gold :{shopSystem.AvailableGold}";

        DisplayShopInventory();
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
}
