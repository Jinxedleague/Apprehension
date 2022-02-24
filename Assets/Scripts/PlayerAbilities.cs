using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera, relicModel, throwSource;
    [SerializeField] private Image crosshair;
    [SerializeField] private float throwForce;
    [SerializeField] private RawImage compassSprite;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject relicIconCompassImage;

    private GameObject heldRelic;

    private bool relicCollected = false;
    List<RelicTracker> relicLocations = new List<RelicTracker>();
    private float compassUnit, relicMaxDistance;
    public RelicTracker relic1, relic2, relic3, relic4;

    // Start is called before the first frame update
    void Start()
    {
        compassUnit = compassSprite.rectTransform.rect.width / 360f;
        relicMaxDistance = 200f;
        UpdateRelicLocation(relic1);
        UpdateRelicLocation(relic2);
        UpdateRelicLocation(relic3);
        UpdateRelicLocation(relic4);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "Relic")
            {
                crosshair.rectTransform.sizeDelta = new Vector2(12f, 12f);
                crosshair.color = new Color(1f, 1f, 1f, 1f);

                if (Input.GetKey(KeyCode.F) && relicCollected == false)
                {
                    //hit.collider.gameObject.SetActive(false);
                    //relicCollected = true;
                    heldRelic = hit.collider.gameObject;
                    heldRelic.transform.position = throwSource.transform.position;
                    heldRelic.transform.parent = throwSource.transform;
                    heldRelic.GetComponent<Rigidbody>().useGravity = false;
                    heldRelic.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    relicCollected = true;
                }
            }

            if (hit.collider.gameObject.tag == "Button")
            {
                crosshair.rectTransform.sizeDelta = new Vector2(12f, 12f);
                crosshair.color = new Color(1f, 1f, 1f, 1f);

                if (Input.GetKey(KeyCode.F))
                {
                    hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                }
            }
        }

        if (Input.GetKey(KeyCode.F) && relicCollected == true)
        {
            heldRelic.transform.parent = null;
            heldRelic.GetComponent<Rigidbody>().useGravity = true;
            relicCollected = false;
        }

        if (hit.collider == null || hit.collider.gameObject.tag == "Untagged")
        {
            crosshair.rectTransform.sizeDelta = new Vector2(7f, 7f);
            crosshair.color = new Color(1f, 1f, 1f, 0.5f);
        }

        if (Input.GetKey(KeyCode.Mouse1) && relicCollected == true)
        {
            GameObject thrownRelic = Instantiate(relicModel, throwSource.transform.position, Quaternion.identity);
            thrownRelic.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce);
            relicCollected = false;
        }

        ///////////////////////////////////////////////
        //Compass Tracking///
        //Code-Tutorial courtesy of https://www.youtube.com/watch?v=MRAVwaGrmrk
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

    public void UpdateRelicLocation(RelicTracker relic)
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
    //////////////////////////////////////////////////////////

}
