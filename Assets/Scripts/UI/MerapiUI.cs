using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MerapiUI : MonoBehaviour
{
    public static MerapiUI instance;
    public PlayersInfo info;

    [Header("Panel")]
    [SerializeField] private GameObject phasePanel;
    [SerializeField] private GameObject changeDayPhasePanel;

    [Header("DayPhase")]
    [SerializeField] private GameObject dayPhase_card;
    [SerializeField] private TextMeshProUGUI dayPhase_title;
    [SerializeField] private TextMeshProUGUI dayPhase_description;
    [SerializeField] private TextMeshProUGUI dayPhase_status;
    [SerializeField] private TextMeshProUGUI dayIndicator;

    [Header("NightPhase")]
    [SerializeField] private GameObject nightPhase_card;
    [SerializeField] private TextMeshProUGUI nightPhase_title;
    [SerializeField] private TextMeshProUGUI nightPhase_description;
    [SerializeField] private TextMeshProUGUI nightPhase_status;

    [Header("ActionPhase")]
    [SerializeField] private GameObject takeAction_panel;
    [SerializeField] private TextMeshProUGUI takeAction_name;
    [SerializeField] private GameObject actionPhase_card;
    [SerializeField] private TextMeshProUGUI actionPhase_title;
    [SerializeField] private TextMeshProUGUI actionPhase_description;
    [SerializeField] private TextMeshProUGUI actionPhase_status;

    [Header("StatusPhase")]
    [SerializeField] private GameObject statusPhase_card;
    [SerializeField] private TextMeshProUGUI statusPhase_title;
    [SerializeField] private TextMeshProUGUI statusPhase_description;
    [SerializeField] private TextMeshProUGUI statusPhase_status;
    [SerializeField] private GameObject rollDiceAction;
    [SerializeField] private GameObject rollDiceStatus;
    [SerializeField] private TextMeshProUGUI rollDice_text_Status;
    [SerializeField] private GameObject rollDiceInfoStatus;
    [SerializeField] private TextMeshProUGUI rollDice_text_Action;
    [SerializeField] private GameObject rollDiceInfoAction;

    [Header("Log")]
    [SerializeField] private TextMeshProUGUI log;

    public enum TypePhase
    {
        DayPhase, ActionPhase, StatusPhase, NightPhase, EndDayPhase
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void Render(TypePhase phase)
    {
        if (TurnPlayerManager.instance.IsAllPlayerDead()) return;
        if ( !(phase == TypePhase.EndDayPhase) ) phasePanel.SetActive(true);

        switch (phase) {
            case TypePhase.DayPhase :
                RenderDayPhase();
                break;
            case TypePhase.ActionPhase:
                RenderActionPhase();
                break;
            case TypePhase.StatusPhase:
                RenderStatusPhase();
                break;
            case TypePhase.NightPhase:
                RenderNightPhase();
                break;
            case TypePhase.EndDayPhase:
                RenderChangeDayPhase();
                break;
        }
    }

    private void RenderDayPhase()
    {
        dayPhase_card.SetActive(true);
        //ColorMode
        dayPhase_card.GetComponent<Image>().color = Color.white;
        dayPhase_title.color = Color.black;
        dayPhase_description.color = Color.black;
        dayPhase_status.color = Color.black;

        KartuHari cardDay = DayManagement.instance.GetCardDay();
        dayPhase_title.text = cardDay.dayName;
        dayPhase_description.text = cardDay.GetDayEffect().description;
        dayPhase_status.text = GenerateStatusEffect(cardDay.GetDayEffect());
    }

    private void RenderNightPhase()
    {
        nightPhase_card.SetActive(true);
        //ColorMode
        nightPhase_card.GetComponent<Image>().color = Color.black;
        nightPhase_title.color = Color.white;
        nightPhase_description.color = Color.white;
        nightPhase_status.color = Color.white;

        KartuHari cardDay = DayManagement.instance.GetCardDay();
        nightPhase_title.text = cardDay.dayName;
        nightPhase_description.text = cardDay.GetNightEffect().description;
        nightPhase_status.text = GenerateStatusEffect(cardDay.GetNightEffect());
    }

    private void RenderActionPhase()
    {
        actionPhase_card.SetActive(true);

        KartuAksi actionCard = ActionManagement.instance.GetCurrentAction();
        actionPhase_title.text = actionCard.GetEffectStatus().nameCard;
        actionPhase_description.text = actionCard.GetEffectStatus().description;
        actionPhase_status.text = GenerateStatusEffect(actionCard.GetEffectStatus());
    }

    private void RenderStatusPhase()
    {
        statusPhase_card.SetActive(true);

        KartuStatus statusCard = StatusManagement.instance.GetCurrentStatus();
        statusPhase_title.text = statusCard.GetEffectStatus().nameCard;
        statusPhase_description.text = statusCard.GetEffectStatus().description;
        statusPhase_status.text = GenerateStatusEffect(statusCard.GetEffectStatus());
    }

    private void RenderChangeDayPhase()
    {
        changeDayPhasePanel.SetActive(true);
    }

    public void RenderTakeAction()
    {
        takeAction_panel.SetActive(true);
        takeAction_name.text = TurnPlayerManager.instance.GetCurrentStatePlayer().GetKarakter().GetName() + "'s Turn";
    }

    public void UpdateLog(string playerName, string message)
    {
        string name = playerName + " : " + message;
        log.text = name;

        info.UpdateRender();
    }

    string GenerateStatusEffect(EffectStatus status)
    {
        string format =  "";

        if (status.hp != 0) format += "HP " + status.hp + "\n";
        if (status.sanity != 0) format += "Sanity " + status.sanity + "\n";
        if (status.food != 0) format += "Food " + status.food + "\n";

        return format;
    }

    public void UpdateIndicator(string dayName, string dayCounter)
    {
        dayIndicator.text = dayName + " - Day " + dayCounter; 
    }

    public void RenderRollDiceInfoStatus(string info)
    {
        rollDiceInfoStatus.SetActive(true);
        rollDice_text_Status.text = info;
    }

    public void RenderRollDiceInfoAction(string info)
    {
        rollDiceInfoAction.SetActive(true);
        rollDice_text_Action.text = info;
    }

    public void RenderRolDice(string type)
    {
        if (type.Equals("STATUS")) rollDiceStatus.SetActive(true);
        else rollDiceAction.SetActive(true);
    }

    public void ClosePanel()
    {
        phasePanel.SetActive(false);
    }
}
