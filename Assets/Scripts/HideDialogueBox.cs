using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDialogueBox : MonoBehaviour
{
    [SerializeField] GameObject storyDialogue;
    [SerializeField] GameObject endDialogue;

    void Awake()
    {
        Time.timeScale = 0;
    }
    public void HideStoryDialogue()
    {
        storyDialogue.SetActive(false);
        endDialogue.SetActive(true);
    }

    public void HideEndDialogue()
    {
        Time.timeScale = 1;
        endDialogue.SetActive(false);
    }
}
