using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private float matchTime;
    [SerializeField] private TextMeshProUGUI relicsCollectedCounter;
    private int challengerScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        matchTime -= 1 * Time.deltaTime;

        if (matchTime <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Win");
        }

        if (challengerScore == 4)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Lose");
        }

        relicsCollectedCounter.text = ("Collected : " + challengerScore.ToString());
    }

    public void RelicDelivered()
    {
        challengerScore++;
    }
}
