using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreater : MonoBehaviour {

    public Country countryPrefab;
    public int mapWidth = 0;
    public int mapLength = 0;
    private List<Country> countryList = new List<Country>();
    

    private string[] countryNames = {"Descovania", "Teshoyca", "Caswar","Xoflington","Skole", "Spuebia","Osnein","Ofror", "Skoel Chana","Pleob Drela Oskeilia",
        "Pecreijan", "Bechil","Kuglon","Spaurhiel", "Smuoce","Ofrijan"," Ofrurg", "Gloa Strya","Spoik Smax","Oskeilia" };

    // Use this for initialization
    void Start () {
        //CreateMap();
        //SetOwners();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateMap()
    {
        for (int rows = 0; rows < mapWidth; rows++)
        {
            for (int cols = 0; cols < mapLength; cols++)
            {
                Country country = Instantiate(countryPrefab);
                country.SetName(countryNames[Random.Range(0, countryNames.Length)]);
                country.transform.position = new Vector3(rows * country.transform.localScale.x, 0.25f, country.transform.localScale.z * cols);
                country.transform.parent = this.transform;
                countryList.Add(country);
            }
        }
    }

    private void SetOwners()
    {
        List<Player> playerList = PlayerTurnHandler.GetPlayerList();
        int amountOfLandForEachPlayer = (int)(countryList.Count / PlayerTurnHandler.amountOfPlayers);
        for (int playerNumber = 0; playerNumber < playerList.Count; playerNumber++)
        {
            for (int i = 0; i < amountOfLandForEachPlayer; i++)
            {
                Country country = countryList[i + (playerNumber * playerList.Count)];
                country.SetPlayerOwner(playerList[playerNumber]);
            }
        }
    }
}
