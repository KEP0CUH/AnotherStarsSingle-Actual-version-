///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

[RequireComponent(typeof(AmmoState))]
[RequireComponent(typeof(AmmoView))]
public class AmmoController : MonoBehaviour
{
    private             AmmoState       ammoState;
    private             AmmoView        ammoView;
    private             GunState        gunState;

    public              AmmoState       State => ammoState;
    public              AmmoView        View => ammoView;
    public              GunState        GunState => gunState;

    public              void            Init(Transform target,GunState gun)
    {
        this.ammoState = GetComponent<AmmoState>().Init(this, ((GunData)(gun.Data)).AmmoKind);
        this.ammoView = GetComponent<AmmoView>().Init(target,this);
        this.gunState = gun;
    }
}
