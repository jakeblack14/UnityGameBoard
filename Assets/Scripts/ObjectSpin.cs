using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpin : MonoBehaviour {

    public float spin = 0.1f;

	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up * spin, Space.Self);
	}
}
