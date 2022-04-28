using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveWalls : MonoBehaviour
{

    [SerializeField] private float coolDown;
    [SerializeField] private Animator movingWallsAnimator;
    //[SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private Image northIcon, southIcon, eastIcon, westIcon, centerIcon, cornerIcon;
    [SerializeField] private Sprite disabledIcon;
    private float coolDownReset;
    private bool wallsReady = true;
    private bool startCooldown = false;
    private bool northUsed, southUsed, eastUsed, westUsed, centerUsed, cornersUsed;

    // Start is called before the first frame update
    void Start()
    {
        coolDownReset = coolDown;

        northUsed = false;
        southUsed = false;
        eastUsed = false;
        westUsed = false;
        centerUsed = false;
        cornersUsed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startCooldown == true)
        {
            coolDown -= 1 * Time.deltaTime;

            if (coolDown < 0f)
            {
                wallsReady = true;
                startCooldown = false;
                coolDown = coolDownReset;
            }

            //timer.text = coolDown.ToString("F2");
        }
    }

    public void BlockNorth()
    {
        if (northUsed == false)
        {
            if (wallsReady == true)
            {
                movingWallsAnimator.Play("BlockNorth");
                wallsReady = false;

                northUsed = true;
                northIcon.sprite = disabledIcon;
            }
        }
    }

    public void BlockSouth()
    {
        if (southUsed == false)
        {
            if (wallsReady == true)
            {
                movingWallsAnimator.Play("BlockSouth");
                wallsReady = false;

                southUsed = true;
                southIcon.sprite = disabledIcon;
            }
        }
    }

    public void BlockEast()
    {
        if (eastUsed == false)
        {
            if (wallsReady == true)
            {
                movingWallsAnimator.Play("BlockEast");
                wallsReady = false;

                eastUsed = true;
                eastIcon.sprite = disabledIcon;
            }
        }
    }

    public void BlockWest()
    {
        if (westUsed == false)
        {
            if (wallsReady == true)
            {
                movingWallsAnimator.Play("BlockWest");
                wallsReady = false;

                westUsed = true;
                westIcon.sprite = disabledIcon;
            }
        }
    }

    public void BlockCenter()
    {
        if (centerUsed == false)
        {
            if (wallsReady == true)
            {
                movingWallsAnimator.Play("BlockCenter");
                wallsReady = false;

                centerUsed = true;
                centerIcon.sprite = disabledIcon;
            }
        }
    }

    public void BlockCorners()
    {
        if (cornersUsed == false)
        {
            if (wallsReady == true)
            {
                movingWallsAnimator.Play("BlockCorners");
                wallsReady = false;

                cornersUsed = true;
                cornerIcon.sprite = disabledIcon;
            }
        }
    }

    public void StartCooldown()
    {
        startCooldown = !startCooldown;
    }
}
