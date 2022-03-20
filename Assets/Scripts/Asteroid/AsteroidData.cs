using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidData : MonoBehaviour
{
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private AsteroidType type;
    [SerializeField] private Sprite icon;
    [SerializeField] private string dropName;

    public string Title => title;
    public string Description => Description;
    public AsteroidType Type => type;
    public Sprite Icon => icon;



    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    public void Init(AsteroidType asteroidType)
    {
        type = asteroidType;
        maxHealth = 840;
        health = maxHealth;

        OnValidate();
    }

    public void ChangeHealth(float value)
    {
        if(health + value <= 0)
        {
            health = 0;
            Destroy(this.gameObject);
        }
        health = health + value;
    }

    private void OnValidate()
    {
        switch(type)
        {
            case AsteroidType.AsteroidGold:
                title = "GoldAsteroid";
                description = "Gold asteroid from Russia *_*.";
                icon = Resources.Load<Sprite>("Images/Asteroids/GoldAsteroid");
                dropName = "Gold";
                break;
            case AsteroidType.AsteroidFerrum:
                title = "FerrumAsteroid";
                description = "Ferrum asteroid from Russia *)";
                icon = Resources.Load<Sprite>("Images/Asteroid/GoldAsteroid");
                dropName = "Ferrum";
                break;

        }
    }

    [ContextMenu("OnDestroy")]
    private void OnDestroy()
    {
        if(gameObject.scene.isLoaded)
        {
            GameObject drop = new GameObject("Item: " + dropName);
            drop.transform.position = this.gameObject.transform.position;

            var data = Resources.Load<BaseItemData>($"ScriptableObjects/" + dropName);

            drop.AddComponent<ItemViewGame>().Init(data, 4);
        }
    }

    

    
}
