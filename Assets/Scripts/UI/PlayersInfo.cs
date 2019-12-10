using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayersInfo : MonoBehaviour
{
    public Preparation prepare;

    public GameObject playerPrefabs;

    public GameObject parentPlayerInfoUI;
    public GameObject PlayerInfoUI;

    public GameObject parentPreparePlayerInfoUI;
    public GameObject preparePlayerInfoUI;

    private List<MerapiPlayer> characterPlayer;
    private List<SetKarakterUIInGame> playerUICharacter;

    [SerializeField] private List<GameObject> deadPanel;

    #region UNITY

    public void Start()
    {
        playerUICharacter = new List<SetKarakterUIInGame>();
        characterPlayer = new List<MerapiPlayer>();
        deadPanel = new List<GameObject>();

        RenderPrepareInfo();
    }

    void RenderCharacterUIInGame(int index)
    {
        GameObject entry = Instantiate(PlayerInfoUI, parentPlayerInfoUI.transform);
        SetKarakterUIInGame UIPreparation = entry.GetComponent<SetKarakterUIInGame>();
        UIPreparation.charImage.sprite = characterPlayer[index].GetKarakter().GetImage();
        UIPreparation.characterName.text = characterPlayer[index].GetKarakter().GetName();
        UIPreparation.characterHP.text = characterPlayer[index].GetKarakter().GetHP() + "";
        UIPreparation.characterSanity.text = characterPlayer[index].GetKarakter().GetSanity() + "";
        UIPreparation.characterFood.text = characterPlayer[index].GetKarakter().GetFood() + "";
        deadPanel.Add(UIPreparation.deadPanel);

        playerUICharacter.Add(UIPreparation);

    }

    void RenderPrepareInfo()
    {
       
        for (int i = 0; i < 4; i++)
        {
            GameObject player = Instantiate(playerPrefabs, Vector2.zero, Quaternion.identity);
            MerapiPlayer mysticPlayer = player.GetComponent<MerapiPlayer>();
            mysticPlayer.SetKarakter(prepare.GetRandomChar());
            characterPlayer.Add(mysticPlayer);

            TurnPlayerManager.instance.AddPlayer(mysticPlayer);

            GameObject entry = Instantiate(preparePlayerInfoUI, parentPreparePlayerInfoUI.transform);
            SetKarakterUIPreparation UIPreparation = entry.GetComponent<SetKarakterUIPreparation>();
            UIPreparation.textNamePlayer.text = "Player " + (i+1);
            UIPreparation.characterName.text = characterPlayer[i].GetKarakter().GetName();
            UIPreparation.charImage.sprite = characterPlayer[i].GetKarakter().GetImage();
            RenderCharacterUIInGame(i);
        }
    }

    #endregion

    public void UpdateRender()
    {
        for (int i = 0; i < 4; i++)
        {      
            if (TurnPlayerManager.instance.CheckPlayerDead(characterPlayer[i]))
            {
                TurnPlayerManager.instance.PlayerDeath(characterPlayer[i]);
            }
/*            playerUICharacter[i].charImage.sprite = characterPlayer[i].GetKarakter().GetImage();
            playerUICharacter[i].characterName.text = characterPlayer[i].GetKarakter().GetName();*/
            playerUICharacter[i].characterHP.text = characterPlayer[i].GetKarakter().GetHP() + "";
            playerUICharacter[i].characterSanity.text = characterPlayer[i].GetKarakter().GetSanity() + "";
            playerUICharacter[i].characterFood.text = characterPlayer[i].GetKarakter().GetFood() + "";
            if (!characterPlayer[i].isDead())
            {
                deadPanel[i].SetActive(false);
            }
            else
            {
                deadPanel[i].SetActive(true);
            }
        }
    }

    public void ChangeTurn(int index)
    {
        for (int i = 0; i < characterPlayer.Count; i++)
        {
            if (i == index) playerUICharacter[i].gameObject.GetComponent<Image>().color = Color.yellow;
            else playerUICharacter[i].gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    public void ChangeToDefault()
    {
        for (int i = 0; i < characterPlayer.Count; i++)
        {
            playerUICharacter[i].gameObject.GetComponent<Image>().color = Color.white;
        }
    }

}
