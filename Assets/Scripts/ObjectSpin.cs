﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpin : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up * 0.1f, Space.Self);
	}
}
