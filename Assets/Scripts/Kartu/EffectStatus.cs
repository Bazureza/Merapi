using UnityEngine;

[System.Serializable]
public class EffectStatus 
{
    public Sprite gambar;
    public string nameCard;
    public int hp;
    public int sanity;
    public int food;
    [TextArea(2,10)] public string description;
}
