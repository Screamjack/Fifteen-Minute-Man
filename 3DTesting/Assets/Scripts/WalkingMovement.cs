using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMovement : MonoBehaviour {

    [SerializeField]
    WalkPoint[] points;

    [SerializeField]
    float WalkSpeed = 1;

    [SerializeField]
    float epsilon = 0.05f;

    bool running;

    IEnumerator RunToPoint()
    {
        running = true;
        for(int i = 0; i < points.Length; i++)
        {
            WalkPoint w = points[i];
            while (Vector3.Distance(transform.position, w.ToPoint.position) > epsilon)
            {
                float step = WalkSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, w.ToPoint.position, step);
                Vector3 faceDirection = new Vector3(w.ToPoint.position.x, transform.position.y, w.ToPoint.position.z);
                transform.rotation = Quaternion.LookRotation(faceDirection - transform.position);
                yield return null;
            }
            Debug.Log("At the point");
        }

        running = false;
    }

    public void Activate()
    {
        if(!running)
            StartCoroutine(RunToPoint());
    }

    void Start()
    {
        Activate();
    }
}
