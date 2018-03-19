using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpin : MonoBehaviour {

    private float Spin;

    void Start()
    {
        Spin = Random.Range(0.01f, 0.07f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Spin, Space.Self);
    }
}
