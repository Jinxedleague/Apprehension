using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Challenger : MonoBehaviour
{
    [SerializeField] private int challengerNumber;
    [SerializeField] private Transform[] searchAreas;
    [SerializeField] private NavMeshAgent challengerAgent;
    [SerializeField] private GameObject challenger, challengerHand, dropOff;
    [SerializeField] private GameObject statusUI;
    [SerializeField] private GameObject player;

    //private GameObject[] activeRelics;
    private List<GameObject> activeRelics = new List<GameObject> ();

    private GameObject carriedRelic;
    private bool searching, relicFound, canSeePlayer;
    private float destinationDistance;






    //////////////////////////////////
    public float radius;
    public float angle;
    public GameObject familiar;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeeThreat;
    /////////////////////////////////











    // Start is called before the first frame update
    void Start()
    {
        //activeRelics = GameObject.FindGameObjectsWithTag("Relic");
        foreach (GameObject relics in GameObject.FindGameObjectsWithTag("Relic"))
        {
            activeRelics.Add(relics);
        }

        Search();
        searching = true;
        relicFound = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRelicRange();
        CheckDestinationRange();

        float mmDistance = Vector3.Distance(transform.position, player.transform.position);
        if (mmDistance < 10)
        {
            statusUI.GetComponent<StatusIcons>().UpdateStatus(challengerNumber, 3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Relic")
        {
            PickUpRelic(other.gameObject);
        }

        if (other.tag == "Relic-DropOff")
        {
            carriedRelic.transform.parent = null;
            activeRelics.Remove(carriedRelic);
            Destroy(carriedRelic.gameObject);
            searching = true;
            relicFound = false;
            Search();
            statusUI.GetComponent<StatusIcons>().UpdateStatus(challengerNumber, 1);
        }
    }

    private void Search()
    {
        challengerAgent.destination = searchAreas[Random.Range(0, searchAreas.Length)].position;
    }

    private void CheckRelicRange()
    {
        foreach (GameObject relic in GameObject.FindGameObjectsWithTag("Relic"))
        {
            float distance = Vector3.Distance(relic.transform.position, challenger.transform.position);
            if (distance <= 150 && searching == true)
            {
                challengerAgent.destination = relic.transform.position;
                searching = false;
                relicFound = true;
            }
        }
    }

    private void CheckDestinationRange()
    {
        destinationDistance = Vector3.Distance(transform.position, challengerAgent.destination);
        if (destinationDistance <= 2 && searching == true && relicFound == false)
        {
            Search();
        }
    }

    private void PickUpRelic(GameObject foundRelic)
    {
        carriedRelic = foundRelic;
        carriedRelic.tag = "CarriedRelic";
        carriedRelic.transform.position = challengerHand.transform.position;
        carriedRelic.transform.parent = challengerHand.transform;
        carriedRelic.GetComponent<Collider>().enabled = false;
        carriedRelic.GetComponent<Rigidbody>().useGravity = false;
        searching = false;
        DeliverRelic();
        statusUI.GetComponent<StatusIcons>().UpdateStatus(challengerNumber, 2);
    }

    private void DeliverRelic()
    {
        challengerAgent.destination = dropOff.transform.position;
    }








    //Needs to be updated
    ///////////////////////////////////////////////////
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        //Range check to search for player within radius around enemy
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        //React to target within range
        if (rangeChecks.Length != 0)
        {
            //Get transform of item within range (first instance in array of items)
            Transform target = rangeChecks[0].transform;

            //Direction from where enemy is facing to player position dictated by range check
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                //if target is within angle of view within range check, identify distance
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                //fires raycast to target while honoring obstructions
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeeThreat = true;
                }
                else
                {
                    canSeeThreat = false;
                }
            }
            else
            {
                //player is outside cone range
                canSeeThreat = false;
            }
        }
        else
        {
            if (canSeePlayer)
            {
                canSeeThreat = false;
            }
        }
    }
    ///////////////////////////////////////////////////

}
