using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusManagement : MonoBehaviour
{
    public static StatusManagement instance;

    [SerializeField] private List<KartuStatus> statusCard;

    private KartuStatus currentActiveStatus;

    private Stack<KartuStatus> graveDeck;
    private Stack<KartuStatus> cardDeck;

    private MerapiPlayer targetPlayer;
    private int dice = 0;

    private System.Random random = new System.Random();

    // Start is called before the first frame update
    void Awake()
    {
        graveDeck = new Stack<KartuStatus>();
        cardDeck = new Stack<KartuStatus>();
        DeckInitialize();
    }

    private void Start()
    {
        instance = this;
    }

    public void DrawStatusCard()
    {
        while (cardDeck.Count > 0)
        {
            TurnPlayerManager.instance.GetCurrentStatePlayer().SetStatus(cardDeck.Peek());
            currentActiveStatus = cardDeck.Peek();
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

    public void ExecuteStatus()
    {
        if (currentActiveStatus.OnExecuteEvent == GameVariables.EffectType.None)
        {
            TurnPlayerManager.instance.ExecuteEffectToCurrentPlayer(currentActiveStatus.GetEffectStatus());
            MerapiUI.instance.UpdateLog((TurnPlayerManager.instance.GetCurrentStatePlayer().GetKarakter().GetName() + " got "), currentActiveStatus.GenerateStatusEffect());
        } else if (currentActiveStatus.OnExecuteEvent == GameVariables.EffectType.PlayerSelect)
        {
            TurnPlayerManager.instance.ExecuteEffectOnSpecificPlayer(targetPlayer, currentActiveStatus.GetPlayerAffect());
            MerapiUI.instance.UpdateLog((targetPlayer.GetKarakter().GetName() + " got effect from "), currentActiveStatus.GetPlayerAffect().nameCard + " by " + TurnPlayerManager.instance.GetCurrentStatePlayer().GetKarakter().GetName());
        } else
        {
            TurnPlayerManager.instance.ExecuteEffectToCurrentPlayer(currentActiveStatus.GetDiceDecide(dice));
            MerapiUI.instance.UpdateLog((TurnPlayerManager.instance.GetCurrentStatePlayer().GetKarakter().GetName() + " got "), currentActiveStatus.GenerateDiceEffect(dice));
        }
    }

    public void RollDice()
    {
        try
        {
            dice = UnityEngine.Random.Range(0, 6);
            MerapiUI.instance.RenderRollDiceInfoStatus("You roll " + dice + "\n" + currentActiveStatus.GetDiceDecide(dice).description);
        } catch (Exception ex)
        {

        }
    }

    public KartuStatus GetCurrentStatus()
    {
        return currentActiveStatus;
    }

    void DeckInitialize()
    {
        KartuStatus[] values = statusCard.ToArray();
        cardDeck.Clear();
        foreach (KartuStatus value in values.OrderBy(x => random.Next()))
            cardDeck.Push(value);
    }
}
