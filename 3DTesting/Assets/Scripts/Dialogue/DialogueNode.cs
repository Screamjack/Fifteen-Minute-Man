using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNode : MonoBehaviour {

    List<DialogueOption> choices;
    List<Item> items; //Items in order of dialogue.    

    public void LoadChoices()
    {
        foreach (DialogueOption d in choices)
        {
            if(d.DialogueType == DialogueOption.OptionType.Item)
            {
                d.checkViability();
            }
            else
            {
                d.checkViability();
            }
        }
    }
}
