using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {

    private float FloatStrength;
    private Vector3 direction;
    public float RandomRotationStrength;

    // Use this for initialization
    void Start () {
        FloatStrength = Random.Range(-1.0f, 1.0f);
        direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

    }

    // Update is called once per frame
    void Update () {
        transform.Rotate(RandomRotationStrength, RandomRotationStrength, RandomRotationStrength);
        transform.position += direction * FloatStrength * Time.deltaTime;

    }
}
