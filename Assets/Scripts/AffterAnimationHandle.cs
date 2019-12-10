using UnityEngine;
using UnityEngine.UI;

public class AffterAnimationHandle : MonoBehaviour
{
    public GameObject[] deactiveObject;
    
    public GameObject[] activeObject;

    public Button[] toggleInteractable;

    public void Execute()
    {
        foreach (var obj in deactiveObject)
        {
            obj.SetActive(false);
        }
        
        foreach (var obj in activeObject)
        {
            obj.SetActive(true);
        }

        foreach (var interact in toggleInteractable)
        {
            interact.interactable = true;
        }
    }

    public void ResetCondition()
    {
        foreach (var obj in deactiveObject)
        {
            obj.SetActive(true);
        }
        
        foreach (var obj in activeObject)
        {
            obj.SetActive(false);
        }

        foreach (var interact in toggleInteractable)
        {
            interact.interactable = false;
        }
    }
}
