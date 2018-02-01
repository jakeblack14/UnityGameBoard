using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpin : MonoBehaviour {

    // Use this for initialization
    int spinx = -1;
    int spiny = 1;
    int spinz = 1;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(spinx, spiny, spinz);
	}
}
