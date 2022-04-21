using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera, throwSource;
    [SerializeField] private Image crosshair;
    [SerializeField] private float throwForce;
    [SerializeField] private int challengersRemaining; //Crude win con int
    [SerializeField] private RawImage compassSprite;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject relicIconCompassImage;
    private GameObject heldRelic;
    public bool relicCollected = false;

    List<RelicTracker> relicLocations = new List<RelicTracker>();
    private float compassUnit, relicMaxDistance;
    private RelicTracker challenger1, challenger2, challenger3, challenger4;

    private float senseDuration = 5f;
    private float senseCooldown = 60f;
    private bool senseReady = true;
    private bool senseActive = false;

    // Start is called before the first frame update
    void Start()
    {
        challenger1 = GameObject.Find("Challenger1").GetComponent<RelicTracker>();
        challenger2 = GameObject.Find("Challenger2").GetComponent<RelicTracker>();
        challenger3 = GameObject.Find("Challenger3").GetComponent<RelicTracker>();
        challenger4 = GameObject.Find("Challenger4").GetComponent<RelicTracker>();

        compassUnit = compassSprite.rectTransform.rect.width / 360f;
        relicMaxDistance = 175f;
        UpdatePlayerLocation(challenger1);
        UpdatePlayerLocation(challenger2);
        UpdatePlayerLocation(challenger3);
        UpdatePlayerLocation(challenger4);

        challengersRemaining = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(challengersRemaining == 0)
        {
            SceneManager.LoadScene("Win");
        }

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity))   //Creates a new raycast starting at the player's camera and projecting forward 3 units
        {

             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Relic Pickups
            if (hit.collider.gameObject.tag == "Relic")                                                     //The following is action code for when the player picks up a relic
            {
                crosshair.rectTransform.sizeDelta = new Vector2(12f, 12f);                                  //Change crosshair to 12f x 12f
                crosshair.color = new Color(1f, 1f, 1f, 1f);                                                //And it's color to white w/ no transparency

                if (Input.GetKey(KeyCode.F) && relicCollected == false)                                     //If the player presses "F" while the raycast is on the object with tag "Relic", and relicCollected is false
                {
                    heldRelic = hit.collider.gameObject;
                    heldRelic.GetComponent<Collider>().enabled = false;
                    heldRelic.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    heldRelic.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    heldRelic.GetComponent<Rigidbody>().useGravity = false;
                    heldRelic.transform.position = throwSource.transform.position;
                    heldRelic.transform.rotation = throwSource.transform.rotation;
                    heldRelic.transform.parent = throwSource.transform;              //NOTE!!!! FIGURE OUT RPC FOR PARENTING! I THINK THIS IS ONE THING THAT IS MAKING IT ALL FUCKY!
                    relicCollected = true;
                }
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Button Interaction
            if (hit.collider.gameObject.tag == "Button")                                                  //If the raycast is over a button, do the following
            {
                crosshair.rectTransform.sizeDelta = new Vector2(12f, 12f);                                //Resize and color the crosshair
                crosshair.color = new Color(1f, 1f, 1f, 1f);

                if (Input.GetKey(KeyCode.F))
                {
                    hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();                      //If "F" is pressed while looking at the button, invoke the onClick function on the Button component
                }
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Individual Wall Section Triggers
            if (hit.collider.gameObject.tag == "Trigger")
            {
                crosshair.rectTransform.sizeDelta = new Vector2(12f, 12f);                                //Resize and color the crosshair
                crosshair.color = new Color(1f, 1f, 1f, 1f);

                hit.collider.gameObject.GetComponent<SectorWalls>().ActivateBeam();

                if (Input.GetKey(KeyCode.F))
                {
                    hit.collider.gameObject.GetComponent<SectorWalls>().ActivateWalls();                      //If "F" is pressed while looking at the button, invoke the onClick function on the Button component
                }
            }
        }

        if (hit.collider == null || hit.collider.gameObject.tag == "Untagged")                           //Resets the crosshair if it is not over an interactable item
        {
            crosshair.rectTransform.sizeDelta = new Vector2(7f, 7f);
            crosshair.color = new Color(1f, 1f, 1f, 0.5f);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///Relic Dropping
        if (Input.GetKey(KeyCode.G) && relicCollected == true)
        {
            DropRelic();
            //heldRelic.GetComponent<Collider>().enabled = true;
            //heldRelic.GetComponent<Rigidbody>().useGravity = true;
            //heldRelic.transform.position = throwSource.transform.position;
            //heldRelic.transform.parent = null;  //NOTE!!!! FIGURE OUT RPC FOR PARENTING! I THINK THIS IS ONE THING THAT IS MAKING IT ALL FUCKY!
            //relicCollected = false;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///Player Abilities
        if (Input.GetKey(KeyCode.E) && relicCollected == false && senseReady == true)
        {
            senseActive = true;
            senseReady = false;
        }

        if (senseActive == true)
        {
            Debug.Log("Sense");
            relicMaxDistance = 275f;
            senseDuration -= 1 * Time.deltaTime;

            if (senseDuration <= 0f)
            {
                senseActive = false;
                relicMaxDistance = 175f;
                senseDuration = 5f;
            }
        }

        if (senseReady == false && senseActive == false)
        {
            senseCooldown -= 1 * Time.deltaTime;

            if (senseCooldown <= 0f)
            {
                senseReady = true;
                senseCooldown = 60f;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///Compass Tracking
        ///Code-Tutorial courtesy of https://www.youtube.com/watch?v=MRAVwaGrmrk

        compassSprite.uvRect = new Rect(playerTransform.localEulerAngles.y / 360f, 0f, 1f, 1f);

        foreach (RelicTracker relic in relicLocations)
        {
            relic.relicImage.rectTransform.anchoredPosition = GetRelicPositionForCompass(relic);
            float distance = Vector2.Distance(new Vector2(playerTransform.position.x, playerTransform.position.z), relic.relicPosition);
            float scale = 0f;

            if (distance < relicMaxDistance)
            {
                scale = 1f - (distance / relicMaxDistance);
            }

            relic.relicImage.rectTransform.localScale = Vector3.one * scale;
        }
    }

    public void UpdatePlayerLocation(RelicTracker relic)
    {
        GameObject newRelic = Instantiate(relicIconCompassImage, compassSprite.transform);
        relic.relicImage = newRelic.GetComponent<Image>();
        relic.relicImage.sprite = relic.relicIcon;

        relicLocations.Add(relic);
    }

    Vector2 GetRelicPositionForCompass (RelicTracker relic)
    {
        Vector2 playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.z);
        Vector2 playerForwardVector = new Vector2(playerTransform.forward.x, playerTransform.forward.z);

        float angle = Vector2.SignedAngle(relic.relicPosition - playerPosition, playerForwardVector);

        return new Vector2(compassUnit * angle, 0f);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void DropRelic()
    {
        heldRelic.GetComponent<Collider>().enabled = true;
        heldRelic.GetComponent<Rigidbody>().useGravity = true;
        heldRelic.transform.position = throwSource.transform.position;
        heldRelic.transform.parent = null;  //NOTE!!!! FIGURE OUT RPC FOR PARENTING! I THINK THIS IS ONE THING THAT IS MAKING IT ALL FUCKY!
        relicCollected = false;
    }

    public void challengerDead()
    {
        challengersRemaining--;
    }
}
