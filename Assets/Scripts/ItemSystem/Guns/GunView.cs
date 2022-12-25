///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

public class GunView : ItemView
{
    public override         ItemView        Init(ItemKind kind, int ammoMax)
    {
        var data = Managers.Resources.DownloadData(kind);
        this.state = this.gameObject.AddComponent<GunState>();
        this.state.Init(kind, ammoMax);
        this.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        this.GetComponent<SphereCollider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;

        return this;
    }
}
