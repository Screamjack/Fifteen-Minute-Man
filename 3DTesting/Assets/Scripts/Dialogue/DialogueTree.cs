using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTree : MonoBehaviour {
    DialogueNode root;
    DialogueNode curRoot;

    [SerializeField]
    AbstractTrigger toTrigger;
    [SerializeField]
    GameObject UI;
    [SerializeField]
    RotationMaster rm;

    bool ticking = false;

    static DialogueTree currentTree;

    public static DialogueTree CurrentTree
    {
        get { return currentTree; }
    }

    bool inTalk = true;

    void OnEnable()
    {
        DialogueNode node1 = new DialogueNode();//              1
        DialogueNode node2 = new DialogueNode();//          2       3
        DialogueNode node3 = new DialogueNode();//
        DialogueOption link1 = new DialogueOption("testing things",node2);
        DialogueTriggerOption link2 = new DialogueTriggerOption("Open door",node3, null, toTrigger);
        DialogueOption link3 = new DialogueOption("End it");
        DialogueOption link4 = new DialogueOption("Thanks boi");
        node1.SetupNode(new DialogueOption[] { link1, link2 }, "Hello there, my boy. What do you need?");
        node2.SetupNode(new DialogueOption[] { link3 }, "Well it worked");
        node3.SetupNode(new DialogueOption[] { link4 }, "Hopefully it opened");

        root = node1;
        UI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            inTalk = true;
            currentTree = this;
            StartCoroutine(Talking());
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTalk = false;
        }
    }

    IEnumerator Talking()
    {
        rm.SetLock(false);
        curRoot = root;
        UI.SetActive(true);
        curRoot.LoadChoices(UI);
        while (inTalk)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                Advance(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                Advance(1);
            yield return null;
        }
        rm.SetLock(true);
        UI.SetActive(false);
    }

    public void Advance(int option)
    {
        Debug.Log(option);
        if (curRoot.RunOption(curRoot.FindOption(option), ref curRoot) == DialogueNode.LoadType.EndOfTree)
        {
            inTalk = false;
        }

        else
        {
            curRoot.LoadChoices(UI);
        }

    }
}
