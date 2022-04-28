using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    //Speed variables
    private float verticalSpeed = 5f;
    private float horizontalSpeed = 10f;
    private float dashSpeed = 35f;
    private float dashRotationSpeed = 20f;
    private float cameraShakeSpeed = 5f;
    //Ability Variables
    private bool canDash;
    private bool isDashing;
    //Component Variables
    private Rigidbody monkeyRB;
    //Camera Variables
    public GameObject cameraObject;
    private bool camUp;
    public GameObject player;
    private PlayerAbilities playerAbilities;

    // Start is called before the first frame update
    void Start()
    {
        isDashing = false;
        canDash = true;
        camUp = true;
        monkeyRB = GetComponent<Rigidbody>();
        playerAbilities = player.GetComponent<PlayerAbilities>();
    }

    void Update()
    {
        Debug.DrawRay(this.transform.position + transform.forward, this.transform.forward * 1f, Color.red);
        if (!isDashing)
        {
            if(cameraObject.transform.localPosition.y > .3f)
            {
                cameraObject.transform.Translate(0, -2 * Time.deltaTime, 0, Space.Self);
            }
            
            //Player walk
            monkeyRB.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") / verticalSpeed) + (transform.right * Input.GetAxis("Horizontal") / horizontalSpeed));

            if(canDash && Input.GetKeyDown(KeyCode.LeftShift))
            {
                isDashing = true;
                StartCoroutine(DashTimer());
            }
        }
        else
        {
            //Dash Mechanic
            float translation = dashSpeed * Time.deltaTime;
            float rotation = Input.GetAxis("Horizontal") * dashRotationSpeed * Time.deltaTime;

            // Move translation along the object's z-axis
            //transform.Translate(0, 0, translation);
            monkeyRB.MovePosition(transform.position + transform.forward);

            // Rotate around our y-axis
            transform.Rotate(0, rotation, 0);

            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                stopDash();
            }

            if(cameraObject.transform.localPosition.y > .75f)
            {
                camUp = false;
            }
            else if(cameraObject.transform.localPosition.y < .3f)
            {
                camUp = true;
            }

            if(camUp)
            {
                cameraObject.transform.Translate(0, 5 * Time.deltaTime, 0, Space.Self);
            }
            else
            {
                cameraObject.transform.Translate(0, -10 * Time.deltaTime, 0, Space.Self);
            }

            RaycastHit collisionCheck;
            if (Physics.Raycast(this.transform.position + this.transform.forward, this.transform.forward, out collisionCheck, 1f))
            {
                Debug.DrawRay(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1), this.transform.forward * collisionCheck.distance, Color.red);
                Debug.Log(collisionCheck.collider.gameObject.name);
                if(collisionCheck.collider.gameObject.CompareTag("DashCollider"))
                {
                    stopDash();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Challenger")
        {
            collision.gameObject.GetComponent<Challenger>().Caught();
            playerAbilities.challengerDead();
        }
    }

    public bool GetDashing()
    {
        return isDashing;
    }

    public IEnumerator DashTimer()
    {
        canDash = false;
        yield return new WaitForSeconds(3);
        isDashing = false;
        StartCoroutine(DashCooldown());
    }

    public IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(5);
        canDash = true;
    }

    public void setInactive()
    {
        isDashing = false;
        canDash = true;
    }

    public void stopDash()
    {
        monkeyRB.velocity = new Vector3(0,0,0);
        StopAllCoroutines();
        isDashing = false;
        canDash = false;
        StartCoroutine(DashCooldown());
    }
}
