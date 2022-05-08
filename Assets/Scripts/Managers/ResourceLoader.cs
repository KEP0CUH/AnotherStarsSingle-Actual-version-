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

        string mobsPath                         = $"ScriptableObject/Mobs";

        string locationsPath                    = $"ScriptableObjects/Locations/";
        string planetsPath                      = $"ScriptableObjects/Planets/";
        string iconsPath                        = $"Icons/Interactive/";
        string soundsPath                       = $"Sounds/";

        string sunIconsPath                     = $"Icons/Suns/";

        
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

        //ammo.Add(AmmoKind.MultiblasterAmmo, Resources.Load<AmmoData>(basePath + ammoPath + "Multiblaster"));

        /*        items.Add(ItemKind.rudaFerrum, Resources.Load<ItemData>(basePath + oresPath                     + $"FerrumOre"));
                items.Add(ItemKind.rudaGold, Resources.Load<ItemData>(basePath + oresPath                       + $"GoldOre"));
                items.Add(ItemKind.rudaNickel, Resources.Load<ItemData>(basePath + oresPath                     + $"NickelOre"));
                items.Add(ItemKind.rudaTitan, Resources.Load<ItemData>(basePath + oresPath                      + $"TitanOre"));
                items.Add(ItemKind.rudaMineral, Resources.Load<ItemData>(basePath + oresPath                    + $"MineralOre"));
                items.Add(ItemKind.rudaOrganic, Resources.Load<ItemData>(basePath + oresPath                    + $"OrganicOre"));
                items.Add(ItemKind.rudaOsmium, Resources.Load<ItemData>(basePath + oresPath                     + $"OsmiumOre"));

                items.Add(ItemKind.weaponEmpty, Resources.Load<ItemData>(basePath + gunsPath                          + "EmptyGun"));
                items.Add(ItemKind.DesintegratorGun, Resources.Load<ItemData>(basePath + gunsPath                + "Desintegrator"));
                items.Add(ItemKind.MultiblasterGun, Resources.Load<ItemData>(basePath + gunsPath                 + "Multiblaster"));
                items.Add(ItemKind.weaponKinetic, Resources.Load<GunData>(basePath + gunsPath                           + "Desintegrator"));

                items.Add(ItemKind.deviceEmpty, Resources.Load<ItemData>(basePath + devicesPath                     + "EmptyDevice"));
                items.Add(ItemKind.deviceTourbine, Resources.Load<ItemData>(basePath + devicesPath                  + "Tourbine"));

                items.Add(ItemKind.blueLaserAmmo, Resources.Load<ItemData>(basePath + ammoPath                      + "BlueLaser"));
                items.Add(ItemKind.redLaserAmmo, Resources.Load<ItemData>(basePath + ammoPath                       + "RedLaser"));

               */

        /*        asteroids.Add(AsteroidType.GoldAsteroid, Resources.Load<AsteroidData>(basePath + asteroidsPath      + "GoldAsteroid"));
                asteroids.Add(AsteroidType.FerrumAsteroid, Resources.Load<AsteroidData>(basePath + asteroidsPath    + "FerrumAsteroid"));
                asteroids.Add(AsteroidType.NickelAsteroid, Resources.Load<AsteroidData>(basePath + asteroidsPath    + "NickelAsteroid"));
                asteroids.Add(AsteroidType.TitanAsteroid, Resources.Load<AsteroidData>($""));*/

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
