using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleManager : MonoBehaviour
{
    // text variables
    public List<KeyValuePair<string,string>> loadedText;
    int textIndex = 0;

    [Header("Timing")]
    [SerializeField]
    float textSpeed = 1;
    float textTimer = 0;
    [SerializeField]
    bool counting = false;

    [Header("Components")]
    [SerializeField]
    Text childText;
    [SerializeField]
    Text phoneticText;
    Text ownText;
    [SerializeField]
    Image bubble;
    [SerializeField]
    Image tail;

    public void LoadText(string text, string subtext = default)
    {
        loadedText.Add(new KeyValuePair<string,string>(text,subtext));
        if (!counting)
        {
            textIndex = 0;
            textTimer = 0;
            counting = true;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        loadedText = new List<KeyValuePair<string, string>>();
        ownText = GetComponent<Text>();

        ownText.text = "";
        childText.text = "";
        bubble.enabled = false;
        tail.enabled = false;
    }

    public void NextLine()
    {
        if (counting)
            return;

        loadedText.RemoveAt(0); // reset list to next
        textIndex = 0;

        ownText.text = "";
        childText.text = "";
        phoneticText.text = "";

        if (loadedText.Count == 0)
        {
            bubble.enabled = false;
            tail.enabled = false;
            PlayerController.State = GameState.Play;
        }
        else
            counting = true;
    }

    // Update is called once per frame
    void Update()
    {
        textTimer += textSpeed * Time.deltaTime; // increment time

        if (counting && textTimer > 1) // incrementing text
        {
            if (textIndex == 0) // setting up speech bubble, only happens once per phrase
            {
                if (loadedText[0].Value == null || loadedText[0].Value == "")
                    bubble.rectTransform.offsetMax = new Vector2(bubble.rectTransform.offsetMax.x, 11);
                else
                    bubble.rectTransform.offsetMax = new Vector2(bubble.rectTransform.offsetMax.x, 18);

                bubble.enabled = true;
                tail.enabled = true;

                phoneticText.text = loadedText[0].Value;
            }

            ownText.text += loadedText[0].Key[textIndex]; // add next letter
            childText.text = ownText.text;
            textIndex++;

            textTimer = 0; // reset timer
            if (textIndex == loadedText[0].Key.Length) // end of phrase
                counting = false;
        }
    }
}
