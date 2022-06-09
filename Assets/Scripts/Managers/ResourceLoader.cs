using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : MonoBehaviour, IGameManager
{
    private Dictionary<ItemKind,ItemData> items;
    private Dictionary<AsteroidType, AsteroidData> asteroids;
    private Dictionary<AmmoKind,AmmoData> ammo;
/*    private Dictionary<GunKind, GunData> guns;*/
    private Dictionary<SoundKind,AudioClip> sounds;
    private Dictionary<ShipKind, ShipData> ships;
    private Dictionary<Location, LocationData> locations;
    private Dictionary<Planet, PlanetData> planets;
    private Dictionary<IconType, Sprite> icons;
    private Dictionary<AsteroidFieldType, AsteroidFieldData> asteroidFields;
    private Dictionary<SunType, Sprite> sunIcons;
    private Dictionary<MobKind, MobData> mobs;
    private Dictionary<ObjectType, GameObject> objects;
    private Dictionary<ItemShopType,ItemShopData> itemShops;

    public Dictionary<ItemKind, ItemData> Items => items;
    public Dictionary<AsteroidType, AsteroidData> Asteroids => asteroids;
    public ManagerStatus Status { get; private set; }


    public void Startup()
    {
        Debug.Log("ResourceLoader starting...".SetColor(Color.Yellow));
        items = new Dictionary<ItemKind, ItemData>();
        asteroids = new Dictionary<AsteroidType,AsteroidData>();
        ammo = new Dictionary<AmmoKind, AmmoData>();
        sounds = new Dictionary<SoundKind,AudioClip>();
        ships = new Dictionary<ShipKind, ShipData>();
/*        guns = new Dictionary<GunKind,GunData>();*/
        locations = new Dictionary<Location, LocationData>();
        planets = new Dictionary<Planet, PlanetData>();
        icons = new Dictionary<IconType, Sprite>();
        asteroidFields = new Dictionary<AsteroidFieldType,AsteroidFieldData>();
        sunIcons = new Dictionary<SunType, Sprite>();
        mobs = new Dictionary<MobKind, MobData>();
        objects = new Dictionary<ObjectType, GameObject>();
        itemShops = new Dictionary<ItemShopType, ItemShopData>();

        LoadAllResources();

        Status = ManagerStatus.Started;
        Debug.Log("ResourceLoader started.".SetColor(Color.Green));
    }

    public ItemData DownloadData(ItemKind kind)
    {
        if(items.ContainsKey(kind))
        {
            return items[kind];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

    public MobData DownloadData(MobKind kind)
    {
        if(mobs.ContainsKey(kind))
        {
            return mobs[kind];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

    public GameObject DownloadData(ObjectType type)
    { 
        if(objects.ContainsKey(type))
        {
            return objects[type];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

    public ItemShopData DownloadData(ItemShopType type)
    {
        if(itemShops.ContainsKey(type))
        {
            return itemShops[type];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

    public AsteroidFieldData DownloadData(AsteroidFieldType kind)
    {
        if (asteroidFields.ContainsKey(kind))
        {
            return asteroidFields[kind];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

/*    public GunData DownloadData(GunKind kind)
    {
        if (guns.ContainsKey(kind))
        {
            return guns[kind];
        }
        Debug.Log($"{kind}.Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }*/

    public AmmoData DownloadData(AmmoKind kind)
    {
        if(ammo.ContainsKey(kind))
        {
            return ammo[kind];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

    public AsteroidData DownloadData(AsteroidType type)
    {
        if(asteroids.ContainsKey(type))
        {
            return asteroids[type];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

    public AudioClip DownloadData(SoundKind kind)
    {
        if(sounds.ContainsKey(kind))
        {
            return sounds[kind];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

    public ShipData DownloadData(ShipKind kind)
    {
        if (ships.ContainsKey(kind))
        {
            return ships[kind];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

    public LocationData DownloadData(Location location)
    {
        if(locations.ContainsKey(location))
        {
            return locations[location];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

    public PlanetData DownloadData(Planet planet)
    {
        if(planets.ContainsKey(planet))
        {
            return planets[planet];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

    public Sprite DownloadData(SunType type)
    {
        if(sunIcons.ContainsKey(type))
        {
            return sunIcons[type];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

    public Sprite DownloadData(IconType icon)
    {
        if(icons.ContainsKey(icon))
        {
            return icons[icon];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }
    private void LoadAllResources()
    {
        string oresPath                         = $"ScriptableObjects/Items/Ores/";
        string gunsPath                         = $"ScriptableObjects/Items/Guns/";
        string devicesPath                      = $"ScriptableObjects/Items/Devices/";
        string ammoPath                         = $"ScriptableObjects/Items/Ammo/";

        string shipsPath                        = $"ScriptableObjects/Ships/";

        string asteroidsPath                    = $"ScriptableObjects/Asteroids/";
        string asteroidFieldsPath               = $"ScriptableObjects/AsteroidFields/";

        string mobsPath                         = $"ScriptableObjects/Mobs/";

        string locationsPath                    = $"ScriptableObjects/Locations/";
        string planetsPath                      = $"ScriptableObjects/Planets/";
        string iconsPath                        = $"Icons/Interactive/";
        string soundsPath                       = $"Sounds/";

        string sunIconsPath                     = $"Icons/Suns/";

        string objectsPath = $"Prefabs/";
        string itemShopsPath = $"ScriptableObjects/ItemShops/";

        
        foreach(ItemKind itemKind in Enum.GetValues(typeof(ItemKind)))
        {
            var itemData = Resources.Load<ItemData>(oresPath + itemKind);
            if (itemData != null)
            {
                items.Add(itemKind, itemData);
                continue;
            }

            itemData = Resources.Load<ItemData>(gunsPath + itemKind);
            if (itemData != null)
            {
                items.Add(itemKind, itemData);
                continue;
            }

            itemData = Resources.Load<ItemData>(devicesPath + itemKind);
            if (itemData != null)
            {
                items.Add(itemKind, itemData);
                continue;
            }

            itemData = Resources.Load<ItemData>(ammoPath + itemKind);
            if (itemData != null)
            {
                items.Add(itemKind, itemData);
                continue;
            }
        }

        foreach(MobKind mobKind in Enum.GetValues(typeof(MobKind)))
        {
            mobs.Add(mobKind,Resources.Load<MobData>(mobsPath + mobKind));
        }

        foreach(AmmoKind ammoKind in Enum.GetValues(typeof(AmmoKind)))
        {
            ammo.Add(ammoKind, Resources.Load<AmmoData>(ammoPath + ammoKind));
        }

        foreach(ObjectType objectType in Enum.GetValues(typeof(ObjectType)))
        {
            objects.Add(objectType, Resources.Load<GameObject>(objectsPath + objectType));
        }

        foreach(ItemShopType itemShopType in Enum.GetValues(typeof(ItemShopType)))
        {
            itemShops.Add(itemShopType, Resources.Load<ItemShopData>(itemShopsPath + itemShopType));
        }

        foreach (var asteroidType in Enum.GetValues(typeof(AsteroidType)))
        {
            asteroids.Add((AsteroidType)asteroidType, Resources.Load<AsteroidData>(asteroidsPath + asteroidType));
        }

     
        foreach (var soundKind in Enum.GetValues(typeof(SoundKind)))
        {
            sounds.Add((SoundKind)soundKind, Resources.Load<AudioClip>(soundsPath + soundKind));
        }

        foreach(var shipKind in Enum.GetValues(typeof(ShipKind)))
        {
            ships.Add((ShipKind)shipKind, Resources.Load<ShipData>(shipsPath + shipKind));
        }

/*        guns.Add(GunKind.weaponMultiblaster, Resources.Load<GunData>(basePath + gunsPath                        + "Multiblaster"));
        guns.Add(GunKind.weaponDesintegrator, Resources.Load<GunData>(basePath + gunsPath                       + "Desintegrator"));
        guns.Add(GunKind.weaponKinetic, Resources.Load<GunData>(basePath + gunsPath                             + "Desintegrator"));*/
        
        foreach(var locationKind in Enum.GetValues(typeof(Location)))
        {
            locations.Add((Location)locationKind, Resources.Load<LocationData>(locationsPath + locationKind));
        }

        foreach(var planetKind in Enum.GetValues(typeof(Planet)))
        {
            planets.Add((Planet)planetKind, Resources.Load<PlanetData>(planetsPath + planetKind));
        }

        foreach(var sunIcon in Enum.GetValues(typeof(SunType)))
        {
            sunIcons.Add((SunType)sunIcon, Resources.Load<Sprite>(sunIconsPath + sunIcon));
        }

        foreach(var icon in Enum.GetValues(typeof(IconType)))
        {
            icons.Add((IconType)icon, Resources.Load<Sprite>(iconsPath + icon));
        }

        foreach(var asteroidField in Enum.GetValues(typeof(AsteroidFieldType)))
        {
            asteroidFields.Add((AsteroidFieldType)asteroidField, Resources.Load<AsteroidFieldData>(asteroidFieldsPath + asteroidField));
        }
    }
}
