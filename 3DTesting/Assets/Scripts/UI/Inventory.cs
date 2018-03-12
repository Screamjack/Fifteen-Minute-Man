using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Inventory{

    int size;
    int Size
    {
        get { return size; }
    }

    List<Item> inventory;
    public List<Item> InventoryList
    {
        get { return inventory; }
    }

    /// <summary>
    /// Constructs an inventory.
    /// </summary>
    /// <param name="size">Default 10. How many items can the inventory hold?</param>
    /// <param name="items">Default empty list. The actual list that will store items</param>
    public Inventory(int size = 10, List<Item> items = null)
    {
        if (items == null)
            inventory = new List<Item>();
        else
            inventory = items;
        this.size = size;
    }
    

    public bool FindItem(Item i)
    {
        bool retVal = false;
        foreach (Item item in inventory)
        {
            if (item.ID == i.ID)
            {
                retVal = true;
                break;
            }
        }
        return retVal;
    }

    public void TakeItem(Item i)
    {
        if (FindItem(i))
        {
            inventory.Remove(i);
            MonoBehaviour.Destroy(i.gameObject);
        }
        else Debug.Log("No item found");
    }

    public bool GiveItem(Item i)
    {
        if (inventory.Count >= size)
        {
            Debug.Log("Inventory Full");
            return false;
        }
        else
        {
            inventory.Add(i);
            return true;
        }
    }
}
