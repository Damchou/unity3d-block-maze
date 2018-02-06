using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    private Vector3 originalPosition;
    private Vector3 temp = new Vector3();
    private float speed = 0.05f;
    private float amplitude = 0.03f;
    private float frequency = 1f;

    void Start()
    {
        originalPosition = transform.position;
    }

	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(100, 0, 0) * Time.deltaTime);
        temp = originalPosition;
        temp.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = temp;
	}
}
