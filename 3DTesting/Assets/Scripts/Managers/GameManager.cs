using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    GameManager manager;
    GameObject loaderUI;

    List<Item> inventory;
    [SerializeField]
    int inventorysize = 10;

    void Awake()
    {
        if (manager == null) //Singleton
            manager = this;
        if (manager != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        inventory = new List<Item>();
    }

    public bool FindItem(Item i)
    {
        bool retVal = false;
        foreach(Item item in inventory)
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
            Destroy(i.gameObject);
        }
        else Debug.Log("No item found");
    }

    public bool GiveItem(Item i)
    {
        if(inventory.Count >= inventorysize)
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
