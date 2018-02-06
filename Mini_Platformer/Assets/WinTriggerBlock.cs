using UnityEngine;
using System.Collections;

public class WinTriggerBlock : MonoBehaviour {

    public bool isTriggered = false;
    public Collider player;

    void OnTriggerEnter(Collider col)
    {
        if(col == player && !isTriggered)
        isTriggered = true;
    }
}
