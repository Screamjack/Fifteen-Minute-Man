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
    [SerializeField]
    int inventorysize = 10;

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
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        RotationMaster rm = GameObject.Find("Target").GetComponent<RotationMaster>();
        if(uiMenu.activeInHierarchy)
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



}
