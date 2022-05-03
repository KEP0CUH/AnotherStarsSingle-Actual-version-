using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AsteroidField", fileName = "newAsteroidField", order = 54)]
public class AsteroidFieldData : ScriptableObject
{
    [SerializeField] private string title;

    [SerializeField] private AsteroidFieldType fieldType;
    [SerializeField] private Sprite iconField;
    [SerializeField] private AsteroidData asteroidData;

    public string Title => title;
    public AsteroidFieldType Type => fieldType;
    public Sprite Icon => iconField;
    public AsteroidData AsteroidData => asteroidData;

    private void OnValidate()
    {
        iconField = Resources.Load<Sprite>("Images/Asteroids/AsteroidField");
        string dropPath = $"ScriptableObjects/Asteroids/";
        switch (fieldType)
        {
            case AsteroidFieldType.GoldField:
                title = "Золотое поле астероидов";
                asteroidData = Resources.Load<AsteroidData>(dropPath + "GoldAsteroid");
                break;
            case AsteroidFieldType.FerrumField:
                title = "Железное поле астероидов";
                asteroidData = Resources.Load<AsteroidData>(dropPath + "FerrumAsteroid");
                break;

        }
    }
}
