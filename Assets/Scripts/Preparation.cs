using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preparation : MonoBehaviour
{
    public Karakter[] chars;

    private List<Karakter> listChars;

    private void Awake()
    {
        listChars = new List<Karakter>();
        CopyToStack();
    }

    public Karakter GetRandomChar()
    {
        int randomNum = Random.Range(0, 100) % listChars.Count;
        Karakter character = listChars[randomNum];
        listChars.RemoveAt(randomNum);
        character.Initialize();
        return character;
    }

    private void CopyToStack()
    {
        for (int i = 0; i<chars.Length; i++)
        {
            listChars.Add(chars[i]);
        }
    }
}
