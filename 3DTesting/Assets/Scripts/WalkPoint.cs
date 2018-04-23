using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPoint : MonoBehaviour {

    [SerializeField]
    Transform toPoint;
    public Transform ToPoint
    {
        get { return toPoint; }
    }
}
