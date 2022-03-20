using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidState : MonoBehaviour
{
    [SerializeField] private AsteroidData data;

    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    public AsteroidData Data => data;

    public void Init(AsteroidData data)
    {
        this.data = data;

        maxHealth = Random.Range(400, 800);
        health = maxHealth;
    }

    public void ChangeHealth(float value)
    {
        if( health + value <= 0)
        {
            health = 0;
            Destroy(this.gameObject);
        }
        health += value;
    }
}
