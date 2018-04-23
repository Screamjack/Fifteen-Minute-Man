using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : Interactable {

    [SerializeField]
    AbstractTrigger trigger;

    public override void Enact()
    {
        if (!trigger.Completed && !trigger.Activated)
            trigger.ActivateTrigger();
    }
}
    