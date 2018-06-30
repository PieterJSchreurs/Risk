using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnHandler : MonoBehaviour
{
    private static List<Player> playerList = new List<Player>();
    public Player playerPrefab;
    public Text currentPlayerText;
    public Text currentModeText;
    public Text reinforcementText;

    public enum GameMode { Reinforcement, Attack }
    public static GameMode CurrentGameMode;
    public static float amountOfPlayers = 2;
    private static int currentPlayerTurn = 0;
    private static Player currentPlayer;
    private GameObject gameSettings;

    //colorListNormal
    //colorListHighlighted

    private Color32[] colorListHighlighted = { new Color32(200, 0, 0, 128), new Color32(0, 200, 0, 128), new Color32(10, 10, 150, 128), new Color32(200, 200, 0, 128),
        new Color32(200, 0, 200, 128), new Color32(0, 200, 200, 128), };

    private Color32[] colorListNormal = { new Color32(255, 0, 0, 255), new Color32(0, 255, 0, 255), new Color32(0, 0, 255, 255), new Color32(255, 255, 0, 255),
        new Color32(255, 0, 255, 255), new Color32(0, 255, 255, 255), };

    // Use this for initialization
    void Awake()
    {
        gameSettings = GameObject.Find("GameSettings");
        GameSetup gameSetup = gameSettings.GetComponent<GameSetup>();
        
        for (int i = 0; i < amountOfPlayers; i++)
        {
            Player player = Instantiate(playerPrefab);
            player.transform.parent = transform;
            player.username = gameSetup.PlayerNames[i];
            player.playerColor = colorListNormal[i];
            player.playerHighLightColor = colorListHighlighted[i];
            playerList.Add(player);
        }
        currentPlayer = playerList[currentPlayerTurn];
        SetActivePlayerText(currentPlayer);
        currentModeText.text = "Current Mode\n" + CurrentGameMode;
    }


    // Update is called once per frame
    void Update()
    {
        if (CurrentGameMode == GameMode.Reinforcement)
        {
            reinforcementText.text = "Reinforcements Left\n" + currentPlayer.reinforcementsAvailable.ToString();
        }
        else
        {
            reinforcementText.text = "Reinforcements Left";
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            HandleNextPlayer();
        }
    }

    public void SetActivePlayerText(Player pPlayer)
    {
        currentPlayerText.text = "Players turn\n" + pPlayer.username;
    }

    public void HandleNextPlayer()
    {
        if (CurrentGameMode == GameMode.Reinforcement)
        {
            if (currentPlayer == playerList[playerList.Count - 1])
            { CurrentGameMode = GameMode.Attack; }
        }
        else
        {
            if (currentPlayer == playerList[playerList.Count - 1])
            { CurrentGameMode = GameMode.Reinforcement; }
        }
        NextPlayer();
        SetActivePlayerText(currentPlayer);
        currentModeText.text = "Current Mode\n" + CurrentGameMode;
        currentPlayer.reinforcementsAvailable = currentPlayer.GetReinforcementsAvailable();
    }

    public static void NextPlayer()
    {
        currentPlayerTurn++;
        if (currentPlayerTurn == playerList.Count)
        {
            currentPlayerTurn = 0;
        }
        currentPlayer = playerList[currentPlayerTurn == -1 ? 0 : currentPlayerTurn % playerList.Count];
        
    }

    public static Player GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public static List<Player> GetPlayerList()
    {
        return playerList;
    }
}
