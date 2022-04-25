using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    private PlayerData data;
    [SerializeField] private ShipState ship;

    private float maxHealth;
    private float health;

    public float Health => health;
    public PlayerData Data => data;
    public ShipState Ship => ship;

    public PlayerState(PlayerData data,ShipState playerShip)
    {
        this.data = data;
        this.ship = playerShip;
    }

    public void ChangeHealth(int value)
    {
        health += value;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
        }

        Debug.Log($"CurrentHealth: {health} / {maxHealth}");
    }

    public void SetShip(ShipKind kind)
    {
        this.ship = this.ship.Init(kind);
        Managers.Player.Controller.UpdateState();
    }
}
