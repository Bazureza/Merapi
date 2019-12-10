using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KartuStatus))] 
public class KartuStatusEditor : Editor
{
    private KartuStatus targetKartu;

    private SerializedProperty description;
    private SerializedProperty onExecute;
    private SerializedProperty onEveryday;
    private SerializedProperty onLast;

    DiceDecide dice;
    PlayerAffect playerAffect;


    private void OnEnable() {
        description = serializedObject.FindProperty("status.description");
        onExecute = serializedObject.FindProperty("OnExecuteEvent");
        onEveryday = serializedObject.FindProperty("OnEverydayEvent");
        onLast = serializedObject.FindProperty("OnLastEvent");
    }
    
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        /*targetKartu = (KartuStatus) target;
        serializedObject.UpdateIfDirtyOrScript();
        RenderDescription();
        CheckingOnExecuteEvent();
        serializedObject.ApplyModifiedProperties();*/
    }

    /*void RenderDescription()
    {
        targetKartu.status.gambar =
            (Sprite) EditorGUILayout.ObjectField("Card Image", targetKartu.status.gambar, typeof(Sprite));
        targetKartu.status.nameCard = EditorGUILayout.TextField("Card Name",targetKartu.status.nameCard);
    }*/

    /*void RenderOnExecuteEvent(System.Object effect)
    {
        EditorGUI.indentLevel++;
        if (targetKartu.OnExecute != effect)
        {
            targetKartu.OnExecute = effect;
            dice = null;
            playerAffect = null;
        }

        try
        {
            if (dice == null) dice = ((DiceDecide) targetKartu.OnExecute);
            GUILayout.Space(5);
            if (dice.maxDice.Length < 2)
            {
                if (GUILayout.Button("+"))
                {
                    dice.maxDice = new int[dice.maxDice.Length + 1];
                    dice.maxDice[dice.maxDice.Length - 1] = 6;
                    dice.specialEffect = new EffectStatus[dice.maxDice.Length];
                    for (int i = 0; i < dice.specialEffect.Length; i++)
                    {
                        dice.specialEffect[i] = new EffectStatus();
                    }
                }
                
            }
            else
            {
                if (GUILayout.Button("-"))
                {
                    dice.maxDice = new int[dice.maxDice.Length - 1];
                    dice.maxDice[dice.maxDice.Length - 1] = 6;
                    dice.specialEffect = new EffectStatus[dice.maxDice.Length];
                    for (int i = 0; i < dice.specialEffect.Length; i++)
                    {
                        dice.specialEffect[i] = new EffectStatus();
                    }
                }
            }

            GUILayout.Space(2);
            if (dice.maxDice.Length < 2)
            {
                EditorGUILayout.LabelField("Max Dice 1 : " + dice.maxDice[0]);
                dice.specialEffect[0].nameCard =
                    EditorGUILayout.TextField("Name Effect", dice.specialEffect[0].nameCard);
                dice.specialEffect[0].hp = EditorGUILayout.IntField("HP", dice.specialEffect[0].hp);
                dice.specialEffect[0].sanity = EditorGUILayout.IntField("Sanity", dice.specialEffect[0].sanity);
                dice.specialEffect[0].food = EditorGUILayout.IntField("Food", dice.specialEffect[0].food);
                dice.specialEffect[0].description =
                    EditorGUILayout.TextField("Description", dice.specialEffect[0].description);
            }
            else
            {
                for (int i = 0; i < dice.maxDice.Length; i++)
                {
                    if (i == 0)
                    {
                        dice.maxDice[i] = EditorGUILayout.IntSlider(("Max Dice " + (i + 1)), dice.maxDice[i], 0,
                            dice.maxDice[i + 1]);
                    }
                    else if (i == dice.maxDice.Length - 1)
                    {
                        EditorGUILayout.LabelField("Max Dice " + (i + 1) + " : " + dice.maxDice[i]);
                    }
                    else
                    {
                        dice.maxDice[i] = EditorGUILayout.IntSlider(("Max Dice " + (i + 1)), dice.maxDice[i],
                            dice.maxDice[i - 1],
                            dice.maxDice[i + 1]);
                    }

                    dice.specialEffect[i].nameCard =
                        EditorGUILayout.TextField("Name Effect", dice.specialEffect[i].nameCard);
                    dice.specialEffect[i].hp = EditorGUILayout.IntField("HP", dice.specialEffect[i].hp);
                    dice.specialEffect[i].sanity = EditorGUILayout.IntField("Sanity", dice.specialEffect[i].sanity);
                    dice.specialEffect[i].food = EditorGUILayout.IntField("Food", dice.specialEffect[i].food);
                    dice.specialEffect[i].description =
                        EditorGUILayout.TextField("Description", dice.specialEffect[i].description);
                }
            }
        }
        catch (Exception ex)
        {
            if (playerAffect == null) playerAffect = ((PlayerAffect) targetKartu.OnExecute);
            if (playerAffect.specialEffect == null) playerAffect.specialEffect = new EffectStatus();
            playerAffect.specialEffect.hp = EditorGUILayout.IntField("HP", playerAffect.specialEffect.hp);
            playerAffect.specialEffect.sanity = EditorGUILayout.IntField("Sanity", playerAffect.specialEffect.sanity);
            playerAffect.specialEffect.food = EditorGUILayout.IntField("Food", playerAffect.specialEffect.food);
            playerAffect.specialEffect.description =  EditorGUILayout.TextField("Description", playerAffect.specialEffect.description);
        }

        EditorGUI.indentLevel--;
    }

    void CheckingOnExecuteEvent()
    {
        EditorGUILayout.PropertyField(onExecute, new GUIContent("On Execute Event"));
        switch (targetKartu.OnExecuteEvent)
        {
            case GameVariables.EffectType.Dice :
                RenderOnExecuteEvent(new DiceDecide());
                break;
            case GameVariables.EffectType.PlayerSelect :
                RenderOnExecuteEvent(new PlayerAffect());
                break;
            case GameVariables.EffectType.None :
                if (targetKartu.OnExecute != null) targetKartu.OnExecute = null;
                targetKartu.status.hp = EditorGUILayout.IntField("HP", targetKartu.status.hp);
                targetKartu.status.sanity = EditorGUILayout.IntField("Sanity", targetKartu.status.sanity);
                targetKartu.status.food = EditorGUILayout.IntField("Food", targetKartu.status.food);
                EditorGUILayout.PropertyField(description, new GUIContent("Description"));
                break;
        }
    }*/
}
