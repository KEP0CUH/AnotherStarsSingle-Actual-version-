using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    private PlayerData data;

    private float maxHealth;
    private float health;

    public float Health => health;
    public PlayerData Data => data;

    public PlayerState(PlayerData data)
    {
        this.data = data;
    }

}
