using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This will send a signal to Wwise everytime the Animation event happens
public class Footsteps : MonoBehaviour {
    //TODO: Account for various types of surfaces
    public void doStep() {
        AkSoundEngine.PostEvent("footstep", gameObject);
    }
	
}
