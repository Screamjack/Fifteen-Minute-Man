using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform tracking;
    public Transform operating;

    void FixedUpdate()
    {
        //tracking.position - tracking.forward;
        operating.position = new Vector3(tracking.position.x - tracking.forward.x*0.75f, tracking.position.y + 0.5f, tracking.position.z - tracking.forward.z*0.75f);

        operating.rotation = Quaternion.LookRotation((tracking.position + (Vector3.up*0.3f)) - operating.position);

    }
}
