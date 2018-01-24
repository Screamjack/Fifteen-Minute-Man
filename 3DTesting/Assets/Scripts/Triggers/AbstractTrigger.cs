using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractTrigger : MonoBehaviour {

    protected bool completed;
    /// <summary>
    /// Has the trigger been completed?
    /// </summary>
    public bool Completed
    {
        get { return completed; }
    }

    protected bool activated;
    /// <summary>
    /// Is the trigger currently running? Useful for triggers that run over time as opposed to instant triggers.
    /// </summary>
    public bool TriggerActive
    {
        get { return activated; }
    }

    [SerializeField]
    protected List<AbstractTrigger> preReqs;
    public List<AbstractTrigger> Requirements
    {
        get { return preReqs; }
    }

    /// <summary>
    /// The actual method for applying the trigger.
    /// </summary>
    void ActivateTrigger()
    {
        Debug.LogError("There is no default ActivateTrigger method. Please override it.");
        return;
    }

    /// <summary>
    /// A means of checking whether or not the trigger can be activated. 
    /// </summary>
    /// <returns>True if the trigger can be activated</returns>
    bool CheckTrigger()
    {
        Debug.LogError("There is no default CheckTrigger method. Please override it.");
        return false;
    }
}
