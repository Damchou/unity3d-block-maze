using UnityEngine;
using System.Collections;

public class ColorChange : MonoBehaviour {

    public Renderer rend;
    GameObject[] enemies;
    bool checker;

    // Use this for initialization
    void Start () {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        rend = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {

        checker = false;
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<ChaseAI>().state == "pursuing")
                checker = true;
        }

        if (checker)
            rend.material.color = Color.red;
        else if (!checker)
            rend.material.color = Color.cyan;

    }
}
