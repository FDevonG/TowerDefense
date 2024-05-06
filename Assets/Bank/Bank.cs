using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;

    TextMeshProUGUI goldtext;

    int currentBalance;
    public int CurrentBalance
    {
        get 
        { 
            return currentBalance; 
        }
        private set 
        { 
            currentBalance = value;
        }
    }

    void Awake()
    {
        CurrentBalance = startingBalance;
        goldtext = GameObject.Find("GoldUI").GetComponent<TextMeshProUGUI>();
        DisplayBalance();
    }

    public void Deposit(int amount)
    {
        CurrentBalance += (int)MathF.Abs(amount);
        DisplayBalance();
    }

    public void Withdraw(int amount)
    {
        if(CurrentBalance >= amount)
        {
            CurrentBalance -= (int)MathF.Abs(amount);
            DisplayBalance();
        }
    }

    void DisplayBalance()
    {
        goldtext.text = $"Gold: {CurrentBalance}";
    }
}
