using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinScript : MonoBehaviour
{
    public GameObject player;
    public Camera mainCamera;
    public Camera winCamera;
    public AudioSource audioEnter;
    public AudioSource audioWin;
    float endTimer;
    Animator anim;
    GameObject[] coins;
    ArrayList temp = new ArrayList();     // määrittääkseen, että mitkä kolikot on jo aktivoitu
    int count;
    int random;
    GameObject trigger;

    void Awake()
    {
        anim = GetComponent<Animator>();
        //audioEnter = GetComponent<AudioSource>();
        mainCamera.gameObject.SetActive(true);
        winCamera.gameObject.SetActive(false);
        trigger = GameObject.Find("WinTrigger");
    }

    void Update()
    {
        if (trigger.GetComponent<WinTriggerBlock>().isTriggered)
        {
            if (endTimer <= 0)
            {
                Debug.Log("TRIGGERED");
                anim.SetTrigger("GameWin");
                Debug.Log("settriggered");
                endTimer += Time.deltaTime;
                Debug.Log("timer set");
                count = player.GetComponent<CubeController>().count;
                Debug.Log("count counted");
                audioEnter.Play();
                Debug.Log("audio play");
            }

            if (endTimer >= 2)
            {
                winCamera.gameObject.SetActive(true);
                mainCamera.gameObject.SetActive(false);
                Debug.Log("WINFANFARE!");
                audioWin.Play();
            }


            if (endTimer >= 4)
            {
                for (int i = 0; i < count; i++)
                {
                    random = Random.Range(0, coins.Length);
                    Debug.Log("Rolled " + random);
                    if (!temp.Contains(random))
                    {
                        coins[random].SetActive(true);
                        temp.Add(random);
                        Debug.Log(random + " was OK!");
                    }

                }
            }

            if (endTimer >= 15)
            {
                Debug.Log("Done'd");
                Application.Quit();
            }
        }
    }
}
