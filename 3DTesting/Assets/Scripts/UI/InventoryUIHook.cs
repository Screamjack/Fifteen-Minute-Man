using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIHook : MonoBehaviour {

    Inventory mainInv;
    Animator anim;
    [SerializeField]
    GameObject inv;
    bool open = false;

    void Awake()
        {
        mainInv = GameManager.manager.GameInventory;
        anim = gameObject.GetComponent<Animator>();
        inv.SetActive(false);
    }

    public void Toggle()
    {
        if(open)
        {
            anim.SetTrigger("close");
            open = false;
        }
        else
        {
            anim.SetTrigger("open");
            inv.SetActive(true);
            open = true;
            Refresh();
        }
    }

    void Refresh()
    {
        GameObject Container = inv.transform.GetChild(1).gameObject;
        int index = 0;
        for(int slot = 0; slot < Container.transform.childCount; slot++)
        {
            Debug.Log(slot);
            if(index < mainInv.InventoryList.Count)
            {
                Container.transform.GetChild(slot).gameObject.SetActive(true);
                Container.transform.GetChild(slot).GetChild(0).GetComponent<Image>().sprite = mainInv.InventoryList[index].Icon;

                index += 1;
                //Do item info
            }
            else
            {
                Container.transform.GetChild(slot).gameObject.SetActive(false);
            }
        }
    }

    void StubShut()
    {
        AnimStub stub = transform.GetChild(0).GetComponent<AnimStub>();
        stub.ShutOff();
    }
}
