using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicTitle : MonoBehaviour
{
    public Slider SensitivitySlider;
    public Slider VolumeSlider;
    public Text volumeText;
    public Text senseText;
    public AudioSource Ambience;

    // Start is called before the first frame update
    void Start()
    {
        SensitivitySlider.value = 250;
        VolumeSlider.value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        senseText.text = "Value: " + SensitivitySlider.value.ToString();

        volumeText.text = "Value:" + VolumeSlider.value.ToString();
        Ambience.volume = VolumeSlider.value;
    }
}
