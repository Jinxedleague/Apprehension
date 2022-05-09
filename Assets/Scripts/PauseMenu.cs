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
    public Slider VolumeSlider;
    public Slider MusicSlider;
    public AudioSource Ambience;
    public AudioSource Music;
    public Text volumeText;
    public Text senseText;
    public Text musicText;

    public PlayerController playerCon;
    public BatCamera batCam;
    public CameraController monkeyCam;

    private void Start()
    {
        SensitivitySlider.value = 250;
        VolumeSlider.value = 0.25f;
        MusicSlider.value = 0.5f;
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

        volumeText.text = "Value:" + VolumeSlider.value.ToString();
        Ambience.volume = VolumeSlider.value;

        musicText.text = "Value:" + MusicSlider.value.ToString();
        Music.volume = MusicSlider.value;
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
