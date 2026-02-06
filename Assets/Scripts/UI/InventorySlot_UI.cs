using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private GameObject slotHighlight;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedInventorySlot;

    private Button button;

    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay ParentDisplay { get; private set; }


    private void Awake()
    {
        ClearSlot();

        itemSprite.preserveAspect = true; // This is because the 'preserve aspect ratio' box will not appear if the source image does not have a sprite.

        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot;
        UpdateUISlot(slot);
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        if (slot.ItemData != null)
        {
            itemSprite.sprite = slot.ItemData.Icon;
            itemSprite.color = Color.white;

            if (slot.StackSize > 1)
                itemCount.text = slot.StackSize.ToString();

            else
                itemCount.text = "";
        }
        else
        {
            itemSprite.sprite = null;
            itemSprite.color = Color.clear;
            itemCount.text = "";
        }
    }

    public void UpdateUISlot()
    {
        if (assignedInventorySlot != null)
        {
            UpdateUISlot(assignedInventorySlot);
        }

    }
    public void ClearSlot()
    {
        assignedInventorySlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    public void OnUISlotClick()
    {
        // Access display class function.
        ParentDisplay?.SlotClicked(this);
    }

    public void ToggleHighlight()
    {
        slotHighlight.SetActive(!slotHighlight.activeInHierarchy);
    }
}
