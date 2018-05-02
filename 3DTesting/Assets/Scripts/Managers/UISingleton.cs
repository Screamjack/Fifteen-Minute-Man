using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISingleton : MonoBehaviour {

    static UISingleton ui;
    public static UISingleton UI
    {
        get { return ui; }
    }

    void Awake()
    {
        if(ui == null)
        {
            ui = this;
        }
        else if(ui != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

}
