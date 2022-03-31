using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSnare : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetTrigger("ActivateSnare");

            //For dropping relic
            //if (other.GetComponent<PlayerAbilities>().relicCollected == true)
            //{
            //    Debug.Log("Test");
            //    other.GetComponent<PlayerAbilities>().DropRelic();
            //}

           // other.gameObject.transform.position = this.transform.position;
        }
    }
}
