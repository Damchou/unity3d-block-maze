using UnityEngine;
using System.Collections;

public class DrawbridgeScript : MonoBehaviour {

    public Rigidbody target;
    public Vector3 targetAngle = new Vector3(0f, 0f, 0f);
    public float textTimer = 10;

    private Vector3 currentAngle;
    private bool isTriggered = false;
    private bool showGUI = false;
    AudioSource audio;

    void OnTriggetEnter(Collider col)
    {
        Debug.Log("Triggered with player");
        if(col.gameObject.tag == "Player")
        {
            isTriggered = true;
            Debug.Log("Rotating!");
            currentAngle = new Vector3(
                Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime),
                Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime),
                Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime));

            target.transform.eulerAngles = currentAngle;
            audio.Play();
            showGUI = true;
        }
    }

    void OnGUI()
    {
        if(showGUI)
        {
            GUI.Box(new Rect(100, 100, 200, 200), "The Drawbridge is lowered");
        }
    }

	// Use this for initialization
	void Start () {
        currentAngle = transform.eulerAngles;
        audio = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update() {

        if (isTriggered && textTimer > 0)
            textTimer -= Time.deltaTime;
        else
            showGUI = false;
    }
}
