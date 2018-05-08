using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : Interactable {

    [SerializeField]
    AbstractTrigger trigger;

    [SerializeField]
    bool force = false;

    public override void Enact()
    {
        if (force)
            trigger.ActivateTrigger();
        else
            if (!trigger.Completed)
                trigger.ActivateTrigger();
    }
}
    