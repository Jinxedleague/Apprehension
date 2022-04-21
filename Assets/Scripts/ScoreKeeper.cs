using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private float matchTime;
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
            SceneManager.LoadScene("Win");
        }

        if (challengerScore == 4)
        {
            SceneManager.LoadScene("Lose");
        }
    }

    public void RelicDelivered()
    {
        challengerScore++;
    }
}
