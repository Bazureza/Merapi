using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DiceDecide
{
    public int[] maxDice;
    public EffectStatus[] specialEffect;

    public DiceDecide()
    {
        maxDice = new[] {6};
        specialEffect = new[] {new EffectStatus()};
    }
}
