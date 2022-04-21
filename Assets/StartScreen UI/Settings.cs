using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject Credit;
    public void Open()
    {
        Credit.SetActive(true);
    }
}
