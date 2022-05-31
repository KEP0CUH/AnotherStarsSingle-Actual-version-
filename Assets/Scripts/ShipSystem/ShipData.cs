using UnityEngine;


[CreateAssetMenu(menuName="ScriptableObjects/Ships/NewShip",fileName="NewShip",order = 54)]
public class ShipData : ScriptableObject
{
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private string title, description;
    [SerializeField]
    private ShipKind kind;

    public Sprite Icon => icon;
    public string Title => title;
    public string Description => description;
    public ShipKind Kind => kind;

    private void OnValidate()
    {
        var iconsShipsPath = "Icons/Ships/";
        icon = Resources.Load<Sprite>(iconsShipsPath + kind);
        description = "";

        switch (kind)
        {
            case ShipKind.GreenLinkor:
                name = "Linkor";
                title = "Линкор";
                description = "Корабль, предназначенный для штурма укрепленных баз противника.";
                break;
            case ShipKind.GreenFrigate:
                name = "Frigate";
                title = "Фрегат";
                description = "";
                break;
            case ShipKind.GreenKorvet:
                name = "Korvet";
                title = "Корвет";
                description = "";
                break;
            case ShipKind.PirateIndus:
                name = "Indus";
                title = "Индустриальный";
                description = "";
                break;
            case ShipKind.PirateIstrebitel:
                name = "Istrebitel";
                title = "Истребитель";
                description = "";
                break;
            case ShipKind.PirateFrigate:
                name = "Frigate";
                title = "Фрегат";
                description = "";
                break;
        }
    }

}
