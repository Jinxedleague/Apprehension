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
    [SerializeField] private GameObject scoreKeeper;

    //private GameObject[] activeRelics;
    private List<GameObject> activeRelics = new List<GameObject> ();

    private GameObject carriedRelic;
    private bool searching, relicFound, carryingRelic;
    private float destinationDistance;

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
        carryingRelic = false;
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
            carryingRelic = true;
        }

        if (other.tag == "Relic-DropOff")
        {
            carriedRelic.transform.parent = null;
            activeRelics.Remove(carriedRelic);
            Destroy(carriedRelic.gameObject);
            searching = true;
            relicFound = false;
            carryingRelic = false;
            Search();
            statusUI.GetComponent<StatusIcons>().UpdateStatus(challengerNumber, 1);
            scoreKeeper.GetComponent<ScoreKeeper>().RelicDelivered();
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

    public void Caught()
    {
        statusUI.GetComponent<StatusIcons>().UpdateStatus(challengerNumber, 4);
        if (carryingRelic == true)
        {
            carriedRelic.tag = "Relic";
            carriedRelic.transform.parent = null;
            carriedRelic.GetComponent<Collider>().enabled = true;
            carriedRelic.GetComponent<Rigidbody>().useGravity = true;
            carryingRelic = false;
        }
        challengerAgent.enabled = false;
        this.gameObject.transform.position = new Vector3(0f, -17f, 0f);
    }
}
