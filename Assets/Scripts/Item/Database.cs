using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Item Database")]
public class Database : ScriptableObject
{
    [SerializeField] private List<ItemData> database;

    [ContextMenu(itemName: "Set IDs")]
    public void SetItemIDs()
    {
        database = new List<ItemData>();

        var foundItems = Resources.LoadAll<ItemData>("ItemData").OrderBy(i => i.ID).ToList();

        var hasIDInRange = foundItems.Where(i => i.ID != -1 && i.ID < foundItems.Count).OrderBy(i => i.ID).ToList();
        var hasIDNotInRange = foundItems.Where(i => i.ID != -1 && i.ID > foundItems.Count).OrderBy(i => i.ID).ToList();
        var noID = foundItems.Where(i => i.ID <= -1).ToList();

        var index = 0;

        for (int i = 0; i < foundItems.Count; i++)
        {
            ItemData itemToAdd;
            itemToAdd = hasIDInRange.Find(d => d.ID == i);

            if (itemToAdd != null)
            {
                database.Add(itemToAdd);
            }
            else if (index < noID.Count)
            {
                noID[index].ID = i;
                itemToAdd = noID[index];
                index++;
                database.Add(itemToAdd);
            }
        }

        foreach (var item in hasIDNotInRange)
        {
            database.Add(item);
        }
    }

    public ItemData GetItem(int _id)
    {
        return database.Find(i => i.ID == _id);
    }
}
