using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManagement : MonoBehaviour
{
    public static GameOverManagement instance;
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private TextMeshProUGUI message;

    [Header("Panel")]
    [SerializeField] private GameObject dayCard;
    [SerializeField] private GameObject actionCard;
    [SerializeField] private GameObject statusCard;
    [SerializeField] private GameObject phasePanel;
    [SerializeField] private GameObject dayChangePanel;
    [SerializeField] private GameObject actionPanel;

    private void Start()
    {
        instance = this;
    }

    public void GameOver()
    {
        DeactivePanel();
        string msg = "";
        int leftPlayer = TurnPlayerManager.instance.GetLeftPlayer();
        if (leftPlayer > 0 && leftPlayer < 4)
        {
            msg = leftPlayer + " person was survive from Mysticalas\n";
            for (int i = 0; i < 4; i++)
            {
                if (!TurnPlayerManager.instance.GetPlayer(i).isDead())
                {
                    if (i + 1 == 4)
                    {
                        msg += TurnPlayerManager.instance.GetPlayer(i).GetKarakter().GetName();
                    }
                    else
                    {
                        msg += TurnPlayerManager.instance.GetPlayer(i).GetKarakter().GetName() + ", ";
                    }
                }
            }
        }
        else if (leftPlayer == 0) msg = "All player is not survive";
        else if (leftPlayer == 4) msg = "All player is survive";
        panelGameOver.SetActive(true);
    
        message.text = msg;
    }

    void DeactivePanel()
    {
        dayCard.SetActive(false);
        actionCard.SetActive(false);
        statusCard.SetActive(false);
        phasePanel.SetActive(false);
        actionPanel.SetActive(false);
        dayChangePanel.SetActive(false);
    }

    public void Click()
    {
        SceneManager.LoadScene("Merapi_MENU");
    }
}
