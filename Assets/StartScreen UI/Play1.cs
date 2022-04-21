using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play1 : MonoBehaviour
{
    public void Starting()
    {
        SceneManager.LoadScene("MainLevel");
    }

}
