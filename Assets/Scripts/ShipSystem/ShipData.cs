using UnityEngine;


[CreateAssetMenu(menuName="Ship",fileName="NewShip",order = 54)]
public class ShipData : ScriptableObject
{

    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private string title;
    [SerializeField]
    private string description;
    [SerializeField]
    private ShipKind kind = ShipKind.Linkor;

    public Sprite Icon => icon;
    public string Title => title;
    public string Description => description;
    public ShipKind Kind => kind;

    private void OnValidate()
    {
        switch(kind)
        {
            case ShipKind.Linkor:
                name = "Linkor";
                icon = Resources.Load<Sprite>("Icons/Ships/Linkor1");
                title = "Линкор";
                description = "Корабль, предназначенный для штурма укрепленных баз противника.";
                break;
        }
    }

}
