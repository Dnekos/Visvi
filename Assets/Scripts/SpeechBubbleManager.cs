using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleManager : MonoBehaviour
{
    // text variables
    public string loadedText;
    int textIndex = 0;

    // timing
    [SerializeField]
    float textSpeed = 1;
    float textTimer = 0;
    [SerializeField]
    bool counting = false;

    // components
    [SerializeField]
    Text childText;
    Text ownText;
    [SerializeField]
    Image bubble;
    [SerializeField]
    Image tail;

    public void LoadText(string text)
    {
        loadedText = text;
        textIndex = 0;
        textTimer = 0;
        counting = true;
        bubble.enabled = true;
        tail.enabled = true;
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
        textTimer += textSpeed * Time.deltaTime;
        if (counting)
        {
            if (textTimer > 1)
            {
                ownText.text += loadedText[textIndex];
                childText.text = ownText.text;

                textIndex++;
                textTimer = 0;
                if (textIndex == loadedText.Length)
                    counting = false;
            }
        }
        else if (textTimer > 10)
        {
            bubble.enabled = false;
            tail.enabled = false;
        }
    }
}
