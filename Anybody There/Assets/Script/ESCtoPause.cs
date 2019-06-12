using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCtoPause : MonoBehaviour {

    
    // Use this for initialization
    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            Application.Quit();
            Debug.Log("Application Quit");
        }
    }
}
