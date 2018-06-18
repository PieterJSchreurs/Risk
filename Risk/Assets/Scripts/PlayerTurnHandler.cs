using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnHandler : MonoBehaviour {

    private static List<Player> playerList = new List<Player>();
    public Player playerPrefab;

    public static float amountOfPlayers = 4;
    private static int currentPlayerTurn = 0;
    private static Player currentPlayer;


    private Color32[] colorListNormal = { new Color32(200, 0, 0, 128), new Color32(0, 200, 0, 128), new Color32(10, 10, 150, 128), new Color32(200, 200, 0, 128),
        new Color32(200, 0, 200, 128), new Color32(0, 200, 200, 128), };

    private Color32[] colorListHighlighted = { new Color32(255, 0, 0, 255), new Color32(0, 255, 0, 255), new Color32(0, 0, 255, 255), new Color32(255, 255, 0, 255),
        new Color32(255, 0, 255, 255), new Color32(0, 255, 255, 255), };

    // Use this for initialization
    void Awake () {
        for (int i = 0; i < amountOfPlayers; i++)
        {
            Player player = Instantiate(playerPrefab);
            player.transform.parent = transform;
            player.username = "Player" + i.ToString();
            player.playerColor = colorListNormal[i];
            player.playerHighLightColor = colorListHighlighted[i];
            playerList.Add(player);
        }
        currentPlayer = playerList[currentPlayerTurn];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void NextPlayer()
    {
        currentPlayerTurn++;
        currentPlayer = playerList[currentPlayerTurn == -1 ? 0 : currentPlayerTurn % playerList.Count];
        Debug.Log("Current player is:" + currentPlayer.username);
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
