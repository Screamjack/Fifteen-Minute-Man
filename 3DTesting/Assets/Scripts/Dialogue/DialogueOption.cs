using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOption {

    protected static PlayerController player;

    protected List<AbstractTrigger> preReqs;
    public List<AbstractTrigger> Requirements
    {
        get { return preReqs; }
    }
    protected bool canDo;
    public bool CanProgress
    {
        get { return canDo; }
    }
    protected string optionText;
    public string Rebuttal
    {
        get { return optionText; }
    }
    protected DialogueNode nextNode;
    public DialogueNode NextNode
    {
        get { return nextNode; }
    }

    public DialogueOption()
    {
        optionText = "YOU FUCKED UP THE DIALOGUE MY DUDE";
        preReqs = new List<AbstractTrigger>();
        canDo = false;
    }
    public DialogueOption(string text,DialogueNode next = null,List<AbstractTrigger> ptriggers = null)
    {
        optionText = text;
        nextNode = next;
        preReqs = ptriggers == null ? new List<AbstractTrigger>() : ptriggers;
    }

    public bool checkViability()
    {
        if (preReqs.Count == 0)
        {
            canDo = true;
            return true;
        }
        bool retVal = true;
        foreach (AbstractTrigger t in preReqs)
        {
            if (!t.Completed)
                retVal = false;
        }
        Debug.Log("Prereqs Done? " + retVal);
        canDo = retVal;
        return retVal;
    }

    public virtual void Enact()
    {
        Debug.Log("Nothing to do here.");
    }

}
