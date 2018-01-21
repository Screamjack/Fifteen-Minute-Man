using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour, TriggerInterface {

    [SerializeField]
    Transform block;

    bool triggerStarted = false;
    bool triggerDone = false;

    public bool completed
    {
        get { return triggerDone; }
    }

    [SerializeField]
    List<TriggerInterface> preReqs;

    public bool CheckTrigger()
    {
        bool retVal = true;
        foreach(TriggerInterface t in preReqs)
        {
            if (!t.completed)
                retVal = false;
        }
        return retVal;
    }

    public void ActivateTrigger()
    {
        triggerStarted = true;
        StartCoroutine(LiftBlock(1));
    }

    IEnumerator LiftBlock(float duration)
    {
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            block.transform.position += Vector3.up * 0.01f;
            yield return null;
        }
        triggerDone = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(!triggerStarted && !triggerDone)
        {
            ActivateTrigger();
        }
    }
}
