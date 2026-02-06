using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            shopKeeperDisplay.gameObject.SetActive(false); 
    }

    private void DisplayShopWindow(ShopSystem shopSystem, PlayerInventoryHolder playerInv)
    {
        shopKeeperDisplay.gameObject.SetActive(true);
        shopKeeperDisplay.DIsplayShopWindow(shopSystem, playerInv);
    }
}
