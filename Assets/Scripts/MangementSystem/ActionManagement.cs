using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionManagement : MonoBehaviour
{
    public static ActionManagement instance;

    [SerializeField] private List<KartuAksi> actionCard;

    private Stack<KartuAksi> graveDeck;
    private Stack<KartuAksi> cardDeck;

    [SerializeField] private RollDice rollDice;

    private KartuAksi currentActionCard;

    private bool takeAction;

    private int dice = 0;

    private System.Random random = new System.Random();

    // Start is called before the first frame update
    void Awake()
    {
        graveDeck = new Stack<KartuAksi>();
        cardDeck = new Stack<KartuAksi>();
        DeckInitialize();
    }

    private void Start()
    {
        instance = this;
    }

    void DeckInitialize()
    {
        KartuAksi[] values = actionCard.ToArray();
        cardDeck.Clear();
        foreach (KartuAksi value in values.OrderBy(x => random.Next()))
            cardDeck.Push(value);
    }

    public void DrawActionCard()
    {
        while (cardDeck.Count > 0)
        {
            currentActionCard = cardDeck.Peek();
            graveDeck.Push(cardDeck.Pop());
        }

        if (cardDeck.Count == 0)
        {
            Shuffle();
        }
    }

    private void Shuffle()
    {
        var values = graveDeck.ToArray();
        graveDeck.Clear();
        foreach (var value in values.OrderBy(x => random.Next()))
            cardDeck.Push(value);
    }

    public void ExecuteAction()
    {
        
        if (currentActionCard.OnExecuteEvent == GameVariables.EffectType.None)
        {
            TurnPlayerManager.instance.ExecuteEffectToCurrentPlayer(currentActionCard.GetEffectStatus());
            MerapiUI.instance.UpdateLog((TurnPlayerManager.instance.GetCurrentStatePlayer().GetKarakter().GetName() + " got "), currentActionCard.GenerateStatusEffect());
        }
        else if (currentActionCard.OnExecuteEvent == GameVariables.EffectType.Dice)
        {
            TurnPlayerManager.instance.ExecuteEffectToCurrentPlayer(currentActionCard.GetDiceDecide(dice));
            MerapiUI.instance.UpdateLog((TurnPlayerManager.instance.GetCurrentStatePlayer().GetKarakter().GetName() + " got "), currentActionCard.GenerateDiceEffect(dice));
        }
    }

    public KartuAksi GetCurrentAction()
    {
        return currentActionCard;
    }

    public void SetTakeAction(bool value)
    {
        takeAction = value;
        GameManagement.instance.AllowExecuteAction(true);
    }

    public bool GetTakeAction()
    {
        return takeAction;
    }

    public void RollTheDice()
    {
        
        try
        {
            dice = UnityEngine.Random.Range(1, 6);
            rollDice.Initialize(dice,"Action", currentActionCard.GetDiceDecide(dice).description);    
        }
        catch (Exception ex)
        {

        }
    }
}
