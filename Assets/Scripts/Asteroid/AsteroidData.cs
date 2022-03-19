using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidData : MonoBehaviour
{
    private string title;
    private string description;
    private AsteroidType type;
    private Sprite icon;

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
        if(health - value <= 0)
        {
            health = 0;
            Destroy(this.gameObject);
        }
        health = health - value;
    }

    private void OnValidate()
    {
        switch(type)
        {
            case AsteroidType.AsteroidGold:
                title = "GoldAsteroid";
                description = "Gold asteroid from Russia *_*.";
                icon = Resources.Load<Sprite>("Images/Asteroids/GoldAsteroid");
                break;
            case AsteroidType.AsteroidFerrum:
                title = "FerrumAsteroid";
                description = "Ferrum asteroid from Russia *)";
                icon = Resources.Load<Sprite>("Images/Asteroid/GoldAsteroid");
                break;

        }
    }

    [ContextMenu("OnDestroy")]
    private void OnDestroy()
    {
        GameObject drop = new GameObject("Item",typeof(CollectableItemData));

        var data = Resources.Load<BaseScriptableItemData>("ScriptableObjects/Gold");

        drop.AddComponent<ItemViewGame>().Init(data);
    }

    

    
}
