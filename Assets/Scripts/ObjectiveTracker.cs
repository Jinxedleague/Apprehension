using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTracker : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    [SerializeField] private List<GameObject> activeRelics;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject relics in GameObject.FindGameObjectsWithTag("Relic"))
        {
            activeRelics.Add(relics);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
