using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    private float horizontalSpeed;
    private float verticalSpeed;
    private Rigidbody batRB;
    private float flapForce;
    private bool isGrounded;
    public GameObject batCam;
    private PlayerAbilities playerAbilities;
    private GameObject player;
    public Challenger challengerController1;
    public Challenger challengerController2;
    public Challenger challengerController3;
    public Challenger challengerController4;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        batRB = GetComponent<Rigidbody>();
        horizontalSpeed = 15f;
        verticalSpeed = 5f;
        flapForce = 5f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerAbilities = player.GetComponent<PlayerAbilities>();
        StartCoroutine(ConstantFlap());
    }

    // Update is called once per frame
    void Update()
    {
        //Player walk
        batRB.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") / verticalSpeed) + (transform.right * Input.GetAxis("Horizontal") / horizontalSpeed));

        //Player fly up and down
        if (Input.GetKey(KeyCode.Space))
        {
            batRB.MovePosition(transform.position + transform.up / 25);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            StopAllCoroutines();
            batRB.MovePosition(transform.position - transform.up / 25);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) && !isGrounded)
        {
            StartCoroutine(ConstantFlap());
        }

        //Mouse flight
        if (Input.GetKey(KeyCode.Mouse0) && !isGrounded)
        {
            batRB.MovePosition(transform.position + batCam.transform.forward / 3);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && !isGrounded)
        {
            batRB.velocity = new Vector3(0, 0, 0);
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(challengerController1.getRevealed());
            StartCoroutine(challengerController2.getRevealed());
            StartCoroutine(challengerController3.getRevealed());
            StartCoroutine(challengerController4.getRevealed());
        }
    }

    public IEnumerator ConstantFlap()
    {
        batRB.velocity = new Vector3(batRB.velocity.x, 0, batRB.velocity.z);
        batRB.AddForce(new Vector3(0, flapForce, 0), ForceMode.Impulse);
        yield return new WaitForSeconds(1);
        StartCoroutine(ConstantFlap());
    }

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
        horizontalSpeed = 12f;
        verticalSpeed = 8f;
        StopAllCoroutines();
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
        horizontalSpeed = 20f;
        verticalSpeed = 10f;
        StartCoroutine(ConstantFlap());
    }
}
