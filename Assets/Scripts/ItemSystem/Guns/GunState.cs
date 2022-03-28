using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : BaseItemState
{
    [SerializeField] private GunData data;
    private int maxAmmo;

    public GunData Data => data;
    public int MaxAmmo => maxAmmo;

    public GunState Init(GunKind kind,int maxAmmo)
    {
        this.data = Managers.Resources.DownloadData(kind);
        this.maxAmmo = maxAmmo;
        return this;
    }

    public override bool IsWeapon()
    {
        return true;
    }

}
