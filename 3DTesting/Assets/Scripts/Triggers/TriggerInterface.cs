using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TriggerInterface {

    /// <summary>
    /// Has this trigger been completed? 
    /// </summary>
    bool completed
    {
        get;
    }

    /// <summary>
    /// The actual method for applying the trigger.
    /// </summary>
    void ActivateTrigger();

    /// <summary>
    /// A means of checking whether or not the trigger can be activated. 
    /// </summary>
    /// <returns>True if the trigger can be activated</returns>
    bool CheckTrigger(); 


}
