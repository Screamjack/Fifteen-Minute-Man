﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBJStart : CameraTrigger {

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

    public override bool CheckTrigger()
    {
        if (GameManager.manager.IsPlaying) return false;
        return base.CheckTrigger();
    }

}
