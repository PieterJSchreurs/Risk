using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public string username;
    public Color playerColor;
    public Color playerHighLightColor;
    public List<Country> countryList = new List<Country>();
    public int reinforcementsAvailable = 4;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetReinforcementsAvailable()
    {
        if(GetAmountOfCountries() /2 >= 4)
        {
            return GetAmountOfCountries() / 2;
        } else
        { return reinforcementsAvailable; }
        
    }

    public int GetAmountOfCountries()
    {
        return countryList.Count;
    }


}
