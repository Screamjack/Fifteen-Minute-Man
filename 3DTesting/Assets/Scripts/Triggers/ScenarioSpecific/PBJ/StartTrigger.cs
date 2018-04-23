using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : CameraTrigger {

    private bool started = false;
    private bool prepped = false;
    [SerializeField]
    GameObject sandwich;



    void Update()
    {
        if(activated && !prepped)
        {
            sandwich.SetActive(true);
            prepped = true;
        }

        if(completed && !started)
        {
            GameManager.manager.StartItAll("pbj");
            Debug.Log("LET'S GO");
            started = true;
        }
    }


}
