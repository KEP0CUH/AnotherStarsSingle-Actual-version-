///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

[CreateAssetMenu(menuName = "Planet", fileName = "NewPlanet", order = 54)]
public class PlanetData : ScriptableObject
{
    [SerializeField] 
    private             string              title;
    [SerializeField] 
    private             string              description;
    [SerializeField]
    private             Planet              planet;
    [SerializeField] 
    private             PlanetIconType      iconType;
    [SerializeField] 
    private             Sprite              iconPlanet;
    [SerializeField] 
    private             Sprite              iconBG;
    [SerializeField] 
    private             ItemShopType        itemShopType;


    public              string              Title => title;
    public              Sprite              Icon => iconPlanet;
    public              Sprite              IconBG => iconBG;
    public              ItemShopType        ItemShopType => itemShopType;

    private             void                OnValidate()
    {
        switch (planet)
        {
            case Planet.Arcea:
                title           = "Арсея";
                description     = "";
                itemShopType    = ItemShopType.GreenShop1;
                break;
            case Planet.Mars:
                title           = "Марс";
                itemShopType    = ItemShopType.ShopEmpty;
                break;
            case Planet.Earth:
                title           = "Земля";
                itemShopType    = ItemShopType.GreenShop2;
                break;

        }
        iconPlanet  = Resources.Load<Sprite>("Icons/Planets/"       + iconType);
        iconBG      = Resources.Load<Sprite>("Icons/Cosmoports/"    + iconType);
    }

}
