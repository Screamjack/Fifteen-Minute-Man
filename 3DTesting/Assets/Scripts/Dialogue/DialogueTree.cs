using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree : MonoBehaviour {
    DialogueNode root;

    [SerializeField]
    AbstractTrigger toTrigger;

    void OnEnable()
    {
        DialogueNode node1 = new DialogueNode();//              1
        DialogueNode node2 = new DialogueNode();//          2       3
        DialogueNode node3 = new DialogueNode();//
        DialogueOption link1 = new DialogueOption("testing things",node2);
        DialogueTriggerOption link2 = new DialogueTriggerOption("Open door",node3, null, toTrigger);
        DialogueOption link3 = new DialogueOption("End it");
        DialogueOption link4 = new DialogueOption("Thanks boi");
        node1.SetupNode(new DialogueOption[] { link1, link2 }, "Hello");
        node2.SetupNode(new DialogueOption[] { link3 }, "Well it worked");
        node3.SetupNode(new DialogueOption[] { link4 }, "Hopefully it opened");

        root = node1;
        root.LoadChoices();
    }

    void Update()
    {
        if (root == null) this.enabled = false;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (root.RunOption(root.FindOption(0), ref root) == DialogueNode.LoadType.EndOfTree) return;
            root.LoadChoices();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(root.RunOption(root.FindOption(1), ref root) == DialogueNode.LoadType.EndOfTree) return;
            root.LoadChoices();
        }
    }
}
