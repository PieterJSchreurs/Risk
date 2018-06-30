using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelCreater : MonoBehaviour
{

    private List<Country> countryList = new List<Country>();
    private int amountOfLandForEachPlayer;
    private int oddLandLeft;
    private List<Player> playerList;

    public void Start()
    {
        SetUpLists();
        amountOfLandForEachPlayer = (int)(countryList.Count / PlayerTurnHandler.amountOfPlayers);
        oddLandLeft = (int)(countryList.Count % PlayerTurnHandler.amountOfPlayers);

        playerList = PlayerTurnHandler.GetPlayerList();
        SetOwners();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetUpLists()
    {
        Transform[] allChildren = this.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.gameObject.tag == "Country")
            { countryList.Add(child.GetComponent<Country>()); }
        }
        countryList = countryList.OrderBy(country => Random.Range(0, 5)).ToList();

    }

    private void SetOwners()
    {
        //Debug.Log(playerList.Count + " " + countryList.Count + " " + amountOfLandForEachPlayer );
        for (int playerNumber = 0; playerNumber < playerList.Count; playerNumber++)
        {
            for (int i = 0; i < amountOfLandForEachPlayer; i++)
            {
                Country country = countryList[i + (playerNumber * amountOfLandForEachPlayer)];
               // Debug.Log(country.countryName);
               //// Debug.Log("Setting player owner");
                country.SetPlayerOwner(playerList[playerNumber]);
            }
            if (oddLandLeft != 0)
            {
                Country country = countryList[(countryList.Count) - oddLandLeft];
                country.SetPlayerOwner(playerList[playerNumber]);
                oddLandLeft--;
            }
        }
    }
}
