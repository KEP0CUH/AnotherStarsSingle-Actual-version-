public enum ManagerStatus
{
    Shutdown = 1,
    Initializing,
    Started
}

public enum EventType
{
    OnItemDrop,
    OnItemPick,
}
public enum UIModuleKind
{
    Canvas = 2,
    Inventory = 4,
    Skills = 8,
    Radar = 16,
    GlobalMap
}
public enum ItemKind
{
    #region WEAPONS
    EmptyGun,
    RocketGun,
    MultiblasterGun,
    DesintegratorGun,
    KineticGun,

    PulsarGun,
    RezakGun, 

    PulemetGun,
    PhotonGun,
    #endregion

    #region ENGINES
    engineReactive = 191,
    engineIon,
    enginePhoton,
    #endregion

    #region DEVICES
    //deviceUnknown,
    EmptyDevice,
    TourbineDevice,
    #endregion

    #region ORE
    GoldOre,
    FerrumOre,
    TitanOre,
    MineralOre,
    OrganicOre,
    OsmiumOre,

    #endregion

    #region OTHER
    otherRubbish,
    #endregion

    #region AMMO
    redLaserAmmo,
    blueLaserAmmo,
    yellowLaserAmmo,
    GreenLaserAmmo,
    #endregion

}
public enum AmmoKind
{
    //redLaserAmmo = 1001,
    //blueLaserAmmo = 1002,
    //yellowLaserAmmo = 1003,
    //GreenLaserAmmo = 1004,
    MultiblasterAmmo,
    DesintegratorAmmo,
    EmptyAmmo,
}

public enum ShipKind
{
    GreenIndus,
    GreenKorvet,
    GreenIstrebitel,
    GreenHeavyIstrebitel,
    GreenLinkor,
    GreenFrigate,
    PirateIndus,
    PirateIstrebitel,
    PirateFrigate,
}

public enum SoundKind
{
    ChargeBattery,
    Click1,
    Click2,
    Click3,
    ClickError,
    ClickMove,
    Destroyed1,
    Destroyed2,
    Destroyed3,
    RepairShip,
    ShotBiochemisry,
    ShotKinetic1,
    ShotKinetic2,
    ShotEnergetic1,
    ShotEnergetic2,
    ShotRocket,

    Tourbine,
}

public enum EntityType
{
    Player,
    Enemy,
}

public enum MobKind
{
    PirateIndus1,
    PirateIstrebitel1,
    PirateFrigate1,
    PirateScience,
}

public enum MobSpawnerType
{
    pirateSpawner1,
    pirateSpawner2,
    pirateSpawner3,
}

public enum AsteroidType
{
    EmptyAsteroid,

    GoldAsteroid,
    FerrumAsteroid,
    TitanAsteroid,

    MineralAsteroid,
    OrganicAsteroid,

    OsmiumAsteroid,

    NickelAsteroid,

}

public enum AsteroidFieldType
{
    EmptyField,
    GoldField,
    FerrumField,
    TitanField,
    MineralField,
    OrganicField,
    OsmiumField,
    NickelField,
}



public enum EntityKind
{
    #region Пираты
    pirateCorsar = 1,
    #endregion

    #region Ксеноморфы
    ksenomorfIstrebitel = 100,
    #endregion

    #region Альянс
    aliansFrigate = 200,
    #endregion
}

public enum Location
{
    Krinul = 1,
    Lambda = 2
}

public enum Planet
{
    Earth,
    Mars,
    Arcea
}

public enum SunType
{
    YellowSun,
    GreenSun,
    BlueSun,
    WhiteSun,
    OrangeSun,
}

public enum PlanetIconType
{
    planetType1,
    planetType2,
    planetType3,
    planetType4,
    planetType5,
    planetType6,
    planetType7,
    planetType8,
    planetType9,
    planetType10,
    planetType11,
    planetType12,
    planetType13,
    planetType14,
    planetType15,
    planetType16,
}

public enum IconType
{
    Land,
    Rise,
    ShipShop,
    ItemShop,
    Asteroid,
    AsteroidField,
    Skills,
    Inventory,
    CloseWindow,
    ButtonOK,
    ButtonOpenMap,
    ButtonSettings,
}

public enum ObjectType
{
    BuyWindow,
    ConfirmBuying,
    ConfirmSelling,
    ItemShop,
    InfoPlanetWindow,
    InfoAsteroidWindow,
    ItemCell,
    ShipShop
}

public enum ItemShopType
{
    ShopEmpty,
    GreenShop1,
    GreenShop2,
    //GreenShop3,
}