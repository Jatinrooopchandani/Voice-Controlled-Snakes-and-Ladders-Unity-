using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Windows.Speech;
public class Dice : MonoBehaviour
{
    // Start is called before the first frame update
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    public static int whosTurn = 1;
    private bool coRoutineAllowed = true;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    void Start()
    {
        actions.Add("roll", Roll);
        Debug.Log("Actions added: " + string.Join(", ", actions.Keys));

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        if (keywordRecognizer != null)
        {
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
            keywordRecognizer.Start();
            Debug.Log("KeywordRecognizer started.");
        }
        else
        {
            Debug.LogError("KeywordRecognizer is null.");
        }
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        rend.sprite = diceSides[3];
    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log($"Recognized speech: {speech.text}, Confidence: {speech.confidence}");

        if (actions.ContainsKey(speech.text))
        {
            Debug.Log("Action found for: " + speech.text);
            actions[speech.text].Invoke();
        }
        else
        {
            Debug.LogWarning("No action found for: " + speech.text);
        }
    }
    private void Roll()
    {
        Debug.Log("Rolling");
        if (!gameControl.gameOver && coRoutineAllowed)
            StartCoroutine("RollTheDice");
    }
    private void OnMouseDown()
    {
        Debug.Log("Rolling");
        if (!gameControl.gameOver && coRoutineAllowed)
            StartCoroutine("RollTheDice");
    }
    private IEnumerator RollTheDice()
    {
        coRoutineAllowed = false;
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = UnityEngine.Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.01f);
        }

        gameControl.diceSideThrown = randomDiceSide + 1;
        if (randomDiceSide == 5)
        {
            if (whosTurn == 1)
            {
                whosTurn = 1;
                gameControl.MovePlayer(1);
            }
            else if (whosTurn == 2)
            {
                whosTurn = 2;
                gameControl.MovePlayer(2);
            }
            coRoutineAllowed = true;
        }
        else
        {
            if (whosTurn == 1)
            {
                whosTurn = 2;
                gameControl.MovePlayer(1);

            }
            else if (whosTurn == 2)
            {
                whosTurn = 1;
                gameControl.MovePlayer(2);

            }
        }
        coRoutineAllowed = true;
    }
        // Update is called once per frame
        void Update()
    {
        
    }
    
}
