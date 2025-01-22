using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.Windows.Speech;
public class gameControl : MonoBehaviour
{
    private static GameObject whoWinsTextShadow, player1MoveText, player2MoveText;
    private static GameObject player1, player2;
    public static int diceSideThrown = 0;
    public static int player1StartWaypoint = 0;
    public static int player2StartWaypoint = 0;
    public static bool gameOver = false;
    public Button replay;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    void Start()
    {
        diceSideThrown = 0;
        player1StartWaypoint = 0;
        player2StartWaypoint = 0;
        gameOver = false;
        whoWinsTextShadow = GameObject.Find("WhoWinsText");
        player1MoveText = GameObject.Find("Player1MoveText");
        player2MoveText = GameObject.Find("Player2MoveText");
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        player1.GetComponent<Logic1>().moveAllowed = false;
        player2.GetComponent<Logic1>().moveAllowed = false;
        whoWinsTextShadow.gameObject.SetActive(false);
        player1MoveText.gameObject.SetActive(true);
        player2MoveText.gameObject.SetActive(false);
        replay.gameObject.SetActive(false);

        replay.onClick.AddListener(ResetGame);
        actions.Add("Replay", ResetGame);
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
    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            replay.gameObject.SetActive(true);
            
            return;
        }
        if (Dice.whosTurn == 1)
        {
            player1MoveText.gameObject.SetActive(true);
            player2MoveText.gameObject.SetActive(false);
        }
        else if (Dice.whosTurn == 2)
        {
            player1MoveText.gameObject.SetActive(false);
            player2MoveText.gameObject.SetActive(true);
        }
        if (player1.GetComponent<Logic1>().waypointIndex > player1StartWaypoint + diceSideThrown)
        {
            if (player1StartWaypoint + diceSideThrown == 6)
            {
                player1.GetComponent<Logic1>().transform.position = player1.GetComponent<Logic1>().waypoints[35].transform.position;
                player1.GetComponent<Logic1>().waypointIndex = 35;
                player1.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player1StartWaypoint + diceSideThrown == 20)
            {
                player1.GetComponent<Logic1>().transform.position = player1.GetComponent<Logic1>().waypoints[57].transform.position;
                player1.GetComponent<Logic1>().waypointIndex = 57;
                player1.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player1StartWaypoint + diceSideThrown == 30)
            {
                player1.GetComponent<Logic1>().transform.position = player1.GetComponent<Logic1>().waypoints[50].transform.position;
                player1.GetComponent<Logic1>().waypointIndex = 50;
                player1.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player1StartWaypoint + diceSideThrown == 32)
            {
                player1.GetComponent<Logic1>().transform.position = player1.GetComponent<Logic1>().waypoints[4].transform.position;
                player1.GetComponent<Logic1>().waypointIndex = 4;
                player1.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player1StartWaypoint + diceSideThrown == 33)
            {
                player1.GetComponent<Logic1>().transform.position = player1.GetComponent<Logic1>().waypoints[83].transform.position;
                player1.GetComponent<Logic1>().waypointIndex = 83;
                player1.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player1StartWaypoint + diceSideThrown == 42)
            {
                player1.GetComponent<Logic1>().transform.position = player1.GetComponent<Logic1>().waypoints[23].transform.position;
                player1.GetComponent<Logic1>().waypointIndex = 23;
                player1.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player1StartWaypoint + diceSideThrown == 53)
            {
                player1.GetComponent<Logic1>().transform.position = player1.GetComponent<Logic1>().waypoints[88].transform.position;
                player1.GetComponent<Logic1>().waypointIndex = 88;
                player1.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player1StartWaypoint + diceSideThrown == 62)
            {
                player1.GetComponent<Logic1>().transform.position = player1.GetComponent<Logic1>().waypoints[81].transform.position;
                player1.GetComponent<Logic1>().waypointIndex = 81;
                player1.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player1StartWaypoint + diceSideThrown == 65)
            {
                player1.GetComponent<Logic1>().transform.position = player1.GetComponent<Logic1>().waypoints[11].transform.position;
                player1.GetComponent<Logic1>().waypointIndex = 11;
                player1.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player1StartWaypoint + diceSideThrown == 95)
            {
                player1.GetComponent<Logic1>().transform.position = player1.GetComponent<Logic1>().waypoints[71].transform.position;
                player1.GetComponent<Logic1>().waypointIndex = 71;
                player1.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            player1.GetComponent<Logic1>().moveAllowed = false;
            player1StartWaypoint = player1.GetComponent<Logic1>().waypointIndex - 1;
        }
        if (player2.GetComponent<Logic1>().waypointIndex > player2StartWaypoint + diceSideThrown)
        {
            if (player2StartWaypoint + diceSideThrown == 6)
            {
                player2.GetComponent<Logic1>().transform.position = player2.GetComponent<Logic1>().waypoints[35].transform.position;
                player2.GetComponent<Logic1>().waypointIndex = 35;
                player2.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player2StartWaypoint + diceSideThrown == 20)
            {
                player2.GetComponent<Logic1>().transform.position = player2.GetComponent<Logic1>().waypoints[57].transform.position;
                player2.GetComponent<Logic1>().waypointIndex = 57;
                player2.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player2StartWaypoint + diceSideThrown == 30)
            {
                player2.GetComponent<Logic1>().transform.position = player2.GetComponent<Logic1>().waypoints[50].transform.position;
                player2.GetComponent<Logic1>().waypointIndex = 50;
                player2.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player2StartWaypoint + diceSideThrown == 32)
            {
                player2.GetComponent<Logic1>().transform.position = player2.GetComponent<Logic1>().waypoints[4].transform.position;
                player2.GetComponent<Logic1>().waypointIndex = 4;
                player2.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player2StartWaypoint + diceSideThrown == 33)
            {
                player2.GetComponent<Logic1>().transform.position = player2.GetComponent<Logic1>().waypoints[83].transform.position;
                player2.GetComponent<Logic1>().waypointIndex = 83;
                player2.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player2StartWaypoint + diceSideThrown == 42)
            {
                player2.GetComponent<Logic1>().transform.position = player2.GetComponent<Logic1>().waypoints[23].transform.position;
                player2.GetComponent<Logic1>().waypointIndex = 23;
                player2.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player2StartWaypoint + diceSideThrown == 53)
            {
                player2.GetComponent<Logic1>().transform.position = player2.GetComponent<Logic1>().waypoints[88].transform.position;
                player2.GetComponent<Logic1>().waypointIndex = 88;
                player2.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player2StartWaypoint + diceSideThrown == 62)
            {
                player2.GetComponent<Logic1>().transform.position = player2.GetComponent<Logic1>().waypoints[81].transform.position;
                player2.GetComponent<Logic1>().waypointIndex = 81;
                player2.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player2StartWaypoint + diceSideThrown == 65)
            {
                player2.GetComponent<Logic1>().transform.position = player2.GetComponent<Logic1>().waypoints[11].transform.position;
                player2.GetComponent<Logic1>().waypointIndex = 11;
                player2.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            if (player2StartWaypoint + diceSideThrown == 95)
            {
                player2.GetComponent<Logic1>().transform.position = player2.GetComponent<Logic1>().waypoints[71].transform.position;
                player2.GetComponent<Logic1>().waypointIndex = 71;
                player2.GetComponent<Logic1>().waypointIndex += 1;
                MovePlayer(1);
            }
            player2.GetComponent<Logic1>().moveAllowed = false;
            player2StartWaypoint = player2.GetComponent<Logic1>().waypointIndex - 1;
        }
        if (player1.GetComponent<Logic1>().waypointIndex == 99)
        {
            whoWinsTextShadow.gameObject.SetActive(true);
            whoWinsTextShadow.GetComponent<TMP_Text>().text = "Player 1 Wins";
            gameOver = true;
        }

        if (player2.GetComponent<Logic1>().waypointIndex ==
            99)
        {
            whoWinsTextShadow.gameObject.SetActive(true);
            whoWinsTextShadow.GetComponent<TMP_Text>().text = "Player 2 Wins";
            gameOver = true;
        }
    }
    private void ResetGame()
    {
        diceSideThrown = 0;
        player1StartWaypoint = 0;
        player2StartWaypoint = 0;
        gameOver = false;
        whoWinsTextShadow.gameObject.SetActive(false);
        player1MoveText.gameObject.SetActive(true);
        player2MoveText.gameObject.SetActive(false);
        replay.gameObject.SetActive(false);

        player1.GetComponent<Logic1>().waypointIndex = 0;
        player2.GetComponent<Logic1>().waypointIndex = 0;
        player1.GetComponent<Logic1>().transform.position = player1.GetComponent<Logic1>().waypoints[0].transform.position;
        player2.GetComponent<Logic1>().transform.position = player2.GetComponent<Logic1>().waypoints[0].transform.position;
    }
    public static void MovePlayer(int playerToMove)
    {
        switch (playerToMove)
        {
            case 1:
                player1.GetComponent<Logic1>().moveAllowed = true;
                break;
            case 2:
                player2.GetComponent<Logic1>().moveAllowed = true;
                break;
        }
    }
}
