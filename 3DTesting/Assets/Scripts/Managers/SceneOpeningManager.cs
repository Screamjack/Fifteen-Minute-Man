using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOpeningManager : MonoBehaviour
{

    [SerializeField]
    List<GameObject> scenariosets;

    [SerializeField]
    AbstractTrigger openingTrigger;

    void Start()
    {
        string scenario = GameManager.manager.Scenario;
        GameManager.manager.TickTheClock = true;

        GameManager.manager.RecollectInformation();

        Debug.Log("scenario " + scenario);
        GameObject scenarioObject = scenariosets.Find(x => x.name.ToLower() == scenario);
        Debug.Log(scenarioObject);
        scenarioObject.SetActive(true);
        if(openingTrigger != null)
        {
            openingTrigger.ActivateTrigger();
        }

    }

}
