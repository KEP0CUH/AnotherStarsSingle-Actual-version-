///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Items/newItem", fileName = "NewItem", order = 53)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private             Sprite           icon;
    [SerializeField]
    private             string           title, description;
    [SerializeField]
    private             int              size;
    [SerializeField]
    private             ItemKind         kind;

    public              Sprite           Icon => icon;
    public              string           Title => title;
    public              string           Description => description;
    public              int              Size => size;
    public              ItemKind         ItemKind => kind;

    public virtual      bool             IsItem()
    {
        return true;
    }

    public virtual      bool             IsWeapon()
    {
        return false;
    }

    public virtual      bool             IsDevice()
    {
        return false;
    }

    protected virtual   void             OnValidate()
    {
        string oreSpritesPath       = "Icons/Items/Ores/";
        string gunSpritesPath       = "Icons/Items/Guns/";
        string deviceSpritesPath    = "Icons/Items/Devices/";
        string ammoSpritesPath      = "Icons/Items/Ammo/";

        name = kind.ToString();
        icon = Resources.Load<Sprite>(gunSpritesPath + kind);

        switch (kind)
        {
            #region Ruda
            case ItemKind.FerrumOre:
                name = "FerrumOre";
                icon = Resources.Load<Sprite>(oreSpritesPath + name);
                title = "Железо";
                description = "Металл с высокой химической реакционной способностью. Широко распространен во Вселенной \"Иные звёзды\"";
                break;
            case ItemKind.GoldOre:
                name = "GoldOre";
                icon = Resources.Load<Sprite>(oreSpritesPath + name);
                title = "Золото";
                description = "Золото – это ценный металл, известный человечеству с древних времён.Полезное ископаемое имеет характерный жёлтый цвет.";
                break;
            case ItemKind.TitanOre:
                name = "TitanOre";
                icon = Resources.Load<Sprite>(oreSpritesPath + name);
                title = "Титан";
                description = "";
                break;
            case ItemKind.MineralOre:
                name = "MineralOre";
                icon = Resources.Load<Sprite>(oreSpritesPath + name);
                title = "Минерал";
                description = "";
                break;
            case ItemKind.OrganicOre:
                name = "OrganicOre";
                icon = Resources.Load<Sprite>(oreSpritesPath + name);
                title = "Органические материалы";
                description = "";
                break;
            case ItemKind.OsmiumOre:
                name = "OsmiumOre";
                icon = Resources.Load<Sprite>(oreSpritesPath + name);
                title = "Осмиевая руда";
                description = "";
                break;
            #endregion

            #region Guns
            case ItemKind.EmptyGun:
                name = "EmptyGun";
                icon = Resources.Load<Sprite>("Icons/Items/EmptySlot");
                title = "Заглушка";
                description = "";
                break;
            case ItemKind.MultiblasterGun:
                name = "Multiblaster";
                icon = Resources.Load<Sprite>(gunSpritesPath + name);
                title = "Мультибластер";
                size = 50;
                description = "";
                break;
            case ItemKind.DesintegratorGun:
                name = "Desintegrator";
                icon = Resources.Load<Sprite>(gunSpritesPath + name);
                title = "Дезинтегратор";
                size = 50;
                description = "";
                break;
            case ItemKind.KineticGun:
                name = "Desintegrator";
                icon = Resources.Load<Sprite>(gunSpritesPath + name);
                title = "Кинетик";
                description = "";
                break;
            case ItemKind.PulsarGun:
                name = kind.ToString();
                icon = Resources.Load<Sprite>(gunSpritesPath + kind);
                title = kind.ToString();
                description = "";
                break;
            

            #endregion

            #region Devices
            case ItemKind.EmptyDevice:
                name = "EmptyDevice";
                icon = Resources.Load<Sprite>("Icons/Items/EmptySlot");
                title = "Заглушка";
                description = "";
                break;
            case ItemKind.TourbineDevice:
                name = "Tourbine";
                icon = Resources.Load<Sprite>(deviceSpritesPath + name);
                title = "Турбина";
                size = 40;
                description = "Ускоряет корабль на 30%";
                break;

            #endregion

            #region Ammo

            case ItemKind.blueLaserAmmo:
                name = "BlueLaser";
                icon = Resources.Load<Sprite>(ammoSpritesPath + name);
                title = "Синий лазерный патрон";
                description = "Синий патрон, наиболее мощный из имеющихся.";
                break;
            case ItemKind.redLaserAmmo:
                name = "RedLaser";
                icon = Resources.Load<Sprite>(ammoSpritesPath + name);
                title = "Красный лазерный патрон.";
                description = "Слабый патрон, позволяющий разрушать астероиды.";
                break;
           #endregion
        }
    }
}
