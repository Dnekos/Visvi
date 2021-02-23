using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleManager : MonoBehaviour
{
    // text variables
    public List<string> loadedText;
    int textIndex = 0;

    [Header("Timing")]
    [SerializeField]
    float textSpeed = 1;
    float textTimer = 0;
    [SerializeField]
    float waitTime = 10;
    [SerializeField]
    bool counting = false;

    [Header("Components")]
    [SerializeField]
    Text childText;
    Text ownText;
    [SerializeField]
    Image bubble;
    [SerializeField]
    Image tail;

    public void LoadText(string text)
    {
        loadedText.Add(text);
        if (!counting)
        {
            textIndex = 0;
            textTimer = 0;
            counting = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ownText = GetComponent<Text>();

        ownText.text = "";
        childText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        textTimer += textSpeed * Time.deltaTime; // increment time

        if (counting && textTimer > 1) // incrementing text
        {
            bubble.enabled = true;
            tail.enabled = true;

            ownText.text += loadedText[0][textIndex]; // add next letter
            childText.text = ownText.text;
            textIndex++;

            textTimer = 0; // reset timer
            if (textIndex == loadedText[0].Length) // end of phrase
            {
                loadedText.RemoveAt(0);
                textIndex = 0;
                counting = false;
            }
        }
        else if (textTimer > waitTime)
        {
            ownText.text = "";
            childText.text = "";

            if (loadedText.Count == 0)
            {
                bubble.enabled = false;
                tail.enabled = false;
                
            }
            else
                counting = true;
        }
    }
}
