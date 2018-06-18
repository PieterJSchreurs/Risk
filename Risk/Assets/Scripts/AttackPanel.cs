using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPanel : MonoBehaviour {

    public Text statusText;
    public Text outcomeText;
    private Country currentSelectedCountry;
    private Country currentAttackedCountry;
    System.Random rnd = new System.Random();


    private void Awake()
    {
        
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attack()
    {
        int player1Roll = rnd.Next(1,6);
        int player2Roll = rnd.Next(1,6);
        outcomeText.text = currentSelectedCountry.GetPlayerOwner().username + " rolled a " + player1Roll + ",  " + currentAttackedCountry.GetPlayerOwner().username + " rolled a " + player2Roll;

    }

    public void StartAttack(Country pCurrentSelectCountry, Country pCurrentAttackedCountry)
    {
        currentSelectedCountry = pCurrentSelectCountry;
        currentAttackedCountry = pCurrentAttackedCountry;
        statusText.text = "This is a battle between " + pCurrentSelectCountry.GetPlayerOwner().username + " and " + pCurrentAttackedCountry.GetPlayerOwner().username + " on the country of " + pCurrentAttackedCountry.GetName();
    }
}
