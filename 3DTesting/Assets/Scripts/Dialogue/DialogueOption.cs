using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOption {

    static PlayerController player;

    List<AbstractTrigger> preReqs;
    public List<AbstractTrigger> Requirements
    {
        get { return preReqs; }
    }
    AbstractTrigger trigger;
    public AbstractTrigger Trigger
    {
        get { return trigger; }
    }
    bool canDo;
    public bool CanProgress
    {
        get { return canDo; }
    }
    string optionText;
    public string Rebuttal
    {
        get { return optionText; }
    }
    OptionType type;
    public OptionType DialogueType //Needed in node to check if available.
    {
        get { return type; }
    }
    DialogueNode nextNode;
    public DialogueNode NextNode
    {
        get { return nextNode; }
    }

    public DialogueOption()
    {
        optionText = "YOU FUCKED UP THE DIALOGUE MY DUDE";
        preReqs = new List<AbstractTrigger>();
        trigger = null;
        canDo = false;
        type = OptionType.Basic;
    }
    public DialogueOption(string text,List<AbstractTrigger> ptriggers = null, AbstractTrigger toTrigger = null, OptionType tType = OptionType.Basic)
    {
        optionText = text;
        preReqs = ptriggers == null ? new List<AbstractTrigger>() : ptriggers;
        trigger = toTrigger;
        type = tType;
    }

    public bool checkViability()
    {
        if (preReqs.Count == 0) return true;
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

    public bool checkViability(Item i)
    {
        if(player == null)
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            if(player == null)
            {
                Debug.LogError("Failed to find player");
                return false;
            }
        }
        Item item = player.Inventory.Find(x => x.Name == i.Name);//Lambda to find an item of the same type in Inventory.
        bool retVal = item != null; //default(Item) == null
        canDo = retVal;
        return retVal;
    }


    public enum OptionType { Item,Trigger,Basic};
}
