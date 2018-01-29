using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CameraLocationInformation : MonoBehaviour {
    //TODO: Look into other possible camera things needed.
    //Use this's transform to position camera.
    public float lerpTime, stallTime;

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }

}
