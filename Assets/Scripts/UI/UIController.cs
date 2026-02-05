using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private ShopKeeperDisplay shopKeeperDisplay;

    private void Awake()
    {
        shopKeeperDisplay.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        ShopKeeper.OnShopWindowRequested += DisplayShopWindow;
    }

    private void OnDisable()
    {
        ShopKeeper.OnShopWindowRequested -= DisplayShopWindow;
    }

    private void DisplayShopWindow(ShopSystem shopSystem, PlayerInventoryHolder playerInv)
    {
        shopKeeperDisplay.gameObject.SetActive(true);
    }
}
