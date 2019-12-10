using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlayerManager : MonoBehaviour
{
    public static TurnPlayerManager instance;
    [SerializeField] private PlayersInfo info;

    private List<MerapiPlayer> players;
    private int currentState;

    private int firstTurn;

    [SerializeField] private int deadPlayer;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        deadPlayer = 0;
        players = new List<MerapiPlayer>();
    }

    public MerapiPlayer GetCurrentStatePlayer()
    {
        return players[currentState];
    }

    public void NextPlayer()
    {
        if (deadPlayer == players.Count)
        {
            currentState = firstTurn;
            StopAllCoroutines();
            Debug.Log("Kucing");
            GameOverManagement.instance.GameOver();
        }
        else
        {
            currentState++;
            if (currentState >= players.Count) currentState = 0;
            if (!TurnComplete())
            {
                if (players[currentState].isDead()) NextPlayer();
                info.ChangeTurn(currentState);
            }
        }
    }

    public void CheckPlayer()
    {
        info.ChangeTurn(currentState);
        if (players[currentState].isDead()) NextPlayer();
    }

    public void NotInTurn()
    {
        info.ChangeToDefault();
    }

    public void AddPlayer(MerapiPlayer player)
    {
        players.Add(player);
    }

    public void SetTurn(int turn)
    {
        firstTurn = turn;
    }

    public bool TurnComplete()
    {
        return firstTurn == currentState;
    }

    public void ExecuteEffectToCurrentPlayer(EffectStatus status)
    {
        GetCurrentStatePlayer().GetEffect(status);
    }

    public void ExecuteEffectOnSpecificPlayer(MerapiPlayer target, EffectStatus status)
    {
        target.GetEffect(status);
    }

    public void ExecuteEffectToAllPlayer(EffectStatus status)
    {
        foreach (MerapiPlayer player in players)
        {
            if (!player.isDead()) player.GetEffect(status);
        }
    }

    public void ChangeDayEffectToAllPlayer(int status)
    {
        foreach (MerapiPlayer player in players)
        {
            if (!player.isDead()) player.GetEffectToFood(status);
        }
    }

    public void PlayerDeath(MerapiPlayer player)
    {
        int currentIndex = players.IndexOf(player);
        if (!players[currentIndex].isDead())
        {
            deadPlayer++;
            players[currentIndex].wasDead();
        }
    }

    public int GetLeftPlayer()
    {
        return players.Count - deadPlayer;
    }
   

    public bool IsAllPlayerDead()
    {
        return deadPlayer == players.Count;
    }

    public bool CheckPlayerDead(MerapiPlayer characterPlayer)
    {
        return characterPlayer.GetKarakter().GetHP() == 0 || characterPlayer.GetKarakter().GetSanity() == 0 || characterPlayer.GetKarakter().GetFood() == 0;
    }

    public MerapiPlayer GetPlayer(int index)
    {
        return players[index];
    }
}
