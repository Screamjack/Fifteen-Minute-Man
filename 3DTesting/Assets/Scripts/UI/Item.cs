using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField]
    string id;
    public string ID
    {
        get
        {
            return id;
        }
    }

    [SerializeField]
    string flavorText;
    public string FlavorText
    {
        get { return flavorText; }
    }

    [SerializeField]
    Sprite icon;
    public Sprite Icon
    {
        get { return icon; }
    }


    public override bool Equals(object other)
    {
        if (other.GetType() == typeof(Item))
        {
            Item iOther = other as Item;
            return this.ID == iOther.ID;
        }
        else
            return false; 
    }
    //TODO
}
