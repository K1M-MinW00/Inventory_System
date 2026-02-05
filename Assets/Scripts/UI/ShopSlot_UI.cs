using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private ShopSlot assignedItemSlot;

    [SerializeField] private Button addItemToCartBtn;
    [SerializeField] private Button removeItemFromCartBtn;

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
        UpdateUISlot();
    }

    private void UpdateUISlot()
    {
        if (assignedItemSlot.ItemData != null)
        {
            itemSprite.sprite = assignedItemSlot.ItemData.Icon;
            itemSprite.color = Color.white;
            itemCount.text = assignedItemSlot.StackSize.ToString();
            itemName.text = $"{assignedItemSlot.ItemData.DisplayName} - {assignedItemSlot.ItemData.GoldValue}G";
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
        Debug.Log("Adding Item To Cart");
    }

    private void RemoveItemFromCart()
    {
        Debug.Log("Removing Item From Cart");
    }
}
