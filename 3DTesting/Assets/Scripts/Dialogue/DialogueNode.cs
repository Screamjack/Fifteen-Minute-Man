using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNode {

    string talk;    //NPC text. Rebuttals from options.
    public string TalkText
    {
        get { return talk; }
    }

    DialogueOption[] choices;
    List<Item> items; //Items in order of dialogue.    

    public void SetupNode(DialogueOption[] options, string talk)
    {
        choices = options;
        this.talk = talk;
    }

    public void LoadChoices(GameObject UI = null)   //TODO: Hook this into a UI.
    {
        Debug.Log(talk);
        int index = 0;
        int i = 1;
        foreach(DialogueOption d in choices)
        {
            if(d.checkViability())
            {
                Debug.Log("\t" + i.ToString() + ": "  + d.Rebuttal);
                index++;
                i++;
            }
            else
            {
                Debug.Log("\tX " + i.ToString() + ": " + d.Rebuttal);
                i++;
            }
        }
    }

    public LoadType RunOption(DialogueOption selected,ref DialogueNode current)
    {
        LoadType retval;
        if (!selected.CanProgress)
        {
            Debug.Log("You can't do that yet.");
            retval = LoadType.OptionLocked;
        }
        else
        {
            selected.Enact();
            if (selected.NextNode != null)
            {
                retval = LoadType.Good;
                current = selected.NextNode;
            }
            else
            {
                retval = LoadType.EndOfTree;
                current = null;
            }
        }
        return retval;
    }

    public DialogueOption FindOption(int index)
    {
        DialogueOption ret = null;
        if(index >= 0 && index < choices.Length)
        {
            ret = choices[index]; 
        }
        return ret;
    }


    public enum LoadType
    {
        Good,EndOfTree,OptionLocked
    }
}
