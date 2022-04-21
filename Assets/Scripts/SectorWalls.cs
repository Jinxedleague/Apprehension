using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorWalls : MonoBehaviour
{
    [SerializeField] private GameObject trigger, beam, wall1, wall2, wall3, wall4, holder;
    [SerializeField] private float duration, speed;
    private bool wasActivated = false;
    private Vector3 upTarget, downTarget;

    // Start is called before the first frame update
    void Start()
    {
        upTarget = new Vector3(transform.localPosition.x, -85f, transform.localPosition.z);
        downTarget = new Vector3(transform.localPosition.x, -120f, transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (wasActivated == true)
        {
            duration -= 1 * Time.deltaTime;

            if (wall1.transform.localPosition.y < -85f && duration >= 0)
            {
                wall1.transform.Translate(Vector3.up * speed * Time.deltaTime);
                wall2.transform.Translate(Vector3.up * speed * Time.deltaTime);
                wall3.transform.Translate(Vector3.up * speed * Time.deltaTime);
                wall4.transform.Translate(Vector3.up * speed * Time.deltaTime);
            }

            if (duration <= 0)
            {
                wall1.transform.Translate(-Vector3.up * speed * Time.deltaTime);
                wall2.transform.Translate(-Vector3.up * speed * Time.deltaTime);
                wall3.transform.Translate(-Vector3.up * speed * Time.deltaTime);
                wall4.transform.Translate(-Vector3.up * speed * Time.deltaTime);
            }
        }

        if (wall1.transform.localPosition.y <= -120f)
        {
            Destroy(holder);
        }

        beam.SetActive(false);
    }

    public void ActivateWalls()
    {
        wasActivated = true;
        trigger.transform.position = new Vector3(0f, 0f, 0f);
    }

    public void ActivateBeam()
    {
        beam.SetActive(true);
    }
}
