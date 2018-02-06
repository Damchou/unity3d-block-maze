using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public float fpsTargetDistance;
    public float enemyLookDistance;
    public float attackDistance;
    public float enemyMovementSpeed;
    public float damping;
    public Transform fpsTarget;
    Rigidbody rb;
    Renderer myRender;

    void lookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(fpsTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }

    void attack()
    {
        rb.AddForce(transform.forward * enemyMovementSpeed);
    }

	// Use this for initialization
	void Start () {
        myRender = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        fpsTargetDistance = Vector3.Distance(fpsTarget.position, transform.position);
	    if(fpsTargetDistance < enemyLookDistance)
        {
            myRender.material.color = Color.yellow;
            lookAtPlayer();
            print("Look at the Player");
        }

        if (fpsTargetDistance < attackDistance)
        {
            myRender.material.color = Color.red;
            attack();
            print("ATTACK");
        }
        else
            myRender.material.color = Color.blue;
	}
}
