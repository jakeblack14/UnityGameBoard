using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NetworkMovements : MonoBehaviour {

    // Use this for initialization
    void Start() {
        PhotonView photonView = PhotonView.Get(this);
        //example of chat message call
        //photonView.RPC("ChatMessage", PhotonTargets.All, "jup", "and jup!");
    }

    // Update is called once per frame
    void Update() {

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
    //So to call a function that is marked like below as PunRPC you have to initialize a PhotonView
    [PunRPC]
    void ChatMessage(string a, string b)
    {
        Debug.Log(string.Format("Chat message ", a, b));
    }
}
