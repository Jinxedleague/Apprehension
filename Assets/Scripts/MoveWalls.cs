using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveWalls : MonoBehaviour
{

    [SerializeField] private float coolDown;
    [SerializeField] private Animator movingWallsAnimator;
    [SerializeField] private TextMeshProUGUI timer;
    private float coolDownReset;
    private bool wallsReady = true;
    private bool startCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        coolDownReset = coolDown;
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

            timer.text = coolDown.ToString("F2");
        }
    }

    public void BlockNorth()
    {
        if (wallsReady == true)
        {
            movingWallsAnimator.Play("BlockNorth");
            wallsReady = false;

            Debug.Log("Blocking North");
        }
    }

    public void BlockSouth()
    {
        if (wallsReady == true)
        {
            movingWallsAnimator.Play("BlockSouth");
            wallsReady = false;
        }
        Debug.Log("Blocking South");
    }

    public void BlockEast()
    {
        if (wallsReady == true)
        {
            movingWallsAnimator.Play("BlockEast");
            wallsReady = false;
        }
        Debug.Log("Blocking East");
    }

    public void BlockWest()
    {
        if (wallsReady == true)
        {
            movingWallsAnimator.Play("BlockWest");
            wallsReady = false;
        }
        Debug.Log("Blocking West");
    }

    public void BlockCenter()
    {
        if (wallsReady == true)
        {
            movingWallsAnimator.Play("BlockCenter");
            wallsReady = false;
        }
        Debug.Log("Blocking Center");
    }

    public void BlockCorners()
    {
        if (wallsReady == true)
        {
            movingWallsAnimator.Play("BlockCorners");
            wallsReady = false;
        }
        Debug.Log("Blocking Corners");
    }

    public void StartCooldown()
    {
        startCooldown = !startCooldown;
    }
}
