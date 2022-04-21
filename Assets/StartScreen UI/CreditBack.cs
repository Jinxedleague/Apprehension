using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditBack : MonoBehaviour
{
    public GameObject Credit;
    public void Close()
    {
        Credit.SetActive(false); ;
    }
}
