using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : AbstractTrigger{

    [SerializeField]
    Transform block;
    [SerializeField]
    float time = 1f;


    public override void ActivateTrigger()
    {
        if (CheckTrigger() && !completed)
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
        SetFlag();
    }


}
