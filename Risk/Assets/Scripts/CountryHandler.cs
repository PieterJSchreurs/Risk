using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryHandler : MonoBehaviour {

    private Country selectedCountry;
    public Text currentSelectedText;
    public Text armyOnSelectedCountry;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetSelectedCountryText(Country pCountry)
    {
        currentSelectedText.text = "Country Selected:\n" + pCountry.GetName();
    }

    public Army GetArmyOnSelectedCountry(Country pCountry)
    {
        Army army = pCountry.getArmy();
        armyOnSelectedCountry.text = army.amountOfSoldiers.ToString();
        return army;
    }
  
    public void SetSelectedCountry(Country pCountry)
    {
        selectedCountry = pCountry;
        SetSelectedCountryText(selectedCountry);
    }
}
