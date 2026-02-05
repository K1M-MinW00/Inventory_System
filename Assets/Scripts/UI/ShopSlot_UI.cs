using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private ShopSlot assignedItemSlot;

    public ShopSlot AssignedItemSlot => assignedItemSlot;

    [SerializeField] private Button addItemToCartBtn;
    [SerializeField] private Button removeItemFromCartBtn;

    private int tempAmount;

    public ShopKeeperDisplay ParentDisplay { get; private set; }
    public float MarkUp { get; private set; }


    private void Awake()
    {
        itemSprite.sprite = null;
        itemSprite.preserveAspect = true;
        itemSprite.color = Color.clear;
        itemName.text = "";
        itemCount.text = "";

        addItemToCartBtn?.onClick.AddListener(AddItemToCart);
        removeItemFromCartBtn?.onClick.AddListener(RemoveItemFromCart);

        ParentDisplay = transform.parent.GetComponentInParent<ShopKeeperDisplay>();
    }

    public void Init(ShopSlot slot, float markUp)
    {
        assignedItemSlot = slot;
        MarkUp = markUp;
        tempAmount = slot.StackSize;
        UpdateUISlot();
    }

    private void UpdateUISlot()
    {
        if (assignedItemSlot.ItemData != null)
        {
            itemSprite.sprite = assignedItemSlot.ItemData.Icon;
            itemSprite.color = Color.white;
            itemCount.text = assignedItemSlot.StackSize.ToString();

            var modifiedPrice = ShopKeeperDisplay.GetModifiedPrice(assignedItemSlot.ItemData, 1, MarkUp);
            itemName.text = $"{assignedItemSlot.ItemData.DisplayName} - {modifiedPrice}G";
        }
        else
        {
            itemSprite.sprite = null;
            itemSprite.color = Color.clear;
            itemName.text = "";
            itemCount.text = "";
        }
    }

    private void AddItemToCart()
    {
        if (tempAmount <= 0)
            return;

        tempAmount--;
        ParentDisplay.AddItemToCart(this);
        itemCount.text = tempAmount.ToString();
    }

    private void RemoveItemFromCart()
    {
        if (tempAmount == assignedItemSlot.StackSize)
            return;

        tempAmount++;
        ParentDisplay.RemoveItemFromCart(this);
        itemCount.text = tempAmount.ToString();
    }
}
