using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestruction : MonoBehaviour {

    public GameObject explosion;

    void OnTriggerEnter (Collider other)
    {
        if(other.tag == "NewAsteroid" && other.tag != "NotExplosive")
        {
            Debug.Log(other.name);

            Instantiate(explosion, other.transform.position, other.transform.rotation);

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
