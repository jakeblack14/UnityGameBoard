using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using GameCore;

public class NetworkMovements : MonoBehaviour {
    Move move;
    Player player = new Player();
    // Use this for initialization
    void Start() {
        PhotonView photonView = PhotonView.Get(this);
        //example of chat message call
        //photonView.RPC("ChatMessage", PhotonTargets.All, "jup", "and jup!");
        move = new Move();
    }
    void Awake()
    {
        // Use this function for initializing variables or game state, called once during the lifetime of the script
        // This is called before Start
        // You can use start to pass any information back and forth when it iniatlizes
        // Will probably move starts stuff into this after more research
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
    //can maybe use this as the event that sends the move? Keep thinking on this
    private void OnMouseEnter()
    {
        
    }
    //Should send the move to everyone on the network, need to look and see if it sends it to itself also
    //it is possible to change it if it does
    [PunRPC]
    public void sendMove()
    {
        //Need more here like some way to connect to the gameboard 
        // movePiece(player.Identity, move);
    }
}
