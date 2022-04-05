using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwap : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject player;
    public GameObject bat;
    public GameObject playerCam;
    public GameObject playerUI;

    private int characterNumber;

    // Start is called before the first frame update
    void Start()
    {
        bat.SetActive(false);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        characterNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            swap();
        }
    }

    public void swap()
    {
        if(characterNumber == 0)
        {
            bat.SetActive(true);
            bat.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z);
            playerController.enabled = false;
            playerCam.SetActive(false);
            playerUI.SetActive(false);
            characterNumber = 1;
        }
        else if(characterNumber == 1)
        {
            bat.SetActive(false);
            playerController.enabled = true;
            playerCam.SetActive(true);
            playerUI.SetActive(true);
            characterNumber = 0;
        }
    }
}
