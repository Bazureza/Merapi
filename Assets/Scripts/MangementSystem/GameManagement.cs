using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;

    private bool dayPhase;
    private bool actionPhase;
    private bool executeAction;
    private bool statusPhase;
    private bool executeStatus;
    private bool changeDayPhase;

    private void Start()
    {
        instance = this;      
    }

    public void StartTheGame()
    {
        StartCoroutine(Sequence());
    }

    void DayPhase()
    {
        dayPhase = false;
        DayManagement.instance.ExecuteDayEffect();

        actionPhase = true;
    }

    void NightPhase()
    {
        dayPhase = false;
        DayManagement.instance.ExecuteNightEffect();

        statusPhase = true;
    }

    IEnumerator ActionPhase()
    {
        actionPhase = false;
        executeAction = false;
        do
        {

            TurnPlayerManager.instance.CheckPlayer();
            //Prepare Action
            MerapiUI.instance.RenderTakeAction();
            yield return new WaitUntil(AllowExecuteAction);
            executeAction = false;
            //If take action
            if (ActionManagement.instance.GetTakeAction())
            {
                TurnPlayerManager.instance.GetCurrentStatePlayer().ResetSkipAction();
                ActionManagement.instance.DrawActionCard();

                MerapiUI.instance.Render(MerapiUI.TypePhase.ActionPhase);
                yield return new WaitUntil(AllowExecuteAction);
                ActionManagement.instance.ExecuteAction();
            }
            else
            {
                TurnPlayerManager.instance.GetCurrentStatePlayer().SkipAction();
            }
            TurnPlayerManager.instance.NextPlayer();
            executeAction = false;
        } while (!TurnPlayerManager.instance.TurnComplete());

        yield return new WaitUntil(TurnPlayerManager.instance.TurnComplete);

        TurnPlayerManager.instance.NotInTurn();
        //Prepare Night Phase
        //Active UI NightPhase kalau dah diklik di UI toggle isNightPhase
        MerapiUI.instance.Render(MerapiUI.TypePhase.NightPhase);
    }

    IEnumerator StatusPhase()
    {
        statusPhase = false;
        executeStatus = false;
        do
        {
            TurnPlayerManager.instance.CheckPlayer();
            // Belum di implement drawnya
            //Prepare Status
            StatusManagement.instance.DrawStatusCard();

            MerapiUI.instance.Render(MerapiUI.TypePhase.StatusPhase);
            yield return new WaitUntil(AllowExecuteStatus);
            StatusManagement.instance.ExecuteStatus();
            TurnPlayerManager.instance.NextPlayer();
            executeStatus = false;
        } while (!TurnPlayerManager.instance.TurnComplete());

        yield return new WaitUntil(TurnPlayerManager.instance.TurnComplete);

        TurnPlayerManager.instance.NotInTurn();
        //Prepare Next day Phase
        MerapiUI.instance.Render(MerapiUI.TypePhase.EndDayPhase);
    }

    void ChangeDayPhase()
    {
        changeDayPhase = false;
        DayManagement.instance.NextDay();
        // Take effect to all player
    }

    IEnumerator Sequence()
    {
        while (DayManagement.instance.GetCounter() < 7)
        {
            TurnPlayerManager.instance.NotInTurn();
            DayManagement.instance.DrawDayCard();
            MerapiUI.instance.Render(MerapiUI.TypePhase.DayPhase);
            yield return new WaitUntil(isDayPhase);
            DayPhase();
            yield return new WaitUntil(isActionPhase);
            StartCoroutine(ActionPhase());
            yield return new WaitUntil(isDayPhase);

            NightPhase();

            if (DayManagement.instance.GetCounter() % 2 == 1)
            {
                yield return new WaitUntil(isStatusPhase);
                StartCoroutine(StatusPhase());
            }
            else
            {
                statusPhase = false;
                MerapiUI.instance.Render(MerapiUI.TypePhase.EndDayPhase);
            }
            yield return new WaitUntil(isChangeDayPhase);
            ChangeDayPhase();
        }
        yield return null;
        GameOverManagement.instance.GameOver();
    }

    private bool isDayPhase()
    {
        return dayPhase;
    }

    private bool isActionPhase()
    {
        return actionPhase;
    }

    private bool isStatusPhase()
    {
        return statusPhase;
    }

    private bool isChangeDayPhase()
    {
        return changeDayPhase;
    }

    public void SetDayPhase(bool value)
    {
        dayPhase = value;
    }

    public void SetActionPhase(bool value)
    {
        actionPhase = value;
    }

    public void SetStatusPhase(bool value)
    {
        statusPhase = value;
    }

    public void SetChangeDayPhase(bool value)
    {
        changeDayPhase = value;
    }

    private bool AllowExecuteAction()
    {
        return executeAction;
    }

    private bool AllowExecuteStatus()
    {
        return executeStatus;
    }

    public void AllowExecuteAction(bool value)
    {
        executeAction = value;
        MerapiUI.instance.ClosePanel();
    }

    public void AllowExecuteStatus(bool value)
    {
        executeStatus = value;
        MerapiUI.instance.ClosePanel();
    }


    public void Status()
    {
        if (StatusManagement.instance.GetCurrentStatus().OnExecuteEvent == GameVariables.EffectType.None)
        {
            AllowExecuteStatus(true);
        } else if (StatusManagement.instance.GetCurrentStatus().OnExecuteEvent == GameVariables.EffectType.Dice)
        {
            MerapiUI.instance.RenderRolDice("STATUS");
        }
    }

    public void Action()
    {
        if (ActionManagement.instance.GetCurrentAction().OnExecuteEvent == GameVariables.EffectType.None)
        {
            AllowExecuteAction(true);
        }
        else if (ActionManagement.instance.GetCurrentAction().OnExecuteEvent == GameVariables.EffectType.Dice)
        {
            MerapiUI.instance.RenderRolDice("ACTION");
        }
    }
}
