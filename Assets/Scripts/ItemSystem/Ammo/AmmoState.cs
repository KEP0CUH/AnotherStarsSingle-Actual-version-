///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

public class AmmoState : MonoBehaviour
{
    private             AmmoData                data;
    private             AmmoController          controller;

    public              AmmoData                Data => data;
    public              float                   MoveSpeed => data.Speed;

    public              AmmoState               Init(AmmoController controller,AmmoKind kind)
    {
        this.controller = controller;
        this.data = Managers.Resources.DownloadData(kind);

        return this;
    }
}
