using UnityEngine;
using System.Collections;

public class CoinRainSoundScript : MonoBehaviour {

    public AudioSource bling;

    void Start()
    {
        bling = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void OnTriggerEnter () {
        bling.Play();
	}
}
