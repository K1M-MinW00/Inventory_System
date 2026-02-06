using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarDisplay : StaticInventoryDisplay
{
    private int maxIndexSize;
    private int currentIndex = 0;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    protected override void Start()
    {
        base.Start();

        currentIndex = 0;
        maxIndexSize = slots.Length - 1;

        slots[currentIndex].ToggleHighlight();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        playerControls.Enable();

        playerControls.Player.HotbarSelect.performed += OnHatbarSelected;
        playerControls.Player.UseItem.performed += UseItem;

    }



    protected override void OnDisable()
    {
        base.OnDisable();

        playerControls.Disable();

        playerControls.Player.HotbarSelect.performed -= OnHatbarSelected;
        playerControls.Player.UseItem.performed -= UseItem;
    }

    private void Update()
    {
        if (playerControls.Player.MouseWheel.ReadValue<float>() > 0.1f)
            ChangeIndex(1);
        if (playerControls.Player.MouseWheel.ReadValue<float>() < -0.1f)
            ChangeIndex(-1);
    }



    private void OnHatbarSelected(InputAction.CallbackContext context)
    {
        int index = Mathf.RoundToInt(context.ReadValue<float>());
        SelectHotbar(index);
    }

    private void SelectHotbar(int index)
    {
        slots[currentIndex].ToggleHighlight();

        if (index > maxIndexSize)
            index = 0;
        else if (index < 0)
            index = maxIndexSize;

        currentIndex = index;
        slots[currentIndex].ToggleHighlight();
    }

    private void UseItem(InputAction.CallbackContext context)
    {
        if (slots[currentIndex].AssignedInventorySlot.ItemData != null)
            slots[currentIndex].AssignedInventorySlot.ItemData.UseItem();
    }

    private void ChangeIndex(int direction)
    {
        slots[currentIndex].ToggleHighlight();
        currentIndex += direction;

        if (currentIndex > maxIndexSize)
            currentIndex = 0;
        else if (currentIndex < 0)
            currentIndex = maxIndexSize;

        slots[currentIndex].ToggleHighlight();
    }
}
