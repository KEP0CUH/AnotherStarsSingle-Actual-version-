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
        string basePath = $"ScriptableObjects/";
        string oresPath = $"Items/Ores/";
        string ammoPath = $"Items/Ammo/";
        string gunsPath = $"Items/Guns/";
        string devicesPath = $"Items/Devices/";
        string shipsPath = "Ships/";
        string asteroidsPath = $"Asteroids/";
        string locationsPath = $"Locations/";
        string planetsPath = $"Planets/";
        string iconsPath = $"Icons/Interactive/";
        string soundsPath = $"Sounds/";
        string asteroidFieldsPath = $"AsteroidFields/";
        string sunIconsPath = $"Icons/Suns/";

        
        items.Add(ItemKind.rudaFerrum, Resources.Load<ItemData>(basePath + oresPath                     + $"FerrumOre"));
        items.Add(ItemKind.rudaGold, Resources.Load<ItemData>(basePath + oresPath                       + $"GoldOre"));
        items.Add(ItemKind.rudaNickel, Resources.Load<ItemData>(basePath + oresPath                     + $"NickelOre"));
        items.Add(ItemKind.rudaTitan, Resources.Load<ItemData>(basePath + oresPath                      + $"TitanOre"));
        items.Add(ItemKind.rudaMineral, Resources.Load<ItemData>(basePath + oresPath                    + $"MineralOre"));
        items.Add(ItemKind.rudaOrganic, Resources.Load<ItemData>(basePath + oresPath                    + $"OrganicOre"));
        items.Add(ItemKind.rudaOsmium, Resources.Load<ItemData>(basePath + oresPath                     + $"OsmiumOre"));

        items.Add(ItemKind.weaponEmpty, Resources.Load<ItemData>(basePath + gunsPath                          + "EmptyGun"));
        items.Add(ItemKind.weaponDesintegrator, Resources.Load<ItemData>(basePath + gunsPath                + "Desintegrator"));
        items.Add(ItemKind.weaponMultiblaster, Resources.Load<ItemData>(basePath + gunsPath                 + "Multiblaster"));
        items.Add(ItemKind.weaponKinetic, Resources.Load<GunData>(basePath + gunsPath                           + "Desintegrator"));

        items.Add(ItemKind.deviceEmpty, Resources.Load<ItemData>(basePath + devicesPath                     + "EmptyDevice"));
        items.Add(ItemKind.deviceTourbine, Resources.Load<ItemData>(basePath + devicesPath                  + "Tourbine"));

        items.Add(ItemKind.blueLaserAmmo, Resources.Load<ItemData>(basePath + ammoPath                      + "BlueLaser"));
        items.Add(ItemKind.redLaserAmmo, Resources.Load<ItemData>(basePath + ammoPath                       + "RedLaser"));

        asteroids.Add(AsteroidType.GoldAsteroid, Resources.Load<AsteroidData>(basePath + asteroidsPath      + "GoldAsteroid"));
        asteroids.Add(AsteroidType.FerrumAsteroid, Resources.Load<AsteroidData>(basePath + asteroidsPath    + "FerrumAsteroid"));
        asteroids.Add(AsteroidType.NickelAsteroid, Resources.Load<AsteroidData>(basePath + asteroidsPath    + "NickelAsteroid"));
        asteroids.Add(AsteroidType.TitanAsteroid, Resources.Load<AsteroidData>($""));

        ammo.Add(AmmoKind.Multiblaster, Resources.Load<AmmoData>(basePath + ammoPath                            + "Multiblaster"));

        sounds.Add(SoundKind.ShotKinetic1, Resources.Load<AudioClip>(soundsPath                                 + "ShotKinetic1"));
        sounds.Add(SoundKind.ShotKinetic2, Resources.Load<AudioClip>(soundsPath                                 + "ShotKinetic2"));
        sounds.Add(SoundKind.ShotEnergetic2, Resources.Load<AudioClip>(soundsPath                               + "ShotEnergetic2"));

        ships.Add(ShipKind.GreenLinkor, Resources.Load<ShipData>(basePath + shipsPath                           + "GreenLinkor"));
        ships.Add(ShipKind.GreenFrigate, Resources.Load <ShipData>(basePath + shipsPath                         + "GreenFrigate"));
        ships.Add(ShipKind.GreenKorvet, Resources.Load<ShipData>(basePath + shipsPath                           + "GreenKorvet"));

