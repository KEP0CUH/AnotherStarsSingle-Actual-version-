using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "NewItem", order = 51)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private string title;
    [SerializeField]
    private string description;
    [SerializeField]
    private ItemKind kind;

    public Sprite Icon => icon;
    public string Title => title;
    public string Description => description;
    public ItemKind ItemKind => kind;

    public virtual bool IsItem()
    {
        return true;
    }

    public virtual bool IsWeapon()
    {
        return false;
    }

    public virtual bool IsDevice()
    {
        return false;
    }
    

    protected void OnValidate()
    {
        string mineralsSpritesPath = "Icons/Items/Minerals/";
        string gunSpritesPath = "Icons/Items/Guns/";
        string deviceSpritesPath = "Icons/Items/Devices/";
        string ammoSpritesPath = "Icons/Items/Ammo/";

        switch (kind)
        {


            #region Ruda

            case ItemKind.rudaFerrum:
                name = "Ferrum";
                icon = Resources.Load<Sprite>(mineralsSpritesPath + name);
                title = "Железо";
                description = "Металл с высокой химической реакционной способностью. Широко распространен во Вселенной \"Иные звёзды\"";
                break;
            case ItemKind.rudaGold:
                name = "Gold";
                icon = Resources.Load<Sprite>(mineralsSpritesPath + name);
                title = "Золото";
                description = "Золото – это ценный металл, известный человечеству с древних времён.Полезное ископаемое имеет характерный жёлтый цвет.";
                break;
            case ItemKind.rudaNickel:
                name = "Nickel";
                icon = Resources.Load<Sprite>(mineralsSpritesPath + name);
                title = "Никель";
                description = "";
                break;
            case ItemKind.rudaTitan:
                name = "Titan";
                icon = Resources.Load<Sprite>(mineralsSpritesPath + name);
                title = "Титан";
                description = "";
                break;
            #endregion

            #region Guns
            case ItemKind.weaponEmpty:
                name = "EmptyGun";
                icon = Resources.Load<Sprite>("Icons/Items/EmptySlot");
                title = "Заглушка";
                description = "";
                break;
            case ItemKind.weaponMultiblaster:
                name = "Multiblaster";
                icon = Resources.Load<Sprite>(gunSpritesPath + name);
                title = "Мультибластер";
                description = "";
                break;
            case ItemKind.weaponDesintegrator:
                name = "Desintegrator";
                icon = Resources.Load<Sprite>(gunSpritesPath + name);
                title = "Дезинтегратор";
                description = "";
                break;
            case ItemKind.weaponKinetic:
                name = "Desintegrator";
                icon = Resources.Load<Sprite>(gunSpritesPath + name);
                title = "Кинетик";
                description = "";
                break;

            #endregion

            #region Devices
            case ItemKind.deviceEmpty:
                name = "EmptyDevice";
                icon = Resources.Load<Sprite>("Icons/Items/EmptySlot");
                title = "Заглушка";
                description = "";
                break;
            case ItemKind.deviceTourbine:
                name = "Tourbine";
                icon = Resources.Load<Sprite>(deviceSpritesPath + name);
                title = "Турбина";
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