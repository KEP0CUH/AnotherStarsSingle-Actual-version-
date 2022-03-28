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

    public PlayerState(PlayerData data)
    {
        this.data = data;
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

    public void ChangeShip(ShipState ship)
    {
        this.ship = ship;
    }

    public void ChangeGun(GunState gun)
    {
        this.ship.SetGun(gun);
    }

}
