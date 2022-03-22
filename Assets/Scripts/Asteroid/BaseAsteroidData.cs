using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName="Asteroids",fileName ="New Asteroid",order = 52)]
public class BaseAsteroidData : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private AsteroidType type;
    [SerializeField] private Sprite icon;
    [SerializeField] private ItemKind dropKind;

    public string Title => title;
    public string Description => description;
    public AsteroidType Type => type;
    public Sprite Icon => icon;
    public ItemKind DropKind => dropKind;

    public void Init(AsteroidType asteroidType)
    {
        type = asteroidType;
        OnValidate();
    }

    private void OnValidate()
    {
        switch (type)
        {
            case AsteroidType.GoldAsteroid:
                title = "GoldAsteroid";
                description = "��������, ���������� � ���� �� 5% ������.";
                icon = Resources.Load<Sprite>("Images/Asteroids/" + AsteroidType.GoldAsteroid.ToString());
                dropKind = ItemKind.rudaGold;
                break;
            case AsteroidType.FerrumAsteroid:
                title = "FerrumAsteroid";
                description = "�������� � ��������� ������.";
                icon = Resources.Load<Sprite>("Images/Asteroids/" + AsteroidType.FerrumAsteroid.ToString());
                dropKind = ItemKind.rudaFerrum;
                break;

        }
    }
}
