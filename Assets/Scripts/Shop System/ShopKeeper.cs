using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]
public class ShopKeeper : MonoBehaviour, IInteractable
{
    [SerializeField] private ShopItemList shopItemHeld;
    [SerializeField] private ShopSystem shopSystem;

    public static UnityAction<ShopSystem, PlayerInventoryHolder> OnShopWindowRequested;

    private ShopSaveData shopSaveData;
    private string id;

    private void Awake()
    {
        shopSystem = new ShopSystem(shopItemHeld.Items.Count, shopItemHeld.MaxAllowedGold, shopItemHeld.BuyMarkUp, shopItemHeld.SellMarkUp); ;

        foreach (var item in shopItemHeld.Items)
        {
            Debug.Log($"{item.ItemData.DisplayName} : {item.Amount}");
            shopSystem.AddToShop(item.ItemData, item.Amount);
        }

        id = GetComponent<UniqueID>().ID;
        shopSaveData = new ShopSaveData(shopSystem);
    }

    private void Start()
    {
        if (!SaveGameManager.data.shopKeeperDictionary.ContainsKey(id))
        {
            SaveGameManager.data.shopKeeperDictionary.Add(id, shopSaveData);
        }
    }

    private void OnEnable()
    {
        SaveLoad.OnLoadGame += LoadInventory;
    }

    private void OnDisable()
    {
        SaveLoad.OnLoadGame -= LoadInventory;
    }

    private void LoadInventory(SaveData data)
    {
        if (!data.shopKeeperDictionary.TryGetValue(id, out ShopSaveData _shopSaveData))
            return;

        shopSaveData = _shopSaveData;
        shopSystem = _shopSaveData.ShopSystem;
    }

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        var playerInv = interactor.GetComponent<PlayerInventoryHolder>();

        if (playerInv != null)
        {
            OnShopWindowRequested?.Invoke(shopSystem, playerInv);
            EndInteraction();
            interactSuccessful = true;
        }
        else
        {
            interactSuccessful = false;
            Debug.LogError("Player Inventory not found");
        }
    }

    public void EndInteraction()
    {
    }

}

[System.Serializable]
public class ShopSaveData
{
    public ShopSystem ShopSystem;

    public ShopSaveData(ShopSystem shopSystem)
    {
        ShopSystem = shopSystem;
    }
}