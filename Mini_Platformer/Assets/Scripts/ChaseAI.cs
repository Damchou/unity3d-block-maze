using UnityEngine;
using System.Collections;

public class ChaseAI : MonoBehaviour {

    public Transform player;

    public string state = "patrol";
    public GameObject[] waypoints;
    public int currentWP = 0;
    public float rotSpeed = 0.2f;
    public float speed = 1.5f;
    public float accuracyWP = 0.5f;
    public AudioSource audio;
    public bool audioPlaying;
    public UnityEngine.AI.NavMeshAgent agent;
    public bool isDead = false;

    public string State
    {
        get { return this.state; }
        set { state = value;  }
    }

    public bool IsDead
    {
        get { return this.isDead; }
        set { isDead = value; }
    }

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        Ray sightRay = new Ray(transform.position, Vector3.forward);


        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, this.transform.forward);

        // Bot patrolling
        if(state =="patrol" && waypoints.Length > 0)
        {
            if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
            {
                currentWP++;
                if (currentWP >= waypoints.Length)
                    currentWP = 0;
            }

            //rotate toward waypoint
            direction = waypoints[currentWP].transform.position - transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            agent.destination = waypoints[currentWP].transform.position;
        }

        RaycastHit hit;

        if ((Vector3.Distance(player.position, this.transform.position) < 3 && (angle < 30 || state == "pursuing") && player.position.y < 1) || Vector3.Distance(player.position, this.transform.position) < 1)
        {
            if (Physics.Raycast(transform.position + new Vector3(0, 0, 0), (player.transform.position + new Vector3(0, 0, 0)) - transform.position, out hit) && hit.collider.name == "Player" || state =="pursuing")
            {
                state = "pursuing";
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                if (direction.magnitude > 0.2f)
                    agent.destination = player.position;
                if (!audioPlaying)
                {
                    audio.Play();
                    audioPlaying = true;
                }
            }
            
        }
        else
        {
            state = "patrol";
            audio.Stop();
            audioPlaying = false;
        }

        // When close to player, give GameOverScript isDead=true to initiate game over sequence
        if ((Vector3.Distance(player.position, this.transform.position) < 0.5))
            isDead = true;
            
	}
}