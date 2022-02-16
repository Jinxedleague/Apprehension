using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();    //Connect to the Photon server
        Debug.Log("Connecting to Server");
    }

    public override void OnConnectedToMaster()   //Function that is called when a connection is made
    {
        Debug.Log("We are connected");
        PhotonNetwork.JoinLobby();               //On connection, join lobby
    }

    public override void OnJoinedLobby()         //Once the connection is made to the lobby, perform inner code
    {
        SceneManager.LoadScene("LobbySelection");
    }
}
