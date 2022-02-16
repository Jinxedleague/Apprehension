using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 playerSpawn = new Vector3(-160f, 22f, -175f);
        PhotonNetwork.Instantiate(player.name, playerSpawn, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
