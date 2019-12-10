using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerapiPlayer : MonoBehaviour
{
    [SerializeField] private Karakter character;
    [SerializeField] private KartuStatus status;

    [SerializeField] private int skipActionCounter;

    public int skipTurn;

    private bool dead;

    public void SetKarakter(Karakter character)
    {
        this.character = character;
    }

    public void SetStatus(KartuStatus status)
    {
        this.status = status;
    }

    public void GetEffect(EffectStatus statusEffect)
    {
        character.TakeHPEffect(statusEffect.hp);
        character.TakeSanityEffect(statusEffect.sanity);
        character.TakeFoodEffect(statusEffect.food);
    }

    public void GetEffectToFood(int value)
    {
        character.TakeFoodEffect(value);
    }

    public Karakter GetKarakter()
    {
        return character;
    }

    public KartuStatus GetStatus()
    {
        return status;
    }

    public void SkipAction()
    {
        skipActionCounter++;
        character.TakeSanityEffect(-skipActionCounter);
        MerapiUI.instance.UpdateLog(character.name + " not doing anything and was possessed, his sanity is decrease by ", ""+skipActionCounter);
    }

    public void ResetSkipAction()
    {
        skipActionCounter = 0;
    }

    public bool isDead()
    {
        return dead;
    }

    public bool checkDead()
    {
        return character.GetFood() == 0 || character.GetSanity() == 0 || character.GetHP() == 0;
    }

    public void wasDead()
    {
        dead = true;
    }
}
