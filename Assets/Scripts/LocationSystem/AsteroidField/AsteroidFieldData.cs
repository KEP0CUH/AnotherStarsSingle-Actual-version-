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
                title = "������� ���� ����������";
                asteroidData = Resources.Load<AsteroidData>(dropPath + AsteroidType.GoldAsteroid);
                break;
            case AsteroidFieldType.FerrumField:
                title = "�������� ���� ����������";
                asteroidData = Resources.Load<AsteroidData>(dropPath + AsteroidType.FerrumAsteroid);
                break;
            case AsteroidFieldType.TitanField:
                title = "�������� ���� ����������";
                asteroidData = Resources.Load<AsteroidData>(dropPath + AsteroidType.TitanAsteroid);
                break;
            case AsteroidFieldType.MineralField:
                title = "����������� ���� ����������";
                asteroidData = Resources.Load<AsteroidData>(dropPath + AsteroidType.MineralAsteroid);
                break;
            case AsteroidFieldType.OrganicField:
                title = "������������ ���� ����������";
                asteroidData = Resources.Load<AsteroidData>(dropPath + AsteroidType.OrganicAsteroid);
                break;
            case AsteroidFieldType.OsmiumField:
                title = "�������� ���� ����������";
                asteroidData = Resources.Load<AsteroidData>(dropPath + AsteroidType.OsmiumAsteroid);
                break;
        }
    }
}
