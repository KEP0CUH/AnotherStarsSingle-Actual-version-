///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private             int         id          = 1;
    [SerializeField] 
    private             Sprite      icon;


    public              Sprite      Icon => icon;

    public              void        Init()
    {
        id = 1;
        icon = Resources.Load<Sprite>("Images/Ships/Frigate");
    }

}
