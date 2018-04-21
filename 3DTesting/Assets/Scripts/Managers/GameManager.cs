using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private int inventorysize = 10;

    public static GameManager manager;
    GameObject uiMenu;
    GameObject popMenu;
    Timer clock;
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

    public List<string> flags;

    private bool isPlaying = false;
    public bool IsPlayer
    {
        get { return isPlaying; }
    }

    private string scenario;
    public string Scenario
    {
        get { return scenario; }
    }

    int seconds;
    public int Seconds
    {
        get { return seconds; }
    }

    private bool tickTheClock;
    public bool TickTheClock
    {
        get { return tickTheClock; }
        set { tickTheClock = value; }
    }

    void Awake()
    {
        if (manager == null) //Singleton
            manager = this;
        if (manager != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        uiMenu = GameObject.Find("UI Canvas");
        popMenu = uiMenu.transform.GetChild(3).gameObject;
        popMenu.SetActive(false);
        uiMenu.transform.GetChild(1).gameObject.SetActive(false);
        clock = uiMenu.transform.GetChild(1).GetComponent<Timer>();
        inventory = new Inventory(inventorysize);
        flags = new List<string>();
        seconds = 900; // 15 minutes
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
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartItAll("test");
        }
    }

    void ToggleMenu(int index)
    {
        RotationMaster rm = GameObject.Find("Target").GetComponent<RotationMaster>();
        if (index == 0)
        {
            if (popMenu.activeInHierarchy)
            {
                popMenu.SetActive(false);
                isOpen = false;
                rm.SetLock(true);
            }
            else
            {
                popMenu.SetActive(true);
                isOpen = true;
                rm.SetLock(false);
            }
        }
        else if (index == 1)
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

    public void StartItAll(string s)
    {
        Debug.Log("IT'S HAPPENING");
        isPlaying = true;
        scenario = s;
        uiMenu.transform.GetChild(1).gameObject.SetActive(true);
        tickTheClock = true;
        StartCoroutine(ClockTick());

        Debug.Log(scenario);
    }

    public void RecollectInformation()
    {
        uiMenu = GameObject.Find("UI Canvas");
        popMenu = uiMenu.transform.GetChild(3).gameObject;
        popMenu.SetActive(false);
        uiMenu.transform.GetChild(1).gameObject.SetActive(false);
        clock = uiMenu.transform.GetChild(1).GetComponent<Timer>();
    }

    IEnumerator ClockTick()
    {
        while(seconds > 0)
        {
            if(tickTheClock)
            {
                seconds -= 1;
                clock.UpdateUI(seconds);
                Debug.Log(seconds);
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return null;
            }
        }
        Debug.Log("It's over, m8");
    }

}
