using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;

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
    }

    public void Deposit(int amount)
    {
        CurrentBalance += (int)MathF.Abs(amount);
    }

    public void Withdraw(int amount)
    {
        if(CurrentBalance >= amount)
        {
            CurrentBalance -= (int)MathF.Abs(amount);
        }
    }
}
