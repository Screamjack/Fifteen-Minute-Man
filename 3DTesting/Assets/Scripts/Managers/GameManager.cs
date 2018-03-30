using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager manager;
    GameObject loaderUI;
    GameObject uiMenu;
    bool isOpen = false;
    public bool menuOpen
    {
        get { return isOpen; }
    }

    Inventory inventory;
    public Inventory GameInventory
    {
        get { return inventory; }
    }
    [SerializeField]
    int inventorysize = 10;

    public List<string> flags;

    void Awake()
    {
        if (manager == null) //Singleton
            manager = this;
        if (manager != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        uiMenu = GameObject.Find("PopMenu");
        uiMenu.SetActive(false);
        inventory = new Inventory(inventorysize);
        flags = new List<string>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ToggleMenu(0);
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            ToggleMenu(1);
        }
    }

    void ToggleMenu(int index)
    {
        RotationMaster rm = GameObject.Find("Target").GetComponent<RotationMaster>();
        if (index == 0)
        {
            if (uiMenu.activeInHierarchy)
            {
                uiMenu.SetActive(false);
                isOpen = false;
                rm.SetLock(true);
            }
            else
            {
                uiMenu.SetActive(true);
                isOpen = true;
                rm.SetLock(false);
            }
        }
        else if (index ==1)
        {
            InventoryUIHook inventoryUI = GameObject.Find("Inventory").GetComponent<InventoryUIHook>();
            if (inventoryUI.gameObject.activeInHierarchy)
            {
                inventoryUI.Toggle();
                rm.SetLock(true);
            }
            else
            {
                inventoryUI.Toggle();
                rm.SetLock(false);
            }
        }
    }



}
