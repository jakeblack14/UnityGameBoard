using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpin : MonoBehaviour {

    // Use this for initialization
    //float spinx = -0.1f;
    //float spiny = 0.1f;
    //float spinz = 0.1f;
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(spinx, spiny, spinz);

        transform.Rotate(Vector3.up * 0.06f, Space.Self);
	}
}
