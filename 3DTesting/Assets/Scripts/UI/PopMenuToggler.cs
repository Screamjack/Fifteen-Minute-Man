using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopMenuToggler : MonoBehaviour {

    Animator anim;
    [SerializeField]
    GameObject PopMenu;
    bool open = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Toggle()
    {
        if (open)
        {
            anim.SetTrigger("close");
            open = false;
        }
        else
        {
            anim.SetTrigger("open");
            PopMenu.SetActive(true);
            open = true;
        }
    }

    void StubShut()
    {
        AnimStub stub = transform.GetChild(0).GetComponent<AnimStub>();
        stub.ShutOff();
    }
}
