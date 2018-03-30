using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemTrigger : AbstractTrigger {

    [SerializeField]
    Item check;

    public override bool CheckTrigger()
    {
        bool retVal = base.CheckTrigger();
        retVal &= GameManager.manager.GameInventory.FindItem(check);
        Debug.Log("Prereqs Done? " + retVal);
        return retVal;
    }

    public override void ActivateTrigger()
    {
        if (CheckTrigger() && !completed)
        {
            activated = true;
            //Do the thing
        }
    }
}
