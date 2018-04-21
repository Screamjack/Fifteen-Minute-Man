using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOpeningManager : MonoBehaviour {

    [SerializeField]
    GameObject[] scenariosets;

void Awake()
    {
        string scenario = GameManager.manager.Scenario;
        GameManager.manager.TickTheClock = true;

        GameManager.manager.RecollectInformation();

        Debug.Log("scenario " + scenario);
        Debug.Log(GameObject.Find(scenario));

    }

}
