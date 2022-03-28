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


    #region weapons
    weaponKinetic = 91,
    weaponRocket,
    weaponEnergetic,
    #endregion

    #region engines
    engineReactive = 191,
    engineIon,
    enginePhoton,
    #endregion

    #region devices
    deviceUnknown = 291,
    #endregion

    #region resources

    rudaGold = 491,
    rudaFerrum,
    rudaNickel,
    rudaTitan,

    #endregion

    #region other
    otherRubbish,
    #endregion
    #region Ammo
    redLaserAmmo = 1001,
    blueLaserAmmo = 1002,
    yellowLaserAmmo = 1003,
    GreenLaserAmmo = 1004
    #endregion

}

public enum ShipKind
{
    Linkor = 1,
    Frigate = 2
}

public enum GunKind
{
    #region weapons
    weaponKinetic = 91,
    weaponRocket,
    weaponEnergetic,
    #endregion
}

public enum SoundKind
{
    ShotKinetic1 = 10,
    ShotKinetic2 = 11
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
