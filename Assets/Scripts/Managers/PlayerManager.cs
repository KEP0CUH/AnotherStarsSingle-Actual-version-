using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int currentHealth;


    public ManagerStatus Status { get; private set; }

    public void Startup()
    {
        Debug.Log("Inventory manager starting...");

        maxHealth = 100;
        currentHealth = 80;



        Status = ManagerStatus.Started;
    }

    public void ChangeHealth(int value)
    {
        currentHealth += value;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if(currentHealth < 0)
        {
            currentHealth = 0;
        }

        Debug.Log($"CurrentHealth: {currentHealth} / {maxHealth}");
    }
}
