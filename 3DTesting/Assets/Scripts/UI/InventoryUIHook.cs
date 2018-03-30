using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIHook : MonoBehaviour {

    Inventory mainInv;
    [SerializeField]
    GameObject inv;
    bool open = false;

    void Awake()
        {
        mainInv = GameManager.manager.GameInventory;
        inv.SetActive(false);
        }

    public void Toggle()
    {
        if(open)
        {
            inv.SetActive(false);
            open = false;
        }
        else
        {
            inv.SetActive(true);
            open = true;
            Refresh();
        }
    }

    void Refresh()
    {
        GameObject Container = inv.transform.GetChild(1).gameObject;
        Debug.Log(Container.name);
        int index = 0;
        for(int slot = 0; slot < Container.transform.childCount; slot++)
        {
            Debug.Log(slot);
            if(index < mainInv.InventoryList.Count)
            {
                Container.transform.GetChild(slot).gameObject.SetActive(true);
                Container.transform.GetChild(slot).GetComponent<Image>().sprite = mainInv.InventoryList[index].Icon;

                index += 1;
                //Do item info
            }
            else
            {
                Container.transform.GetChild(slot).gameObject.SetActive(false);
            }
        }
    }
}
