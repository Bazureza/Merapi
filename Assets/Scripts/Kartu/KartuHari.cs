using UnityEngine;

[CreateAssetMenu(fileName = "KartuHari", menuName = "Kartu/Kartu Hari", order = 3)]
public class KartuHari : ScriptableObject, IKartu
{
    public string dayName;
    [SerializeField] private EffectStatus statusPagi;
    [SerializeField] private EffectStatus statusMalam;

    public EffectStatus GetEffectStatus()
    {
        return null;
    }

    public EffectStatus GetDayEffect()
    {
        return statusPagi;
    }

    public EffectStatus GetNightEffect()
    {
        return statusMalam;
    }

    public string GenerateStatusEffect(EffectStatus status)
    {
        string format = "";

        if (status.hp != 0) format += "HP " + status.hp +"   ";
        if (status.sanity != 0) format += "Sanity " + status.sanity + "   ";
        if (status.food != 0) format += "Food " + status.food;

        return format;
    }
}