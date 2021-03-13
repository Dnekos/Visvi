using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CA //cherokee alphabet
{
    A = '\u13A0',
    E = '\u13A1',
    I = '\u13A2',
    O = '\u13A3',
    U = '\u13A4',
    V = '\u13A5',
    GA = '\u13A6',
    KA = '\u13A7',
    GE = '\u13A8',
    GI = '\u13A9',
    GO = '\u13AA',
    GU = '\u13AB',
    GV = '\u13AC',
    HA = '\u13AD',
    HE = '\u13AE',
    HI = '\u13AF',
    HO = '\u13B0',
    HU = '\u13B1',
    HV = '\u13B2',
    LA = '\u13B3',
    LE = '\u13B4',
    LI = '\u13B5',
    LO = '\u13B6',
    LU = '\u13B7',
    LV = '\u13B8',
    MA = '\u13B9',
    ME = '\u13BA',
    MI = '\u13BB',
    MO = '\u13BC',
    MU = '\u13BD',
    NA = '\u13BE',
    HNA = '\u13BF',
    NAH = '\u13C0',
    NE = '\u13C1',
    NI = '\u13C2',
    NO = '\u13C3',
    NU = '\u13C4',
    NV = '\u13C5',
    QUA = '\u13C6',
    QUE = '\u13C7',
    QUI = '\u13C8',
    QUO = '\u13C9',
    QUU = '\u13CA',
    QUV = '\u13CB',
    SA = '\u13CC',
    S = '\u13CD',
    SE = '\u13CE',
    SI = '\u13CF',
    SO = '\u13D0',
    SU = '\u13D1',
    SV = '\u13D2',
    DA = '\u13D3',
    TA = '\u13D4',
    DE = '\u13D5',
    TE = '\u13D6',
    DI = '\u13D7',
    TI = '\u13D8',
    DO = '\u13D9',
    DU = '\u13DA',
    DV = '\u13DB',
    DLA = '\u13DC',
    TLA = '\u13DD',
    TLE = '\u13DE',
    TLI = '\u13DF',
    TLO = '\u13E0',
    TLU = '\u13E1',
    TLV = '\u13E2',
    TSA = '\u13E3',
    TSE = '\u13E4',
    TSI = '\u13E5',
    TSO = '\u13E6',
    TSU = '\u13E7',
    TSV = '\u13E8',
    WA = '\u13E9',
    WE = '\u13EA',
    WI = '\u13EB',
    WO = '\u13EC',
    WU = '\u13ED',
    WV = '\u13EE',
    YA = '\u13EF',
    YE = '\u13F0',
    YI = '\u13F1',
    YO = '\u13F2',
    YU = '\u13F3',
    YV = '\u13F4'
};
public enum CW
{
    cup = CA.U+CA.LI+CA.GU+CA.GU,
    basket = CA.TA+CA.LU+CA.TSA,
    flour = CA.I+CA.SV+CA.WA+CA.NI+CA.GE,
    grapes = CA.U+CA.NI+CA.TE+CA.LV+CA.LA+CA.DI,
}
public class SpeechBubbleManager : MonoBehaviour
{
    // text variables
    public List<KeyValuePair<string,string>> loadedText;
    int textIndex = 0;

    [Header("Timing")]
    [SerializeField]
    float textSpeed = 1;
    float textTimer = 0;
    //[SerializeField]
    //float waitTime = 10;
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
            {
                loadedText.RemoveAt(0);
                textIndex = 0;
                counting = false;
            }
        }
    }
}
