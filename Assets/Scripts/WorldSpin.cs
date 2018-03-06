using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpin : MonoBehaviour {

    // Use this for initialization
    int spinx = 0;
    int spiny = 1;
    int spinz = 0;

    float Spin;

    void Start()
    {
        Spin = Random.Range(0.01f, 0.07f);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(spinx, spiny, spinz);

        transform.Rotate(Vector3.up * Spin, Space.Self);
    }
}
