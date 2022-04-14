using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDialogueBox : MonoBehaviour
{
    [SerializeField] GameObject storyDialogue;
    [SerializeField] GameObject endDialogue;

    public void HideStoryDialogue()
    {
        storyDialogue.SetActive(false);
        endDialogue.SetActive(true);
    }

    public void HideEndDialogue()
    {
        endDialogue.SetActive(false);
    }
}
