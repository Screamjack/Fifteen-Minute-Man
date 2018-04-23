using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTree : Interactable {
    DialogueNode root;
    DialogueNode curRoot;

    Animator anim;
    Animator playerAnim;

    [SerializeField]
    AbstractTrigger toTrigger;
    [SerializeField]
    GameObject UI;
    [SerializeField]
    RotationMaster rm;
    [SerializeField]
    string dialogueName;

    bool ticking = false;

    static DialogueTree currentTree;

    public static DialogueTree CurrentTree
    {
        get { return currentTree; }
    }

    bool inTalk = true;
    bool talkable = false;

    void Awake()
    {
        anim = UI.GetComponent<Animator>();
        playerAnim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        buildTree();
        //defaultTreeStart();
        UI.SetActive(false);
    }

    void defaultTreeStart() {
        DialogueNode node1 = new DialogueNode();//              1
        DialogueNode node2 = new DialogueNode();//          2       3
        DialogueNode node3 = new DialogueNode();//
        DialogueOption link1 = new DialogueOption("testing things", node2);
        DialogueTriggerOption link2 = new DialogueTriggerOption("Open door", node3, null, toTrigger);
        DialogueOption link3 = new DialogueOption("End it");
        DialogueOption link4 = new DialogueOption("Thanks boi");
        node1.SetupNode(new DialogueOption[] { link1, link2 }, "Hello there, my boy. What do you need?");
        node2.SetupNode(new DialogueOption[] { link3 }, "Well it worked");
        node3.SetupNode(new DialogueOption[] { link4 }, "Hopefully it opened");

        root = node1;
    }

    void buildTree()
    {
        TextAsset xmlDoc = Resources.Load(dialogueName) as TextAsset;
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xmlDoc.text);

        XmlNode first = doc.DocumentElement;
        first = first.ChildNodes[0];
        root = new DialogueNode();

        List<DialogueOption> options = new List<DialogueOption>();
        XmlNode speech = first.SelectSingleNode("Speech");
        root.setTalk(speech.InnerText);

        foreach(XmlNode x in first.ChildNodes) //Root run is a little weird and is split out here. 
        {
            if (x.Name == "Option")
            {
                if(x.Attributes["Type"].Value == "Trigger")
                {
                    DialogueTriggerOption option = new DialogueTriggerOption();
                    option.SetText(x.SelectSingleNode("Speech").InnerText);
                    option.SetTrigger(GameObject.Find(x.Attributes["GameObject"].Value),x.Attributes["TriggerName"].Value);
                    if (x.ChildNodes.Count > 1)
                        option.SetNext(recurse(x));
                    options.Add(option);
                }
                else if(x.Attributes["Type"].Value == "Basic")
                {
                    DialogueOption option = new DialogueOption();
                    option.SetText(x.SelectSingleNode("Speech").InnerText);
                    if (x.ChildNodes.Count > 1)
                        option.SetNext(recurse(x));
                    options.Add(option);
                }
            }
        }
        root.setOptions(options.ToArray());
    }
    /// <summary>
    /// Recursively builds a dialogue tree from an XML tree. 
    /// </summary>
    /// <param name="xmlCur">The current XML node to parse from. The start is a little odd, so look at how it is used above.</param>
    /// <returns>The Dialogue node to connect to an Option.</returns>
    DialogueNode recurse(XmlNode xmlCur) //Cooking up some FRESH SPAGHETTI
    {
        Debug.Log("Recursing");
        List<DialogueOption> options = new List<DialogueOption>();
        XmlNode thisDialogue = xmlCur.SelectSingleNode("Dialogue");
        DialogueNode n = new DialogueNode();
        n.setTalk(thisDialogue.SelectSingleNode("Speech").InnerText);
        foreach (XmlNode x in thisDialogue.ChildNodes)
        {
            if (x.Name == "Option")
            {
                if (x.Attributes["Type"].Value == "Trigger")
                {
                    DialogueTriggerOption option = new DialogueTriggerOption();
                    option.SetText(x.SelectSingleNode("Speech").InnerText);
                    option.SetTrigger(GameObject.Find(x.Attributes["GameObject"].Value), x.Attributes["TriggerName"].Value);
                    if (x.ChildNodes.Count > 1)
                        option.SetNext(recurse(x));
                    options.Add(option);
                }
                else if (x.Attributes["Type"].Value == "Basic")
                {
                    DialogueOption option = new DialogueOption();
                    option.SetText(x.SelectSingleNode("Speech").InnerText);
                    if (x.ChildNodes.Count > 1)
                        option.SetNext(recurse(x));
                    options.Add(option);
                }
            }
        }
        n.setOptions(options.ToArray());
        return n;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            talkable = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            talkable = false;
            inTalk = false;
        }
    }

    public override void Enact()
    {
        StartTalking();
    }

    public void StartTalking()
    {
        if (talkable)
        {
            inTalk = true;
            currentTree = this;
            StartCoroutine(Talking());
        }
    }

    IEnumerator Talking()
    {
        anim.SetTrigger("open");
        playerAnim.SetTrigger("talk");
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
        anim.SetTrigger("close");
    }

    public void ShutOff()
    {
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
            playerAnim.SetTrigger("talk");
            curRoot.LoadChoices(UI);
        }

    }
}
