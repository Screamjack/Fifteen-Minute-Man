using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree : MonoBehaviour {
    DialogueNode root;
    DialogueNode curRoot;

    [SerializeField]
    AbstractTrigger toTrigger;
    [SerializeField]
    GameObject UI;

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
        node1.SetupNode(new DialogueOption[] { link1, link2 }, "Hello");
        node2.SetupNode(new DialogueOption[] { link3 }, "Well it worked");
        node3.SetupNode(new DialogueOption[] { link4 }, "Hopefully it opened");

        root = node1;
        UI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pepsi");
        if(other.gameObject.tag == "Player")
        {
            inTalk = true;
            StartCoroutine(Talking());
        }

    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("BEpis");
        if (other.gameObject.tag == "Player")
        {
            inTalk = false;
        }

    }

    IEnumerator Talking()
    {
        curRoot = root;
        UI.SetActive(true);
        curRoot.LoadChoices(UI);
        while (inTalk)
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (curRoot.RunOption(curRoot.FindOption(0), ref curRoot) == DialogueNode.LoadType.EndOfTree)
                {
                    inTalk = false;
                    break;
                }
                curRoot.LoadChoices(UI);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (curRoot.RunOption(curRoot.FindOption(1), ref curRoot) == DialogueNode.LoadType.EndOfTree)
                {
                    inTalk = false;
                    break;
                }
                curRoot.LoadChoices(UI);

            }
            yield return null;
        }
        UI.SetActive(false);
    }
}
