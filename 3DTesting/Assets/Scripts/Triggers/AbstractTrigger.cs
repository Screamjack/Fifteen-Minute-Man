using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTrigger : MonoBehaviour {

    [SerializeField]
    private bool collidable = false;

    protected bool completed;
    /// <summary>
    /// Has the trigger been completed?
    /// </summary>
    public bool Completed
    {
        get { return completed; }
    }

    protected bool activated;
    public bool Activated
    {
        get { return activated; }
    }
    /// <summary>
    /// Is the trigger currently running? Useful for triggers that run over time as opposed to instant triggers.
    /// </summary>
    public bool TriggerActive
    {
        get { return activated; }
    }

    [SerializeField]
    protected List<string> preReqs;
    [SerializeField]
    string triggerFlag;
    /// <summary>
    /// All the previous triggers that must be completed to allow for this trigger to activate.
    /// </summary>
    public List<string> Requirements
    {
        get { return preReqs; }
    }

    /// <summary>
    /// The actual method for applying the trigger.
    /// </summary>
    public virtual void ActivateTrigger()
    {
        Debug.LogError("There is no default ActivateTrigger method. Please override it.");
        return;
    }

    /// <summary>
    /// Used to assign a flag to GameManager for checking if the trigger is done.
    /// </summary>
    public virtual void SetFlag()
    {
        GameManager.manager.flags.Add(triggerFlag);
    }

    /// <summary>
    /// A means of checking whether or not the trigger can be activated. 
    /// </summary>
    /// <returns>True if the trigger can be activated</returns>
    public virtual bool CheckTrigger()
    {
        if (preReqs.Count == 0) return true;
        bool retVal = true;
        foreach(string s in preReqs)
        {
            if(!GameManager.manager.flags.Contains(s)) {
                retVal = false;
                Debug.Log("Failed to ensure trigger can run");
                break;
            }
        }
        return retVal;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" || !collidable) return;
        if (!activated && !completed)
        {
            ActivateTrigger();
        }
    }
}
