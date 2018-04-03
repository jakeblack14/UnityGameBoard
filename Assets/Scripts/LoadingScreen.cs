using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

    public Image Rocket;
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Rocket.transform.position += (Vector3.right * speed) * Time.deltaTime;

        if (Rocket.transform.position.x > -0.20f)
        {   
            Rocket.transform.position = new Vector3(-0.60f, Rocket.transform.position.y, Rocket.transform.position.z);
        }
    }
}
