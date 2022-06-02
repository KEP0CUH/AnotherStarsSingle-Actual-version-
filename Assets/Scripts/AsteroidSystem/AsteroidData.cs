using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/Asteroids/NewAsteroid",fileName ="New Asteroid",order = 50)]
public class AsteroidData : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private AsteroidType type;
    [SerializeField] private Sprite icon;
    [SerializeField] private string iconPath = "Images/Asteroids/Asteroid";
    [SerializeField] private ItemKind dropKind;

    [Header("LIFE_PARAMETERS")]
    [SerializeField] [Range(100, 10000)] private int minHealth = 400;
    [SerializeField] [Range(100, 10000)] private int maxHealth = 900;

    [Header("MOVE_PARAMETERS")]
    [SerializeField] private float moveSpeedMin = 2.0f / Constants.TICKS_PER_SEC;
    [SerializeField] private float moveSpeedMax = 3.0f / Constants.TICKS_PER_SEC;

    public string Title => title;
    public string Description => description;
    public AsteroidType Type => type;
    public Sprite Icon => icon;
    public ItemKind DropKind => dropKind;

    public int MaxHealth => maxHealth;
    public int MinHealth => minHealth;
    public float MoveSpeedMin => moveSpeedMin;
    public float MoveSpeedMax => moveSpeedMax;

    private void OnValidate()
    {
        icon = Resources.Load<Sprite>(this.iconPath);
        switch (type)
        {
            case AsteroidType.GoldAsteroid:
                title = "GoldAsteroid";
                description = "Астероид, содержащий в себе до 5% золота.";
                dropKind = ItemKind.GoldOre;
                break;
            case AsteroidType.FerrumAsteroid:
                title = "FerrumAsteroid";
                description = "Астероид с примесями железа.";
                dropKind = ItemKind.FerrumOre;
                break;
            case AsteroidType.EmptyAsteroid:
                title = "EmptyAsteroid";
                description = "";
                break;
            case AsteroidType.MineralAsteroid:
                title = "MineralAsteroid";
                description = "Asteroid from mineral";
                dropKind = ItemKind.MineralOre;
                break;
            case AsteroidType.OrganicAsteroid:
                title = "OrganicAsteroid";
                description = "Asteroid from organic";
                dropKind = ItemKind.OrganicOre;
                break;
            case AsteroidType.TitanAsteroid:
                title = "TitanAsteroid";
                description = "Asteroid from titan";
                dropKind = ItemKind.TitanOre;
                break;
            case AsteroidType.OsmiumAsteroid:
                title = "OsmiumAsteroid";
                description = "Asteroid from osmium";
                dropKind = ItemKind.OsmiumOre;
                break;
        }
    }
}
