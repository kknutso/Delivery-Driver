using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliverySystem : MonoBehaviour
{
    [SerializeField] int packagesToBeDelivered;
    [SerializeField] Image[] packages;

    [SerializeField] Canvas winLabel;
    [SerializeField] Canvas loseLabel;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    void Update()
    {
        timerImage.fillAmount = timer.GetFillFraction();
        DisplayPackages();
        LoseLevel();       
    }

    public void SetPackagesDelivered(int value)
    {
        packagesToBeDelivered = packagesToBeDelivered - value;
    }

    void DisplayPackages()
    {
        for (int i = 0; i < packages.Length; i++)
        {
            if(i < packagesToBeDelivered)
            {
                packages[i].enabled = true;
            }
            else
            {
                packages[i].enabled = false;
            }
        }
    }

    public void WinLevel()
    {
        if(packagesToBeDelivered <= 0)
        {
            Time.timeScale = 0;
            timer.CancelTimer();
            winLabel.gameObject.SetActive(true);
        }
    }

    void LoseLevel()
    {
        float timeLeft = timer.GetTimerValue();

        if (timeLeft <= 0)     
        {
            if (winLabel.gameObject.activeSelf == false)
            {
                Time.timeScale = 0;
                loseLabel.gameObject.SetActive(true);
            }
        }
    }
}
