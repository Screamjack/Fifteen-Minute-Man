using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : AbstractTrigger {

    [SerializeField]
    List<CameraLocationInformation> cameraLocations;

    bool isLerping = false;
    bool isStalling = false;
    static Transform cam;
    static Transform camPriorParent;
    Vector3 realigner;
    Quaternion rotationRealigner;
    float epsilon = 0.01f;

    void Start()
    {
        if(cam == null) 
            cam = Camera.main.transform.parent;
        if(camPriorParent == null)
            camPriorParent = cam.parent;
        Debug.Log(cam);
        realigner = new Vector3(0, 0.3f, 0);
        rotationRealigner = Quaternion.identity; 
    }

    public override void ActivateTrigger()
    {
        CameraLocationInformation[] tempLocs = new CameraLocationInformation[cameraLocations.Count];
        cameraLocations.CopyTo(tempLocs);
        cam.GetComponent<RotationMaster>().enabled = false;
        cam.SetParent(null);
        StartCoroutine(CameraMovement(tempLocs));
    }

    IEnumerator CameraMovement(CameraLocationInformation[] locs)
    {
        int i = 0;
        CameraLocationInformation current = null;
        Vector3 velocityref = Vector3.zero;
        GameManager.manager.PlayerCanMove = false;
        while(i <= locs.Length)  //Part 1: Lerp to locations
        {
            if(!isLerping && !isStalling)
            {
                if (i >= locs.Length) break;
                Debug.Log("Switching Location");
                current = locs[i];
                isLerping = true;
                isStalling = false;
                i++;
                yield return null;
            }
            else if(!isStalling && isLerping)
            {
                Debug.Log("Lerping");
                cam.position = Vector3.SmoothDamp(cam.position, current.transform.position,ref velocityref, current.lerpTime);
                cam.rotation = Quaternion.Slerp(cam.rotation, current.transform.rotation, Time.deltaTime);
                if (Vector3.Distance(cam.position,current.transform.position) < epsilon)
                {
                    cam.position = current.transform.position;
                    isLerping = false;
                    isStalling = true;
                }
                yield return null; 
            }
            else if(!isLerping && isStalling)
            {
                Debug.Log("Stalling");
                yield return new WaitForSeconds(current.stallTime);
                isStalling = false;
            }
            else if(isLerping && isStalling)
            {
                Debug.LogError("Somehow the camera is both lerping and stalling. This is bad.");
                break;
            }
        }
        while(Vector3.Distance(cam.position,camPriorParent.position) > epsilon) //Part 2: Lerp back to player.
        {
            cam.position = Vector3.Lerp(cam.position, camPriorParent.position, 0.5f);
            yield return null;
        }

        cam.SetParent(camPriorParent); //Part 3: Reconnect camera to player and finish.
        cam.GetComponent<RotationMaster>().enabled = true;
        cam.localPosition = realigner;
        Debug.Log(cam.position);
        cam.localRotation = rotationRealigner;
        completed = true;
        SetFlag();
        GameManager.manager.PlayerCanMove = true;
    }

}
