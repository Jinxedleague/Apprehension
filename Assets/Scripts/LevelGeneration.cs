using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private int xPos = -350;
    [SerializeField] private int zPos = 350;
    [SerializeField] private GameObject[] cellFills;
    [SerializeField] private int[] cellRotation;
    [SerializeField] private GameObject redChalice, blueChalice, greenChalice, yellowChalice;

    private int cellCount;
    private List<GameObject> generatedCells = new List<GameObject>();
    private int[] sector1Cells = { 0, 1, 2, 3, 8, 9, 10, 11, 16, 17, 18, 19, 24, 25, 26 };        //These arrays of ints are the indexes for the various cell spaces on the map
    private int[] sector2Cells = { 4, 5, 6, 7, 12, 13, 14, 15, 20, 21, 22, 23, 27, 28, 29 };      //Each cell space is designated a sector (the array they are in)
    private int[] sector3Cells = { 30, 31, 32, 36, 37, 38, 39, 44, 45, 46, 47, 52, 53, 54, 55 };  //Each sector divides the map into 4 equal pieces
    private int[] sector4Cells = { 33, 34, 35, 40, 41, 42, 43, 48, 49, 50, 51, 56, 57, 58, 59 };

    private void Awake()
    {
        GenerateLevel();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void GenerateLevel()
    {
        //Generate Cells
        while (cellCount < 60)                                                                                                                                                            //Current map of 8x8 w/ 4 empty in the center gives us 60. Run the following until we create all 60 cells
        {
            int randomCell = Random.Range(0, 15);                                                                                                                                          //Pick a number between 0 and how ever many cell fill prefabs are used
            int randomRotation = Random.Range(0, 4);                                                                                                                                      //Select a number corresponding with one of four 90 degree rotations for the cell fills
            GameObject newCell = Instantiate<GameObject>(cellFills[randomCell], new Vector3(xPos, 0f, zPos), Quaternion.AngleAxis(cellRotation[randomRotation], Vector3.up));      //(Photon requires a special instantiation)  In the cellFills list, select a cell using the randomly selected number from before. Give it the currently used xPos and zPos. Lastly, give it a random rotation from the cellRotation list.
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
        GameObject redChaliceSpawn = generatedCells[sector1Cells[Random.Range(0, 15)]].transform.Find("RelicSpawn").gameObject;   //Reference the generatedCells list and select a random sell that is in the sector. Find the "Cell-Relic" in that cell and activate it.
        GameObject newRedChalice = Instantiate<GameObject>(redChalice, redChaliceSpawn.transform.position, redChaliceSpawn.transform.rotation);

        GameObject blueChaliceSpawn = generatedCells[sector2Cells[Random.Range(0, 15)]].transform.Find("RelicSpawn").gameObject;
        GameObject newBlueChalice = Instantiate<GameObject>(blueChalice, blueChaliceSpawn.transform.position, blueChaliceSpawn.transform.rotation);

        GameObject greenChaliceSpawn = generatedCells[sector3Cells[Random.Range(0, 15)]].transform.Find("RelicSpawn").gameObject;
        GameObject newGreenChalice = Instantiate<GameObject>(greenChalice, greenChaliceSpawn.transform.position, greenChaliceSpawn.transform.rotation);

        GameObject yellowChaliceSpawn = generatedCells[sector4Cells[Random.Range(0, 15)]].transform.Find("RelicSpawn").gameObject;
        GameObject newYellowChalice = Instantiate<GameObject>(yellowChalice, yellowChaliceSpawn.transform.position, yellowChaliceSpawn.transform.rotation);
    }
}
