using UnityEngine;

[CreateAssetMenu(fileName = "KartuAksi", menuName = "Kartu/Kartu Aksi", order = 1)]
public class KartuAksi : ScriptableObject, IKartu
{
    [SerializeField] private EffectStatus status;

    public GameVariables.EffectType OnExecuteEvent;

    public DiceDecide diceDecide;
    public PlayerAffect playerAffect;

    public EffectStatus GetEffectStatus()
    {
        return status;
    }

    public string GenerateStatusEffect()
    {
        string format = "";

        if (status.hp != 0) format += "HP " + status.hp + "   ";
        if (status.sanity != 0) format += "Sanity " + status.sanity + "   ";
        if (status.food != 0) format += "Food " + status.food;

        return format;
    }

    public EffectStatus GetDiceDecide(int diceNumber)
    {
        if (diceNumber > 0 && diceNumber <= diceDecide.maxDice[0])
        {
            return diceDecide.specialEffect[0];
        }
        else return diceDecide.specialEffect[1];
    }
    public string GenerateDiceEffect(int index)
    {
        string format = "";

        if (GetDiceDecide(index).hp != 0) format += "HP " + GetDiceDecide(index).hp + "   ";
        if (GetDiceDecide(index).sanity != 0) format += "Sanity " + GetDiceDecide(index).sanity + "   ";
        if (GetDiceDecide(index).food != 0) format += "Food " + GetDiceDecide(index).food;

        return format;
    }
}
