﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour {
    private bool state;
    public AudioSource audioClip;
    public float DestroyTime = (5.0f);
    public GameObject Target;
    public float ScareDestroyTime = (5.0f);
   
	// Use this for initialization
	void Start () {
    	
    }
    // Update is called once per frame
    void Update () {
		
	}
    private void OnTriggerEnter(Collider coll)
       
    {
        if (coll.tag == "Player")
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            Debug.Log("player Contaceted");
            Destroy(gameObject, DestroyTime);
        }
        if (coll.tag == "Player")
        {
            Target.SetActive(true);
            print("ON");
            Destroy(Target, ScareDestroyTime);
        }
       
    }
}
