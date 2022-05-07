using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Planet", fileName = "NewPlanet", order = 54)]
public class PlanetData : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private Planet planet;
    [SerializeField] private PlanetIconType iconType;
    [SerializeField] private Sprite iconPlanet;
    [SerializeField] private Sprite iconBG;


    public string Title => title;
    public Sprite Icon => iconPlanet;
    public Sprite IconBG => iconBG;

    private void OnValidate()
    {
        switch (planet)
        {
            case Planet.Arcea:
                title = "Арсея";
                description = "";
                break;
            case Planet.Mars:
                title = "Марс";
                break;
            case Planet.Earth:
                title = "Земля";
                break;

        }
        iconPlanet = Resources.Load<Sprite>("Icons/Planets/" + iconType);
        iconBG = Resources.Load<Sprite>("Icons/Cosmoports/" + iconType);
    }

}
