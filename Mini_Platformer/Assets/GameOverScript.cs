using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameOverScript : MonoBehaviour {

    public float restartDelay = 4f;         // Time to wait before restarting the level
    float restartTimer;                     // Timer to count up to restarting the level
    public Transform player;
    private float cheatTimer;
    private string winCheat = "";
    private string iWin = "yolohansolo";
    private int whyWontYouPlayThisGameNormally = 0;

    Animator anim;
    GameObject[] enemies;


    //--liitetyt muuttujat
    public Camera mainCamera;
    public Camera winCamera;
    public AudioSource audioEnter;
    public AudioSource audioWin;
    public AudioSource audioDie;
    float endTimer;
    List<GameObject> coins = new List<GameObject>();
    ArrayList temp = new ArrayList();     // määrittääkseen, että mitkä kolikot on jo aktivoitu
    int count;
    int random;
    GameObject trigger;

    // Use this for initialization
    void Awake () {
        anim = GetComponent<Animator>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        coins.AddRange(GameObject.FindGameObjectsWithTag("GravityCoin"));


        //-liitetty scripti
        mainCamera.gameObject.SetActive(true);
        winCamera.gameObject.SetActive(false);
        trigger = GameObject.Find("WinTrigger");
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        cheatTimer += Time.deltaTime;
        if (cheatTimer <= 20 && winCheat.Length < 11)
        {
            string dunno = "";
            dunno = Input.inputString;
            if (dunno.Contains(iWin[whyWontYouPlayThisGameNormally]))
            {
                winCheat = winCheat + dunno.ToString();
                whyWontYouPlayThisGameNormally++;
                Debug.Log(winCheat);
                audioDie.Play();
            }
            if (winCheat == iWin)
                trigger.GetComponent<WinTriggerBlock>().isTriggered = true;
        }
        

        // When player is too close to a enemy
        foreach (GameObject enemy in enemies)
        {
            if(enemy.GetComponent<ChaseAI>().isDead)
            {
                anim.SetTrigger("GameOver");
                if (!audioDie.isPlaying)
                    audioDie.Play();
                restartTimer += Time.deltaTime;
                if (restartTimer >= restartDelay)
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
            
        }

        //-liitetty scripti
        if (trigger.GetComponent<WinTriggerBlock>().isTriggered)
        {
            if(endTimer <= 0)
            {
                count = player.GetComponent<CubeController>().count;
                audioEnter.Play();
            }

                anim.SetTrigger("GameOverStateTrigger");
                endTimer += Time.deltaTime;

            if (endTimer >= 2.2 && !audioWin.isPlaying)
            {
                winCamera.gameObject.SetActive(true);
                mainCamera.gameObject.SetActive(false);
                audioWin.Play();
            }


            if (endTimer >= 4)
            {
                if (winCheat == "yolohansolo")
                    count = 100;
                for (int i = 0; i < coins.Count-count; i++)
                {
                    coins[i].gameObject.SetActive(false);
                }

                for (int i = 0; i < count; i++)
                {
                    coins[coins.Count - (1+i)].GetComponent<Rigidbody>().useGravity = true;
                }
            }

            if (endTimer >= 14.5)
            {
                Debug.Log("Done'd");
                Application.Quit();
            }
        }
    }
}
