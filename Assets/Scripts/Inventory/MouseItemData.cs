using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Unity.VisualScripting;


public class MouseItemData : MonoBehaviour
{
    public Image itemSprite;
    public TextMeshProUGUI itemCount;
    public InventorySlot assignedInventorySlot;

    private Transform playerTransform;
    [SerializeField] private float dropOffset = 2f;

    private void Awake()
    {
        itemSprite.preserveAspect = true;

        itemSprite.color = Color.clear;
        itemCount.text = "";

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (playerTransform == null)
            Debug.LogError("Player Not Found");
    }

    private void Update()
    {
        // TODO : Add controller support

        if (assignedInventorySlot.ItemData != null) // If has an item, follow the mouse position
        {
            transform.position = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                if (assignedInventorySlot.ItemData.ItemPrefab != null) // Drop the item on the ground.
                    Instantiate(assignedInventorySlot.ItemData.ItemPrefab, playerTransform.position + playerTransform.forward * dropOffset, Quaternion.identity);

                if (assignedInventorySlot.StackSize > 1)
                {
                    assignedInventorySlot.AddToStack(-1);
                    UpdateMouseSlot();
                }
                else
                {
                    ClearSlot();
                }

            }
        }
    }

    public void ClearSlot()
    {
        assignedInventorySlot.ClearSlot();
        itemCount.text = "";
        itemSprite.color = Color.clear;
        itemSprite.sprite = null;
    }

    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        this.assignedInventorySlot.AssignItem(invSlot);
        UpdateMouseSlot();
    }

    public void UpdateMouseSlot()
    {
        itemSprite.sprite = assignedInventorySlot.ItemData.Icon;
        itemCount.text = assignedInventorySlot.StackSize.ToString();
        itemSprite.color = Color.white;
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}