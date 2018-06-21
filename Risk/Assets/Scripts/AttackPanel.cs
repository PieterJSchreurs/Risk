using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPanel : MonoBehaviour
{

    public Text statusText;
    public Text outcomeText;
    private Country currentSelectedCountry;
    private Country currentAttackedCountry;
    System.Random rnd = new System.Random();
    public Text armyAttackingText;
    public Text armyDefendingText;
    private bool canAttack = true;

    private void Awake()
    {
        statusText.text = "";
        outcomeText.text = "";
        armyAttackingText.text = "Attacker:";
        armyDefendingText.text = "Defender:";
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        if (canAttack == true)
        {
            int player1Roll = rnd.Next(1, 6);
            int player2Roll = rnd.Next(1, 6);
            outcomeText.text = currentSelectedCountry.GetPlayerOwner().username + " rolled a " + player1Roll + ",  " + currentAttackedCountry.GetPlayerOwner().username + " rolled a " + player2Roll;

            HandleAttack(player1Roll, player2Roll, currentSelectedCountry, currentAttackedCountry);
            if(currentSelectedCountry.getArmy().amountOfSoldiers <= 0)
            {
                canAttack = false;
            }
        } else
        {
            outcomeText.text = "Not enough manpower to attack.";
        }


    }

    public void StartAttack(Country pCurrentSelectCountry, Country pCurrentAttackedCountry)
    {
        currentSelectedCountry = pCurrentSelectCountry;
        currentAttackedCountry = pCurrentAttackedCountry;
        statusText.text = "This is a battle between " + pCurrentSelectCountry.GetPlayerOwner().username + " and " + pCurrentAttackedCountry.GetPlayerOwner().username + " on the country of " + pCurrentAttackedCountry.GetName();
        SetArmyLeftText(currentSelectedCountry.getArmy(), currentAttackedCountry.getArmy());
        if (pCurrentSelectCountry.getArmy().amountOfSoldiers <= 0)
        {
            canAttack = false;
        } else
        {
            canAttack = true;
        }
    }

    public void SetArmyLeftText(Army attacker, Army defender)
    {
        armyAttackingText.text = "Attacker:\n" + attacker.amountOfSoldiers.ToString();
        armyDefendingText.text = "Defender:\n" + defender.amountOfSoldiers.ToString();
    }

    private void HandleAttack(int pPlayer1Roll, int pPlayer2Roll, Country attacker, Country defender)
    {
        Army attackerArmy = attacker.getArmy();
        Army defendingArmy = defender.getArmy();

        int armySizeAttacker = attackerArmy.amountOfSoldiers;
        int armySizeDefender = defendingArmy.amountOfSoldiers;

        attackerArmy.amountOfSoldiers -= ((armySizeDefender / 10) * (pPlayer2Roll + 1));
        defendingArmy.amountOfSoldiers -= ((armySizeAttacker / 10) * pPlayer1Roll);

        if(attackerArmy.amountOfSoldiers <= 0)
        {
            attackerArmy.amountOfSoldiers = 0;
            canAttack = false;
        }

        if(defendingArmy.amountOfSoldiers <= 0)
        {
            defendingArmy.amountOfSoldiers = 0;
            AttackSucceeded(attacker, defender);
        }
        SetArmyLeftText(attackerArmy, defendingArmy);
    }

    private void AttackSucceeded(Country pAttackerCountry, Country pDefenderCountry)
    {
        pDefenderCountry.SetPlayerOwner(pAttackerCountry.GetPlayerOwner());
        this.transform.gameObject.SetActive(false);
        
    }
}
