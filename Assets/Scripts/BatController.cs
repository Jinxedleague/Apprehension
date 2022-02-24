using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    private float horizontalSpeed;
    private float verticalSpeed;
    private Rigidbody batRB;

    // Start is called before the first frame update
    void Start()
    {
        batRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            batRB.AddForce(transform.forward * horizontalSpeed);
        }
    }
}
