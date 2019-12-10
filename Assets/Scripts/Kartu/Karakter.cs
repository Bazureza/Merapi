using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Karakter", menuName = "Kartu/Karakter", order = 0)]
public class Karakter : ScriptableObject
{
    [SerializeField] private EffectStatus status;

    [SerializeField] private int hp;
    [SerializeField] private int sanity;
    [SerializeField] private int food;

    public void Initialize()
    {
        hp = status.hp;
        sanity = status.sanity;
        food = status.food;
    }

    public void TakeHPEffect(int hp)
    {
        this.hp += hp;
        if (this.hp > 10) this.hp = 10;
        if (this.hp <= 0)
        {
            this.hp = 0; 
        }
    }

    public void TakeSanityEffect(int sanity)
    {
        this.sanity += sanity;
        if (this.sanity > 10) this.sanity = 10;
        if (this.sanity <= 0)
        {
            this.sanity = 0;
        }
    }

    public void TakeFoodEffect(int food)
    {
        this.food += food;
        if (this.food > 10) this.food = 10;
        if (this.food <= 0)
        {
            this.food = 0;
        }
    }

    public string GetName()
    {
        return status.nameCard;
    }

    public int GetHP()
    {
        return hp;
    }

    public int GetSanity()
    {
        return sanity;
    }

    public int GetFood()
    {
        return food;
    }

    public Sprite GetImage()
    {
        return status.gambar;
    }
}
