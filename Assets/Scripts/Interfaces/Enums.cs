public enum ManagerStatus
{
    Shutdown = 1,
    Initializing,
    Started
}
public enum ItemKind
{


    #region weapons
    weaponKinetic = 91,
    weaponLaser,
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

}

public enum EntityType
{
    Player,
    Enemy,
    Asteroid
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
