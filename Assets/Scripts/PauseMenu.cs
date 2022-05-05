using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Slider SensitivitySlider;
    public Text senseText;

    public PlayerController playerCon;
    public BatCamera batCam;
    public CameraController monkeyCam;

    private void Start()
    {
        SensitivitySlider.value = 250;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        senseText.text = "Value: " + SensitivitySlider.value.ToString();
        playerCon.setMouseSensitivity(SensitivitySlider.value);
        batCam.setMouseSensitivity(SensitivitySlider.value);
        monkeyCam.setMouseSensitivity(SensitivitySlider.value);
    }

    private void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false; 
    }
    private void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void lockValues()
    {

    }
}
