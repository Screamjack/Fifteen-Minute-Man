using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : AbstractTrigger {

    [SerializeField]
    Item i;

    public override void ActivateTrigger()
    {
        GameManager.manager.GameInventory.GiveItem(i);
    }


}
