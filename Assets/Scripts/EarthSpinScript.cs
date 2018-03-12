using UnityEngine;
using System.Collections;

public class EarthSpinScript : MonoBehaviour {
    public float speed = 10f;
    Vector3 Rotation = new Vector3();


    void Start()
    {
        Rotation.x = Random.Range(0.0f, speed);
        Rotation.y = Random.Range(0.0f, speed);
        Rotation.z = Random.Range(0.0f, speed);
    }

    void Update() {
        transform.Rotate(Rotation * Time.deltaTime);
    }
}