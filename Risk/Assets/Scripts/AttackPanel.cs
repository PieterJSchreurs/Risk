using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPanel : MonoBehaviour
{

    public Text statusText;
    public Text battleText;
    private Country currentSelectedCountry;
    private Country currentAttackedCountry;
    System.Random rnd = new System.Random();
    public Text armyAttackingText;
    public Text armyDefendingText;
    private bool canAttack = true;
    public Text attackerDice;
    public Text defenderDice;
    private int attackerRolls = 1;
    private int defenderRolls = 1;

    private void Awake()
    {
        statusText.text = "";
        battleText.text = "";
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
    public void AddDefenderDice()
    {
        //Check if defender has more than 2.
        if (defenderRolls == 2)
        {

        }
        else
        {
            if (currentAttackedCountry.getArmy().amountOfSoldiers > defenderRolls)
            { defenderRolls++; }

        }
        defenderDice.text = defenderRolls.ToString();
    }

    public void DecreaseDefenderDice()
    {
        if (defenderRolls == 1)
        {

        }
        else
        {
            defenderRolls--;
        }
        defenderDice.text = defenderRolls.ToString();
    }

    public void AddAttackDice()
    {
        //Check if army has more than 4.
        if (attackerRolls == 3)
        {

        }
        else
        {
            if (currentSelectedCountry.getArmy().amountOfSoldiers > attackerRolls)
            { attackerRolls++; }
        }
        attackerDice.text = attackerRolls.ToString();
    }

    public void DecreaseAttackDice()
    {
        if (attackerRolls == 1)
        {

        }
        else
        {
            attackerRolls--;
        }
        attackerDice.text = attackerRolls.ToString();
    }

    public void Attack()
    {
        if (canAttack == true)
        {
            string outcomeText = "";
            int[] attackerRollsArray = new int[attackerRolls];
            int[] defenderRollsArray = new int[defenderRolls];
            for (int i = 0; i < attackerRolls; i++)
            {
                attackerRollsArray[i] = rnd.Next(1, 6);
            }
            for (int i = 0; i < defenderRolls; i++)
            {
                defenderRollsArray[i] = rnd.Next(1, 6);
            }
            for (int roll = 0; roll < attackerRollsArray.Length; roll++)
            {
                outcomeText += currentSelectedCountry.GetPlayerOwner().username + " rolled a " + attackerRollsArray[roll] + ".\n";
            }
            for (int roll = 0; roll < defenderRollsArray.Length; roll++)
            {
                outcomeText += currentAttackedCountry.GetPlayerOwner().username + " rolled a " + defenderRollsArray[roll] + ".\n";
            }
            battleText.text = outcomeText;
            HandleAttack(attackerRollsArray, defenderRollsArray, currentSelectedCountry, currentAttackedCountry);
            if (currentSelectedCountry.getArmy().amountOfSoldiers <= 0)
            {
                canAttack = false;
            }
        }
        else
        {
            battleText.text += "\nNot enough manpower to attack.";
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
        }
        else
        {
            canAttack = true;
        }
    }

    public void SetArmyLeftText(Army attacker, Army defender)
    {
        armyAttackingText.text = "Attacker:\n" + attacker.amountOfSoldiers.ToString();
        armyDefendingText.text = "Defender:\n" + defender.amountOfSoldiers.ToString();
    }

    private void HandleAttack(int[] pPlayer1Rolls, int[] pPlayer2Rolls, Country attacker, Country defender)
    {
        Army attackerArmy = attacker.getArmy();
        Army defendingArmy = defender.getArmy();

        Array.Sort(pPlayer1Rolls);
        Array.Reverse(pPlayer1Rolls);
        Array.Sort(pPlayer2Rolls);
        Array.Reverse(pPlayer2Rolls);

        for (int i = 0; i < pPlayer2Rolls.Length; i++)
        {
            try
            {
                if (pPlayer1Rolls[i] <= pPlayer2Rolls[i])
                { attacker.getArmy().amountOfSoldiers--; }
                else
                { defender.getArmy().amountOfSoldiers--; }
            } catch
            { }
        }

        if (attackerArmy.amountOfSoldiers <= 0)
        {
            attackerArmy.amountOfSoldiers = 0;
            canAttack = false;
        }

        if (defendingArmy.amountOfSoldiers <= 0)
        {
            defendingArmy.amountOfSoldiers = 0;
            AttackSucceeded(attacker, defender);
        }
        SetArmyLeftText(attackerArmy, defendingArmy);
    }

    private void AttackSucceeded(Country pAttackerCountry, Country pDefenderCountry)
    {
        Player defenderPlayer = pDefenderCountry.GetPlayerOwner();
        defenderPlayer.countryList.Remove(pDefenderCountry);
        pDefenderCountry.SetPlayerOwner(pAttackerCountry.GetPlayerOwner());
        pAttackerCountry.getArmy().amountOfSoldiers -= attackerRolls;
        pDefenderCountry.getArmy().amountOfSoldiers += attackerRolls;
        this.transform.gameObject.SetActive(false);

    }
}
