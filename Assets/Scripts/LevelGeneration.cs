using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private int xPos = -350;
    [SerializeField] private int zPos = 350;
    [SerializeField] private GameObject[] cellFills;
    [SerializeField] private int[] cellRotation;

    private int cellCount;
    private List<GameObject> generatedCells = new List<GameObject>();
    private int[] sector1Cells = { 0, 1, 2, 3, 8, 9, 10, 11, 16, 17, 18, 19, 24, 25, 26 };
    private int[] sector2Cells = { 4, 5, 6, 7, 12, 13, 14, 15, 20, 21, 22, 23, 27, 28, 29 };
    private int[] sector3Cells = { 30, 31, 32, 36, 37, 38, 39, 44, 45, 46, 47, 52, 53, 54, 55 };
    private int[] sector4Cells = { 33, 34, 35, 40, 41, 42, 43, 48, 49, 50, 51, 56, 57, 58, 59 };

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)                                                                                                                                                     //Run the following ONLY if the user is the host. 
        {

            //Generate Cells
            while (cellCount < 60)                                                                                                                                                            //Current map of 8x8 w/ 4 empty in the center gives us 60. Run the following until we create all 60 cells
            {
                int randomCell = Random.Range(0, 8);                                                                                                                                          //Pick a number between 0 and how ever many cell fill prefabs are used
                int randomRotation = Random.Range(0, 4);                                                                                                                                      //Select a number corresponding with one of four 90 degree rotations for the cell fills
                GameObject newCell = PhotonNetwork.Instantiate(cellFills[randomCell].name, new Vector3(xPos, 0f, zPos), Quaternion.AngleAxis(cellRotation[randomRotation], Vector3.up));      //(Photon requires a special instantiation)  In the cellFills list, select a cell using the randomly selected number from before. Give it the currently used xPos and zPos. Lastly, give it a random rotation from the cellRotation list.
                generatedCells.Add(newCell);                                                                                                                                                  //Add the newly created cell to the generatedCells list.

                xPos += 100;                                                                                                                                                                  //Move xPos left to get the position for the next cell

                if (xPos > 350)                                                                                                                                                               //With the 100x100 cells, going over 350 would start generating a 9-wide map
                {
                    xPos = -350;                                                                                                                                                              //Reset xPos back to the left and move down to the next row
                    zPos -= 100;
                }

                if (xPos == -50 && zPos == 50)                                                                                                                                                //Following lines are used to keep the center 4 spaces empty                                                                                                                                         
                {
                    xPos = 150;
                    zPos = 50;
                }

                if (xPos == -50 && zPos == -50)
                {
                    xPos = 150;
                    zPos = -50;
                }

                cellCount++;
            }


            //Activate Relics
            GameObject relic1 = generatedCells[sector1Cells[Random.Range(0, 15)]].transform.Find("Cell-Relic").gameObject;                //Reference the generatedCells list and select a random sell that is in the sector. Find the "Cell-Relic" in that cell and activate it.
            relic1.SetActive(true);

            GameObject relic2 = generatedCells[sector2Cells[Random.Range(0, 15)]].transform.Find("Cell-Relic").gameObject;
            relic2.SetActive(true);

            GameObject relic3 = generatedCells[sector3Cells[Random.Range(0, 15)]].transform.Find("Cell-Relic").gameObject;
            relic3.SetActive(true);

            GameObject relic4 = generatedCells[sector4Cells[Random.Range(0, 15)]].transform.Find("Cell-Relic").gameObject;
            relic4.SetActive(true);


            //Activate Walls
            int randomStart = Random.Range(0, 3);                                                                                         //This random int will determine where the following code starts.                                       
            int randomInterval = 3;                                                                                                       //This int determines how many cells are skipped before the next wall is activated
            int count = randomStart;

            while (count < 60)
            {
                GameObject cellWall = generatedCells[randomStart].transform.Find("Cell-Gate").gameObject;                                 //Reference the list generatedCell and select one with the randomStart index. Find the "Cell-Gate" child and activate it.
                cellWall.SetActive(true);                                                                                                 //Move to a new cell based on the randomInterval and repeat.
                randomStart += randomInterval;
                count += randomInterval;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
