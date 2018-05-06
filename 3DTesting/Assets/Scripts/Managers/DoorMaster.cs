using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMaster : MonoBehaviour {

    static DoorMaster master;
    public static DoorMaster Master
    {
        get { return master; }
    }

    [SerializeField]
    List<Vector3> locations;
    [SerializeField]
    List<int> angles;

    private int lastDoorIndex;

    void Awake()
    {
        if (master == null)
            master = this;
        if (master != this)
            Destroy(gameObject);
        DontDestroyOnLoad(master);
    }

    public void SetLDI(int index)
    {
        lastDoorIndex = index;
    }

    public void AdjustPlayer(ref Transform player)
    {
        player.position = locations[lastDoorIndex];
        player.eulerAngles = new Vector3(0, angles[lastDoorIndex], 0);
    }
}
