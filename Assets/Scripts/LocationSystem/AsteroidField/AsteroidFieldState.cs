///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

public class AsteroidFieldState : MonoBehaviour
{
    [SerializeField]
    private             AsteroidFieldData           data;
    public              AsteroidFieldData           Data => data;

    public              AsteroidFieldState          Init(AsteroidFieldType type)
    {
        this.data = Managers.Resources.DownloadData(type);
        return this;
    }
}
