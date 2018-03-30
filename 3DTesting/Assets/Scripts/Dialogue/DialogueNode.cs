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
    public void setTalk(string talk)
    {
        this.talk = talk;
    }
    public void setOptions(DialogueOption [] options)
    {
        choices = options;
    }

    public void LoadChoices(GameObject UI = null)   //TODO: Hook this into a UI.
    {
        /* UI FORM
 *         ROOT
 *          DIALOGUEBOX
 *            OPTIONS BODY
 *                  1           Option
 *                  2           Texts
 *                  3
 *                  4
 *                  B1         Option
 *                  B2         Buttons
 *                  B3
 *                  B4
 *              DIALOGUE BODY
 *                  BODY TEXT   (Dump talk here)  
 * 
 */
        TextIntermediate bodyText = UI.transform.GetChild(1).GetChild(0).GetComponent<TextIntermediate>();
        GameObject rebuttals = UI.transform.GetChild(0).gameObject;

        if (bodyText == null || rebuttals == null)
        {
            Debug.LogError("Either dialogue text box or rebuttal text box are missing.");
            return;
        }

        if (bodyText.isWriting)
            bodyText.StopWrite();
        bodyText.Write(talk,1);

        for (int i = 0; i < 4; i++) //Reset the buttons
        {
            rebuttals.transform.GetChild(i).GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        }
        
        //int index = 0;
        //foreach (DialogueOption d in choices) //Load all active choices
        //{
        //    if (d.checkViability())
        //    {
        //        rebuttals.transform.GetChild(index).GetComponent<Text>().text = d.Rebuttal;
        //        switch(index) //Because I don't know how delegates work...
        //        {
        //            case 0:
        //                rebuttals.transform.GetChild(index).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { DialogueTree.CurrentTree.Advance(0); });
        //                break;
        //            case 1:
        //                rebuttals.transform.GetChild(index).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { DialogueTree.CurrentTree.Advance(1); });
        //                break;
        //            case 2:
        //                rebuttals.transform.GetChild(index).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { DialogueTree.CurrentTree.Advance(2); });
        //                break;
        //            case 3:
        //                rebuttals.transform.GetChild(index).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { DialogueTree.CurrentTree.Advance(0); });
        //                break;
        //        }
        //        index++;
        //    }
        //}
        //for (int i = index; i < 4; i++) //Clear out the rest
        //{
        //    rebuttals.transform.GetChild(i).GetComponent<Text>().text = "";
        //}

        for(int i = 0; i < 4; i++)
        {
            if(i < choices.Length && choices[i].checkViability())
            {
                rebuttals.transform.GetChild(i).GetComponent<Text>().text = choices[i].Rebuttal;
                switch (i) //Because I don't know how delegates work...
                {
                    case 0:
                        Debug.Log("0");
                        rebuttals.transform.GetChild(i).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { DialogueTree.CurrentTree.Advance(0); });
                        break;
                    case 1:
                        rebuttals.transform.GetChild(i).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { DialogueTree.CurrentTree.Advance(1); });
                        break;
                    case 2:
                        rebuttals.transform.GetChild(i).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { DialogueTree.CurrentTree.Advance(2); });
                        break;
                    case 3:
                        rebuttals.transform.GetChild(i).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { DialogueTree.CurrentTree.Advance(3); });
                        break;
                }
            }
            else
                rebuttals.transform.GetChild(i).GetComponent<Text>().text = "";
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
