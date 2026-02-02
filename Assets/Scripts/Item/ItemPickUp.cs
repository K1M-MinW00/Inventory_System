using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(UniqueID))]
public class ItemPickUp : MonoBehaviour
{
    public float pickUpRadius = 1f;
    public ItemData itemData;

    private SphereCollider col;

    [SerializeField] private ItemPickUpSaveData itemSaveData;
    private string id;

    private void Awake()
    {
        id = GetComponent<UniqueID>().ID;
        SaveLoad.OnLoadGame += LoadGame;
        itemSaveData = new ItemPickUpSaveData(itemData, transform.position, transform.rotation);

        col = GetComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = pickUpRadius;
    }

    private void Start()
    {
        SaveGameManager.data.activeItems.Add(id, itemSaveData);
        SaveLoad.OnLoadGame += LoadGame;
    }

    private void LoadGame(SaveData data)
    {
        if (data.collectedItems.Contains(id))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (SaveGameManager.data.activeItems.ContainsKey(id))
            SaveGameManager.data.activeItems.Remove(id);

        SaveLoad.OnLoadGame -= LoadGame;
    }

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();

        if (inventory == null)
            return;

        if (inventory.AddToInventory(itemData, 1))
        {
            SaveGameManager.data.collectedItems.Add(id);
            Destroy(this.gameObject);
        }
    }
}


[System.Serializable]
public struct ItemPickUpSaveData
{
    public ItemData itemData;
    public Vector3 position;
    public Quaternion rotation;

    public ItemPickUpSaveData(ItemData _itemData, Vector3 _position, Quaternion _rotation)
    {
        itemData = _itemData;
        position = _position;
        rotation = _rotation;
    }

}