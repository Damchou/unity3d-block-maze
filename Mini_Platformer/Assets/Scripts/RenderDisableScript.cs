using UnityEngine;
using System.Collections;

public class RenderDisableScript : MonoBehaviour {

    public string tag;

	// Use this for initialization
	void Start () {

        GameObject[] rendies = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < rendies.Length; i++)
            rendies[i].GetComponent<Renderer>().enabled = false;
    }
}
