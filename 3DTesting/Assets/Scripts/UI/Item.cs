using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    string id;
    public string ID
    {
        get
        {
            return id;
        }
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
