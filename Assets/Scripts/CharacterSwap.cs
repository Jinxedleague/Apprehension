using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwap : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject player, bat, monke, playerCam;
    [SerializeField] private Slider timerMM, timerBat, timerMonke;
    [SerializeField] private Image mmTimerBarFill, batTimerBarFill, monkeTimerBarFill, mmIconBackground, batIconBackground, monkeIconBackground;
    [SerializeField] private int mmCooldownTime, batCooldownTime, monkeCooldownTime;    //! Whenever this value is changed in the editor, change the MaxValue for the associated timer UI elements !

    private float mmRecharge, batRecharge, monkeRecharge;
    private bool mmActive, batActive, monkeActive;

    // Start is called before the first frame update
    void Start()
    {
        bat.SetActive(false);
        monke.SetActive(false);

        mmActive = true;
        batActive = false;
        monkeActive = false;

        timerMM.value = mmCooldownTime;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Alpha1) && mmActive == false && mmRecharge >= mmCooldownTime)
        {
            ActivateMM();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && batActive == false && batRecharge >= batCooldownTime)
        {
            ActivateBat();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && monkeActive == false && monkeRecharge >= monkeCooldownTime)
        {
            ActivateMonke();
        }

        ManageCooldown();
        ManageTimerColors();
        ManageIconBackgrounds();
        UpdateTimers();
    }

    private void ManageCooldown()
    {
        if (mmActive == false && mmRecharge < mmCooldownTime)
        {
            mmRecharge += 1 * Time.deltaTime;
        }

        if (batActive == false && batRecharge < batCooldownTime)
        {
            batRecharge += 1 * Time.deltaTime;
        }

        if (monkeActive == false && monkeRecharge < monkeCooldownTime)
        {
            monkeRecharge += 1 * Time.deltaTime;
        }
    }

    private void ManageTimerColors()
    {
        if (mmRecharge < mmCooldownTime)
        {
            mmTimerBarFill.color = new Color(0.92549f, 0.77255f, 0.1725f);
        } else
        {
            mmTimerBarFill.color = new Color(0.39216f, 0.65098f, 0.15686f);
        }

        if (batRecharge < batCooldownTime)
        {
            batTimerBarFill.color = new Color(0.92549f, 0.77255f, 0.1725f);
        }
        else
        {
            batTimerBarFill.color = new Color(0.39216f, 0.65098f, 0.15686f);
        }

        if (monkeRecharge < monkeCooldownTime)
        {
            monkeTimerBarFill.color = new Color(0.92549f, 0.77255f, 0.1725f);
        }
        else
        {
            monkeTimerBarFill.color = new Color(0.39216f, 0.65098f, 0.15686f);
        }
    }

    private void ManageIconBackgrounds()
    {
        if (mmActive == true)
        {
            mmIconBackground.color = new Color(0.81f, 0.19f, 0.09f);
        } else
        {
            mmIconBackground.color = new Color(0.78f, 0.78f, 0.78f);
        }

        if (batActive == true)
        {
            batIconBackground.color = new Color(0.81f, 0.19f, 0.09f);
        }
        else
        {
            batIconBackground.color = new Color(0.78f, 0.78f, 0.78f);
        }

        if (monkeActive == true)
        {
            monkeIconBackground.color = new Color(0.81f, 0.19f, 0.09f);
        }
        else
        {
            monkeIconBackground.color = new Color(0.78f, 0.78f, 0.78f);
        }
    }

    private void UpdateTimers()
    {
        timerMM.value = mmRecharge;
        timerBat.value = batRecharge;
        timerMonke.value = monkeRecharge;
    }

    public void ActivateMM()
    {
        bat.SetActive(false);
        monke.SetActive(false);
        playerController.enabled = true;
        playerCam.SetActive(true);

        mmRecharge = 0;
        mmActive = true;
        batActive = false;
        monkeActive = false;
    }

    public void ActivateBat()
    {
        bat.SetActive(true);
        monke.SetActive(false);
        bat.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z);
        playerController.enabled = false;
        playerCam.SetActive(false);

        batRecharge = 0;
        mmActive = false;
        batActive = true;
        monkeActive = false;
    }

    public void ActivateMonke()
    {
        bat.SetActive(false);
        monke.SetActive(true);
        playerController.enabled = false;
        playerCam.SetActive(false);

        monkeRecharge = 0;
        mmActive = false;
        batActive = false;
        monkeActive = true;
    }
}
