using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<Item, int> collectedItems = new Dictionary<Item, int>();

    public int ChangeMaxInventoryItems { get { return maxInventoryItems; } set { maxInventoryItems += value; } }
    [SerializeField] private int maxInventoryItems = 3;

    public void AddItem(Item item)
    {
        bool invFull = collectedItems.Count >= maxInventoryItems;

        foreach (var invItem in collectedItems.Keys)
        {
            if (invItem == item)
            {
                if (collectedItems[invItem] < invItem.MaxStackSize)
                {
                    collectedItems[invItem]++;
                    return;
                }
                else if (collectedItems[invItem] >= invItem.MaxStackSize && !invFull)
                {
                    collectedItems.Add(item, 1);
                    return;
                }
            }
        }

        if (!invFull)
        {
            collectedItems.Add(item, 1);
            return;
        }
        else return;
    }

    public void RemoveItem(Item item)
    {
        if (collectedItems.ContainsKey(item))
        {
            collectedItems[item]--;
            if (collectedItems[item] <= 0)
            {
                collectedItems.Remove(item);
            }
        }
    }
}
