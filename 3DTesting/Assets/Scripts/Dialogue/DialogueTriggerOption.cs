using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerOption : DialogueOption {
    AbstractTrigger trigger;
    public AbstractTrigger Trigger
    {
        get { return trigger; }
    }


    public DialogueTriggerOption() : base()
    {
        trigger = null;
    }
    public DialogueTriggerOption(string text,DialogueNode next, List<string> ptriggers = null,AbstractTrigger toTrigger = null) : base(text,next,ptriggers)
    {
        trigger = toTrigger;
        preReqs.AddRange(toTrigger.Requirements);
    }
    public void SetTrigger(GameObject obj, string component)
    {
        trigger = obj.GetComponent(component) as AbstractTrigger;
        preReqs.AddRange(trigger.Requirements);
    }

    public override void Enact()
    {
        trigger.ActivateTrigger();
    }
}
