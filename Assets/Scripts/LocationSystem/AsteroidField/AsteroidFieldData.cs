using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AsteroidFields/AsteroidField", fileName = "newAsteroidField", order = 52)]
public class AsteroidFieldData : ScriptableObject
{
    [SerializeField] private string title;

    [SerializeField] private AsteroidFieldType type;
    [SerializeField] private Sprite iconField;
    [SerializeField] private string iconPath = "Images/Asteroids/AsteroidField";
    [SerializeField] private AsteroidData asteroidData;

    public string Title => title;
    public AsteroidFieldType Type => type;
    public Sprite Icon => iconField;
    public AsteroidData AsteroidData => asteroidData;

    private void OnValidate()
    {
        iconField = Resources.Load<Sprite>(iconPath);
        string dropPath = $"ScriptableObjects/Asteroids/";
        switch (type)
        {
            case AsteroidFieldType.GoldField:
                title = "Золотое поле астероидов";
                asteroidData = Resources.Load<AsteroidData>(dropPath + AsteroidType.GoldAsteroid);
                break;
            case AsteroidFieldType.FerrumField:
                title = "Железное поле астероидов";
                asteroidData = Resources.Load<AsteroidData>(dropPath + AsteroidType.FerrumAsteroid);
                break;
            case AsteroidFieldType.TitanField:
                title = "Титаново поле астероидов";
                asteroidData = Resources.Load<AsteroidData>(dropPath + AsteroidType.TitanAsteroid);
                break;
            case AsteroidFieldType.MineralField:
                title = "Минеральное поле астероидов";
                asteroidData = Resources.Load<AsteroidData>(dropPath + AsteroidType.MineralAsteroid);
                break;
            case AsteroidFieldType.OrganicField:
                title = "Органическое поле астероидов";
                asteroidData = Resources.Load<AsteroidData>(dropPath + AsteroidType.OrganicAsteroid);
                break;
            case AsteroidFieldType.OsmiumField:
                title = "Осмиевое поле астероидов";
                asteroidData = Resources.Load<AsteroidData>(dropPath + AsteroidType.OsmiumAsteroid);
                break;
        }
    }
}
