using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAsteroids : MonoBehaviour {

    public GameObject asteroid;
    private float waitTime;

	// Use this for initialization
	void Start () {
        waitTime = Random.Range(2, 5);

        StartCoroutine(wait());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator wait()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(asteroid, transform.position, transform.rotation);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
