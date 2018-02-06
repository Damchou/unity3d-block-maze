using UnityEngine;
using System.Collections;

public class ButtonTest : MonoBehaviour {

    public GameObject disableTarget;
    public GameObject EnableTarget;
    public float textTimer = 10f;

    private bool isTriggered = false;
    private bool showGUI = false;
    AudioSource audio;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && !isTriggered)
        {
            Debug.Log("DONE");
            isTriggered = true;

            //Disables the old "drawbridge" and enables new one
            disableTarget.SetActive(false);
            EnableTarget.SetActive(true);

            audio.Play();

            Vector3 temp = new Vector3(0, 0.15f, 0);
            transform.position -= temp;
        }
    }

    void OnGUI()
    {
        if(showGUI)
            GUI.Box(new Rect(200, 100, 150, 30), "Something changed...");
    }

    void Start()
    {
        audio = GetComponent<AudioSource>();
        disableTarget.SetActive(true);
        EnableTarget.SetActive(false);
    }

    void Update()
    {
        if (isTriggered && textTimer > 0)
        {
            showGUI = true;
            textTimer -= Time.deltaTime;
        } else
            showGUI = false;

    }
}
