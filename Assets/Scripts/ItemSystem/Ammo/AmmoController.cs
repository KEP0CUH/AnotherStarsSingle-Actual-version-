using UnityEngine;

[RequireComponent(typeof(AmmoState))]
[RequireComponent(typeof(AmmoView))]
public class AmmoController : MonoBehaviour
{
    private AmmoState ammoState;
    private AmmoView ammoView;

    public AmmoState State => ammoState;
    public AmmoView View => ammoView;

    public void Init(Transform target,GunState gun)
    {
        this.ammoState = GetComponent<AmmoState>().Init(this, ((GunData)(gun.Data)).AmmoKind);
        this.ammoView = GetComponent<AmmoView>().Init(target,this);
    }
}
