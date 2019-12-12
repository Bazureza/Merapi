using UnityEngine;
using UnityEngine.UI;

public class RollDice : MonoBehaviour
{

    private string parameter;
    private string message;
    private int number;

    public Sprite[] imageDice;
    public Image dice;

    public Button button;
    
    public void Initialize(int number, string parameter, string message)
    {
        GetComponent<Animator>().enabled = true;
        this.number = number - 1;
        this.parameter = parameter;
        this.message = message;
    }

    public void Render()
    {
        GetComponent<Animator>().enabled = false;
        dice.sprite = imageDice[number];
        button.interactable = true;
    }

    public void Click()
    {
        switch (parameter)
        {
            case "Status" :
                MerapiUI.instance.RenderRollDiceInfoStatus("You roll " + (number+1) + "\n" + message);
                break;
            case "Action" :
                MerapiUI.instance.RenderRollDiceInfoAction("You roll " + (number+1) + "\n" + message);
                break;
        }
        button.interactable = false;
    }
}
