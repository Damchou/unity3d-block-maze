using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    private Vector3 originalPosition;
    private Vector3 temp = new Vector3();
    public float speed = 0.05f;
    public float amplitude = 0.03f;
    public float frequency = 1f;
    public bool moveX = false;
    public bool moveY = false;
    public bool moveZ = false;


    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        temp = originalPosition;
        if(moveY)
            temp.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        if (moveX)
            temp.x += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        if (moveZ)
            temp.z += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = temp;
    }
}
