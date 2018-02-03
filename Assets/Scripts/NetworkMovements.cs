using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NetworkMovements : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //stream.SendNext() Send Moved Piece first location
            //stream.SendNext() Send Moved Piece New location
            //stream.SendNext() Send Turn Bool
        }
        else
        {
            // MovedPiece = stream.ReceiveNext();
            // NewLocation = stream.ReceiveNext();
            //Turn = stream.ReceiveNext();
        }

    }
}
