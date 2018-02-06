using UnityEngine;
using System.Collections;

public class BackgroundMusicController : MonoBehaviour {

    public AudioSource audio1;
    public AudioSource audio2;
    public AudioClip audioNormal, audioDanger;
    GameObject[] enemies;
    int checker;
    GameObject trigger;

	// Use this for initialization
	void Start () {
        audio1.Play();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        trigger = GameObject.Find("WinTrigger");
    }
	
	// Update is called once per frame
	void Update () {

        checker = 0;
        foreach(GameObject enemy in enemies)
        {
            if (enemy.GetComponent<ChaseAI>().state == "pursuing")
                checker = 1;
        }

        if (trigger.GetComponent<WinTriggerBlock>().isTriggered)
            checker = 2;

        if (checker == 1)
        {
            if(audio1.isPlaying)
            {
                audio1.Stop();
                audio2.Play();
            }
        }
        if(checker == 0)
        {
            if (audio2.isPlaying)
            {
                audio2.Stop();
                audio1.Play();
            }
        }
        if(checker == 2)
        {
            if (audio1.isPlaying)
            {
                Debug.Log("bgm stopped");
                audio1.Stop();
            }
            
        }
	}
}
