using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]
public class ShopKeeper : MonoBehaviour, IInteractable
{
    [SerializeField] private ShopItemList shopItemHeld;
    [SerializeField] private ShopSystem shopSystem;

    private void Awake()
    {
        shopSystem = new ShopSystem(shopItemHeld.Items.Count, shopItemHeld.MaxAllowedGold, shopItemHeld.BuyMarkUp, shopItemHeld.SellMarkUp); ;

        foreach (var item in shopItemHeld.Items)
        {
            Debug.Log($"{item.ItemData.DisplayName} : {item.Amount}");
            shopSystem.AddToShop(item.ItemData, item.Amount);
        }
    }
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        throw new System.NotImplementedException();
    }
}
