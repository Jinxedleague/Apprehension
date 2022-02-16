using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private GameObject playerHUD, playerCamera;

    private bool relicCollected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "Relic")
            {
                if (Input.GetKey(KeyCode.F))
                {
                    hit.collider.gameObject.SetActive(false);
                    relicCollected = true;
                    Debug.Log("Relic collected");
                }
            }

            if (hit.collider.gameObject.tag == "Relic-DropOff")
            {
                if (Input.GetKey(KeyCode.F) && relicCollected == true)
                {
                    relicCollected = false;
                    Debug.Log("Relic dropped off");
                }
            }
        }
    }
}
