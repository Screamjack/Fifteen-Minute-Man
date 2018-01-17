using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMaster : MonoBehaviour {
    public float sensitivity = 3f;
    public float maxRange = 79;
    public float minRange = 281;

    private float xRot, yRot;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        if (Cursor.lockState == CursorLockMode.Locked && Input.GetMouseButton(1))
        {
            Quaternion currentRot = transform.localRotation;
            xRot = currentRot.eulerAngles.y + (Input.GetAxis("Mouse X") * sensitivity);
            yRot = currentRot.eulerAngles.x + (Input.GetAxis("Mouse Y") * sensitivity);

            if(yRot < minRange && yRot > maxRange + 10) //>270 and >99. Assume clamping lower value.
            {
                yRot = minRange;
            }
            else if(yRot < minRange && yRot < maxRange) //>270 and >99. Assume clamping upper value
            {
                yRot = maxRange;
            }
            transform.localRotation = Quaternion.Euler(yRot, xRot, 0);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void OnApplicationFocus(bool focus)
    {
        return;
        if(focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }


}
