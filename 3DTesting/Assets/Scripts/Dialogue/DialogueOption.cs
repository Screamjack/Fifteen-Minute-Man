using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOption {

    protected static PlayerController player;

    protected List<string> preReqs;
    public List<string> Requirements
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
        preReqs = new List<string>();
        canDo = false;
    }
    public DialogueOption(string text,DialogueNode next = null,List<string> ptriggers = null)
    {
        optionText = text;
        nextNode = next;
        preReqs = ptriggers == null ? new List<string>() : ptriggers;
    }

    public void SetNext(DialogueNode next)
    {
        nextNode = next;
    }
    public void SetText(string t)
    {
        optionText = t;
    }

    public bool checkViability()
    {
        if (preReqs.Count == 0)
        {
            canDo = true;
            return true;
        }
        bool retVal = true;
        GameManager.manager.flags.ToString();
        foreach (string t in preReqs)
        {
            bool isDone = GameManager.manager.flags.Contains(t);
            Debug.Log(t + "?: " + isDone);
            if (!isDone)
                retVal = false;
        }
        canDo = retVal;
        return retVal;
    }

    public virtual void Enact()
    {
        Debug.Log("Nothing to do here.");
    }

}
