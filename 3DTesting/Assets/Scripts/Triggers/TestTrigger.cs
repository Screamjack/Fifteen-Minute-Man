using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : AbstractTrigger{

    [SerializeField]
    Transform block;
    [SerializeField]
    float time = 1f;

    public override bool CheckTrigger()
    {
        if (preReqs.Count == 0) return true;
        bool retVal = true;
        foreach(AbstractTrigger t in preReqs)
        {
            if (!t.Completed)
                retVal = false;
        }
        Debug.Log("Prereqs Done? " + retVal);
        return retVal;
    }

    public override void ActivateTrigger()
    {
        if (CheckTrigger())
        {
            activated = true;
            StartCoroutine(LiftBlock(time));
        }
    }

    public void ForceTrigger() //For debug only
    {
        activated = true;
        StartCoroutine(LiftBlock(time));
    }

    IEnumerator LiftBlock(float duration)
    {
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            block.transform.position += Vector3.up * 0.01f;
            yield return null;
        }
        completed = true;
    }


}
