using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : MonoBehaviour, IGameManager
{
    private Dictionary<ItemKind,BaseItemData> items;
    private Dictionary<AsteroidType, BaseAsteroidData> asteroids;
    //private Dictionary<AmmoType, BaseAmmoData> ammo;
    private Dictionary<GunKind, GunData> guns;
    private Dictionary<SoundKind,AudioClip> sounds;
    private Dictionary<ShipKind, ShipData> ships;
    private Dictionary<Location, LocationData> locations;
    private Dictionary<Planet, PlanetData> planets;
    private Dictionary<IconType, Sprite> icons;


    public Dictionary<ItemKind, BaseItemData> Items => items;
    public Dictionary<AsteroidType, BaseAsteroidData> Asteroids => asteroids;
    public ManagerStatus Status { get; private set; }


    public void Startup()
    {
        Debug.Log("ResourceLoader starting...".SetColor(Color.Yellow));
        items = new Dictionary<ItemKind, BaseItemData>();
        asteroids = new Dictionary<AsteroidType,BaseAsteroidData>();
        sounds = new Dictionary<SoundKind,AudioClip>();
        ships = new Dictionary<ShipKind, ShipData>();
        guns = new Dictionary<GunKind,GunData>();
        locations = new Dictionary<Location, LocationData>();
        planets = new Dictionary<Planet, PlanetData>();
        icons = new Dictionary<IconType, Sprite>();

        LoadAllResources();

        Status = ManagerStatus.Started;
        Debug.Log("ResourceLoader started.".SetColor(Color.Green));
    }

    public BaseItemData DownloadData(ItemKind kind)
    {
        if(items.ContainsKey(kind))
        {
            return items[kind];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

    public GunData DownloadData(GunKind kind)
    {
        if (guns.ContainsKey(kind))
        {
            return guns[kind];
        }
        Debug.Log("Critical warning!!! No all resources were founded.".SetColor(Color.Red));
        return null;
    }

    public BaseAsteroidData DownloadData(AsteroidType type)
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
        string basePath = $"ScriptableObjects/";
        string mineralsPath = $"Items/Minerals/";
        string ammoPath = $"Items/Ammo/";
        string gunsPath = $"Items/Guns/";
        string shipsPath = "Ships/";
        string asteroidsPath = $"Asteroids/";
        string locationsPath = $"Locations/";
        string planetsPath = $"Planets/";
        string iconsPath = $"Icons/Interactive/";
        Resources.Load<AudioClip>("Sounds/ShotKinetic");
        string soundsPath = $"Sounds/";

        items.Add(ItemKind.rudaFerrum, Resources.Load<BaseItemData>(basePath + mineralsPath                     + "Ferrum"));
        items.Add(ItemKind.rudaGold, Resources.Load<BaseItemData>(basePath + mineralsPath                       + "Gold"));
        items.Add(ItemKind.rudaNickel, Resources.Load<BaseItemData>(basePath + mineralsPath                     + "Nickel"));
        items.Add(ItemKind.rudaTitan, Resources.Load<BaseItemData>($""));

        items.Add(ItemKind.weaponKinetic, Resources.Load<BaseItemData>(basePath + gunsPath                      + "Laser"));
        Debug.Log(items[ItemKind.weaponKinetic].Title);

        items.Add(ItemKind.blueLaserAmmo, Resources.Load<BaseItemData>(basePath + ammoPath                      + "BlueLaser"));
        items.Add(ItemKind.redLaserAmmo, Resources.Load<BaseItemData>(basePath + ammoPath                       + "RedLaser"));

        asteroids.Add(AsteroidType.GoldAsteroid, Resources.Load<BaseAsteroidData>(basePath + asteroidsPath      + "GoldAsteroid"));
        asteroids.Add(AsteroidType.FerrumAsteroid, Resources.Load<BaseAsteroidData>(basePath + asteroidsPath    + "FerrumAsteroid"));
        asteroids.Add(AsteroidType.NickelAsteroid, Resources.Load<BaseAsteroidData>(basePath + asteroidsPath    + "NickelAsteroid"));
        asteroids.Add(AsteroidType.TitanAsteroid, Resources.Load<BaseAsteroidData>($""));


        sounds.Add(SoundKind.ShotKinetic1, Resources.Load<AudioClip>(soundsPath                                 + "ShotKinetic1"));
        sounds.Add(SoundKind.ShotKinetic2, Resources.Load<AudioClip>(soundsPath                                 + "ShotKinetic2"));

        ships.Add(ShipKind.Linkor, Resources.Load<ShipData>(basePath + shipsPath                                + "Linkor1"));

        guns.Add(GunKind.weaponKinetic, Resources.Load<GunData>(basePath + gunsPath                             + "Laser"));

        locations.Add(Location.Krinul, Resources.Load<LocationData>(basePath + locationsPath                    + "Krinul"));

        planets.Add(Planet.Arcea, Resources.Load<PlanetData>(basePath + planetsPath                             + "Arcea"));

        icons.Add(IconType.Land, Resources.Load<Sprite>(iconsPath                                               + "Land"));
        icons.Add(IconType.Rise, Resources.Load<Sprite>(iconsPath                                               + "Rise"));
        icons.Add(IconType.ShipShop,Resources.Load<Sprite>(iconsPath                                            + "ShipShop"));     
    }
}
