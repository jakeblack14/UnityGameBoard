using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

    public Image Rocket;

    private float rocketPositionX;
    private float rocketPositionY;
    private float rocketPositionZ;

	// Use this for initialization
	void Start () {
        rocketPositionX = Rocket.transform.parent.gameObject.transform.position.x;
        rocketPositionY = Rocket.transform.parent.gameObject.transform.position.y;
        rocketPositionZ = Rocket.transform.parent.gameObject.transform.position.z;

        rocketPositionX -= 270;
        rocketPositionY -= 60;

        Rocket.transform.localPosition = new Vector3(rocketPositionX, rocketPositionY, rocketPositionZ);
    }
	
	// Update is called once per frame
	void Update () {

        if(rocketPositionX < 265)
        {
            rocketPositionX += 3;
            Rocket.transform.localPosition = new Vector3(rocketPositionX, rocketPositionY, rocketPositionZ);
        }
        else
        {
            rocketPositionX = Rocket.transform.parent.gameObject.transform.position.x - 270;
        }

        Rocket.transform.localPosition = new Vector3(rocketPositionX, rocketPositionY, rocketPositionZ);
    }
}
