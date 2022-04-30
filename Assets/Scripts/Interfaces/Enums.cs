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
    Radar = 16
}
public enum ItemKind
{
    #region WEAPONS
    weaponEmpty,
    weaponRocket,
    weaponMultiblaster,
    weaponDesintegrator,
    weaponKinetic,
    #endregion

    #region ENGINES
    engineReactive = 191,
    engineIon,
    enginePhoton,
    #endregion

    #region DEVICES
    deviceUnknown = 291,
    deviceEmpty,
    deviceTourbine,
    #endregion

    #region RUDA

    rudaGold = 491,
    rudaFerrum,
    rudaNickel,
    rudaTitan,

    #endregion

    #region OTHER
    otherRubbish,
    #endregion

    #region AMMO
    redLaserAmmo = 1001,
    blueLaserAmmo = 1002,
    yellowLaserAmmo = 1003,
    GreenLaserAmmo = 1004
    #endregion

}
public enum AmmoKind
{
    redLaserAmmo = 1001,
    blueLaserAmmo = 1002,
    yellowLaserAmmo = 1003,
    GreenLaserAmmo = 1004,
    Multiblaster,
    Desintegrator,
}

public enum ShipKind
{
    GreenIndus = 1,
    GreenKorvet,
    GreenIstrebitel,
    GreenHeavyIstrebitel,
    GreenLinkor,
    GreenFrigate,

}

public enum SoundKind
{
    ShotKinetic1 = 10,
    ShotKinetic2,
    ShotEnergetic2
}

public enum EntityType
{
    Player,
    Enemy,
 
}

public enum AsteroidType
{
    GoldAsteroid = 1,
    FerrumAsteroid,
    NickelAsteroid,
    TitanAsteroid
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

public enum IconType
{
    Land,
    Rise,
    ShipShop,
    ItemShop,
}
