using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIcons : MonoBehaviour
{
    [SerializeField] private Image C1Icon;
    [SerializeField] private Image C2Icon;
    [SerializeField] private Image C3Icon;
    [SerializeField] private Image C4Icon;

    [SerializeField] private Sprite[] icons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStatus(int challenger, int status)
    {
        if (challenger == 1)
        {
            if (status == 1)
            {
                C1Icon.sprite = icons[0];
            }
            if (status == 2)
            {
                C1Icon.sprite = icons[1];
            }
            if (status == 3)
            {
                C1Icon.sprite = icons[2];
            }
            if (status == 4)
            {
                C1Icon.sprite = icons[3];
            }
        }

        if (challenger == 2)
        {
            if (status == 1)
            {
                C2Icon.sprite = icons[0];
            }
            if (status == 2)
            {
                C2Icon.sprite = icons[1];
            }
            if (status == 3)
            {
                C2Icon.sprite = icons[2];
            }
            if (status == 4)
            {
                C2Icon.sprite = icons[3];
            }
        }

        if (challenger == 3)
        {
            if (status == 1)
            {
                C3Icon.sprite = icons[0];
            }
            if (status == 2)
            {
                C3Icon.sprite = icons[1];
            }
            if (status == 3)
            {
                C3Icon.sprite = icons[2];
            }
            if (status == 4)
            {
                C3Icon.sprite = icons[3];
            }
        }

        if (challenger == 4)
        {
            if (status == 1)
            {
                C4Icon.sprite = icons[0];
            }
            if (status == 2)
            {
                C4Icon.sprite = icons[1];
            }
            if (status == 3)
            {
                C4Icon.sprite = icons[2];
            }
            if (status == 4)
            {
                C4Icon.sprite = icons[3];
            }
        }
    }
}