/*        guns.Add(GunKind.weaponMultiblaster, Resources.Load<GunData>(basePath + gunsPath                        + "Multiblaster"));
        guns.Add(GunKind.weaponDesintegrator, Resources.Load<GunData>(basePath + gunsPath                       + "Desintegrator"));
        guns.Add(GunKind.weaponKinetic, Resources.Load<GunData>(basePath + gunsPath                             + "Desintegrator"));*/
        

        locations.Add(Location.Krinul, Resources.Load<LocationData>(basePath + locationsPath                    + "Krinul"));

        planets.Add(Planet.Arcea, Resources.Load<PlanetData>(basePath + planetsPath                             + Planet.Arcea));
        planets.Add(Planet.Earth, Resources.Load<PlanetData>(basePath + planetsPath                             + Planet.Earth));
        planets.Add(Planet.Mars, Resources.Load<PlanetData>(basePath + planetsPath                              + Planet.Mars));

        sunIcons.Add(SunType.YellowSun, Resources.Load<Sprite>(sunIconsPath                                     + SunType.YellowSun));
        sunIcons.Add(SunType.GreenSun, Resources.Load<Sprite>(sunIconsPath                                      + SunType.GreenSun));
        sunIcons.Add(SunType.OrangeSun, Resources.Load<Sprite>(sunIconsPath                                     + SunType.OrangeSun));
        sunIcons.Add(SunType.BlueSun, Resources.Load<Sprite>(sunIconsPath                                       + SunType.BlueSun));
        sunIcons.Add(SunType.WhiteSun, Resources.Load<Sprite>(sunIconsPath                                      + SunType.WhiteSun));

        icons.Add(IconType.Land, Resources.Load<Sprite>(iconsPath                                               + "Land"));
        icons.Add(IconType.Rise, Resources.Load<Sprite>(iconsPath                                               + "Rise"));
        icons.Add(IconType.ShipShop,Resources.Load<Sprite>(iconsPath                                            + "ShipShop"));
        icons.Add(IconType.ItemShop, Resources.Load<Sprite>(iconsPath                                           + "ItemShop"));
        icons.Add(IconType.Asteroid, Resources.Load<Sprite>(iconsPath                                           + "Asteroid"));
        icons.Add(IconType.AsteroidField, Resources.Load<Sprite>(iconsPath                                      + "AsteroidField"));
        icons.Add(IconType.Skill, Resources.Load<Sprite>(iconsPath                                              + "Skills"));
        icons.Add(IconType.Inventory, Resources.Load<Sprite>(iconsPath                                          + "Inventory"));
        icons.Add(IconType.CloseWindow, Resources.Load<Sprite>(iconsPath                                        + "CloseWindow"));
        icons.Add(IconType.ButtonOK, Resources.Load<Sprite>(iconsPath                                           + "ButtonOK"));
        icons.Add(IconType.ButtonOpenMap, Resources.Load<Sprite>(iconsPath                                      + "ButtonOpenMap"));
        icons.Add(IconType.ButtonSettings, Resources.Load<Sprite>(iconsPath                                     + "ButtonSettings"));


        asteroidFields.Add(AsteroidFieldType.GoldField,Resources.Load<AsteroidFieldData>(basePath + asteroidFieldsPath          + "GoldField"));
        asteroidFields.Add(AsteroidFieldType.FerrumField, Resources.Load<AsteroidFieldData>(basePath + asteroidFieldsPath       + "FerrumField"));
    }
}
