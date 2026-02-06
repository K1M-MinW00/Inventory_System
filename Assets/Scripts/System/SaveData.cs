using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public List<string> collectedItems;
    public SerializableDictionary<string, ItemPickUpSaveData> activeItems;

    public SerializableDictionary<string, InventorySaveData> chestDictionary;

    public SerializableDictionary<string, ShopSaveData> shopKeeperDictionary;

    public InventorySaveData playerInventory;

    public SaveData()
    {
        collectedItems = new List<string>();

        activeItems = new SerializableDictionary<string, ItemPickUpSaveData>();

        chestDictionary = new SerializableDictionary<string, InventorySaveData>();

        shopKeeperDictionary = new SerializableDictionary<string, ShopSaveData>();

        playerInventory = new InventorySaveData();
    }
}