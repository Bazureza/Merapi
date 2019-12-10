using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class MerapiManager : MonoBehaviour
{
    public static MerapiManager Instance = null;

    [SerializeField] private TextMeshProUGUI countdownText;

    private float timeCount;
    [SerializeField] private float timeMaxCount;

    private bool isCounting;

    [Header("Panel")]
    [SerializeField] private GameObject preparationPanel;
    [SerializeField] private GameObject inGamePanel;

    private void Start()
    {
        Instance = this;
        isCounting = true;
        timeCount = timeMaxCount;
        countdownText.text = "Game Starts in " + ((int)timeCount) + " second";
    }

    private void Update()
    {
        Countdown();
    }

    void Countdown()
    {
        if (isCounting)
        {
            if (timeCount > 0)
            {
                countdownText.text = "Game Starts in " + ((int)timeCount) + " second";
                timeCount -= Time.deltaTime;              
            }
            else
            {
                isCounting = false;
                timeCount = timeMaxCount;
                preparationPanel.SetActive(false);
                inGamePanel.SetActive(true);
                GameManagement.instance.StartTheGame();
            }
            
        }
    }
}

