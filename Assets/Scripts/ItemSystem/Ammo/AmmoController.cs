using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AmmoState))]
[RequireComponent(typeof(AmmoView))]
public class AmmoController : MonoBehaviour
{
    private AmmoState ammoState;
    private AmmoView ammoView;

    public AmmoState AmmoState => ammoState;
    public AmmoView AmmoView => ammoView;

    public void Init(GunState gun)
    {
        this.ammoState = GetComponent<AmmoState>().Init(this, gun.AmmoKind);
        this.ammoView = GetComponent<AmmoView>().Init(ammoState,((GunData)gun.Data).SoundKind);
    }


}
