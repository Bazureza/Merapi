using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DayManagement : MonoBehaviour
{
    public static DayManagement instance;

    [SerializeField] private List<KartuHari> cardDay;

    private Stack<KartuHari> graveDeck;
    private Stack<KartuHari> cardDeck;

    private int dayCounter = 0;

    private KartuHari cardToday;

    private string currentDay;

    private List<string> days = new List<string>() { "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu", "Minggu" };
    private int dayState;

    private System.Random random = new System.Random();

    // Start is called before the first frame update
    void Awake()
    {
        currentDay = TranslateDayToIndonesia(DateTime.Now.DayOfWeek.ToString());
        dayState = GetState();
        graveDeck = new Stack<KartuHari>();
        cardDeck = new Stack<KartuHari>();
        dayCounter = 0;
        DeckInitialize();
        cardToday = null;
    }

    private void Start()
    {
        instance = this;
    }

    public string GetDay()
    {
        return days[dayState];
    }

    public KartuHari GetCardDay()
    {
        return cardToday;
    }

    int GetState()
    {
        return days.IndexOf(currentDay);
    }

    public void NextDay()
    {
        TurnPlayerManager.instance.ChangeDayEffectToAllPlayer(-1);
        MerapiUI.instance.UpdateLog("All Player has eaten ", 1 +" food");
        dayCounter++;
        dayState++;
        if (dayState >= days.Count) dayState = 0;

        currentDay = GetDay();
    }

    public void DrawDayCard()
    {
        SearchingDay();
        MerapiUI.instance.UpdateIndicator(cardToday.dayName, (dayCounter+1).ToString());
    }

    void SearchingDay()
    {
        KartuHari currentDayCard = cardToday;
        while (currentDayCard == cardToday)
        {
            while (cardDeck.Count > 0)
            {
                if (cardDeck.Peek().dayName.Equals(currentDay))
                {
                    cardToday = cardDeck.Peek();
                    graveDeck.Push(cardDeck.Pop());
                    break;
                }

                graveDeck.Push(cardDeck.Pop());
            }

            if (cardDeck.Count == 0)
            {
                Shuffle();
            }

        }
    }

    public void ExecuteDayEffect()
    {
        TurnPlayerManager.instance.ExecuteEffectToAllPlayer(cardToday.GetDayEffect());
        MerapiUI.instance.UpdateLog("All Player got ", cardToday.GenerateStatusEffect(cardToday.GetDayEffect()));
    }

    public void ExecuteNightEffect()
    {
        TurnPlayerManager.instance.ExecuteEffectToAllPlayer(cardToday.GetNightEffect());
        MerapiUI.instance.UpdateLog("All Player got ", cardToday.GenerateStatusEffect(cardToday.GetNightEffect()));
    }

    public void Shuffle()
    {
        var values = graveDeck.ToArray();
        graveDeck.Clear();
        foreach (var value in values.OrderBy(x => random.Next()))
            cardDeck.Push(value);
    }

    public int GetCounter()
    {
        return dayCounter;
    }

    void DeckInitialize()
    {
        KartuHari[] values = cardDay.ToArray();
        cardDeck.Clear();
        foreach (KartuHari value in values.OrderBy(x => random.Next()))
            cardDeck.Push(value);
    }

    string TranslateDayToIndonesia(string day)
    {
        switch (day)
        {
            case "Monday": return "Senin";
            case "Tuesday": return "Selasa";
            case "Wednesday": return "Rabu";
            case "Thursday": return "Kamis";
            case "Friday": return "Jumat";
            case "Saturday": return "Sabtu";
            case "Sunday": return "Minggu";
            default: return "";
        }
    }
}
