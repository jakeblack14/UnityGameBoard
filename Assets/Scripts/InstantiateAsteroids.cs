using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAsteroids : MonoBehaviour {

    public GameObject asteroid;
    private float waitTime;

	// Use this for initialization
	void Start () {
        waitTime = Random.Range(5, 8);

        StartCoroutine(wait());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator wait()
    {

        while(true)
        //for (int i = 0; i < 10; i++)
        {
            //Debug.Log(asteroid);
            GameObject currentAsteroid = Instantiate(asteroid, transform.position, transform.rotation) as GameObject;
            currentAsteroid.transform.localScale = new Vector3(20, 20, 20);

            yield return new WaitForSeconds(waitTime);
        }
    }
}
