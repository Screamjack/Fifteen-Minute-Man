using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        /* UI FORM
 *         ROOT
 *          DIALOGUEBOX
 *              DIALOGUE BODY
 *                  BODY TEXT   (Dump talk here)  
 *              OPTIONS BODY
 *                  OPTION TEXT (Iterate rebuttals here) (Might be buttons instead of number optiinos)
 * 
 */
        Text dText = UI.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        Text rText = UI.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        if(dText == null || rText == null)
        {
            Debug.LogError("Either dialogue text box or rebuttal text box are missing.");
            return;
        }
        dText.text = talk;
        rText.text = "";
        int index = 0;
        int i = 1;
        foreach(DialogueOption d in choices)
        {
            if(d.checkViability())
            {
                rText.text += i.ToString() + ": " + d.Rebuttal + "\n";
                index++;
                i++;
            }
            else
            {
                rText.text += "XXX " + i.ToString() + ": " + d.Rebuttal + "\n";
                i++;
            }
        }
    }

    public LoadType RunOption(DialogueOption selected,ref DialogueNode current)
    {
        if (selected == null) return LoadType.EndOfTree;
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
                Debug.Log("End");
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
