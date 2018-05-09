using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

public class SceneOpeningManager : MonoBehaviour
{

    [System.Serializable]
    public class triggerDictionary : SerializableDictionaryBase<string, GameObject> { }

    [SerializeField]
    List<GameObject> scenariosets;

    [SerializeField]
    triggerDictionary dict;

    void Start()
    {
        string scenario = GameManager.manager.Scenario;
        Transform player = GameObject.Find("Character").transform;
        Debug.Log(player);
        DoorMaster.Master.AdjustPlayer(ref player);
        GameManager.manager.TickTheClock = true;

        Debug.Log("scenario " + scenario);
        GameObject scenarioObject = scenariosets.Find(x => x.name.ToLower() == scenario);
        Debug.Log(scenarioObject);
        scenarioObject.SetActive(true);

        GameObject g;
        dict.TryGetValue(scenario, out g);
        if(g != null)
        {
            AbstractTrigger a = g.GetComponent<AbstractTrigger>();
            if(a != null)
            {
                if(a.GetType() == typeof(CameraTrigger))
                {
                    CameraTrigger c = a as CameraTrigger;
                    c.forceInit();
                }
                a.ActivateTrigger();
            }
        }

    }

}
