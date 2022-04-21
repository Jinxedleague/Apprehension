using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBack : MonoBehaviour
{
    public GameObject Credit;
    public void Close()
    {
        Credit.SetActive(false); ;
    }
}
