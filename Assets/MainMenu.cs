using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;
    
public class MainMenu : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    void Start()
    {
        actions.Add("play", PlayGame);
        actions.Add("quit", Quit);
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
    public void PlayGame()
    {
        Debug.Log("Loading");
        SceneManager.LoadSceneAsync("SampleScene");

    }
    public void Quit()
    {
        Application.Quit(); 
    }
}
