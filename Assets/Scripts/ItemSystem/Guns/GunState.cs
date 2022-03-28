using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : BaseItemState
{
    [SerializeField] private GunKind gunKind;
    private int maxAmmo;

    public GunKind GunKind => gunKind;
    public int MaxAmmo => maxAmmo;

    public override void Init(GunKind kind, int count)
    {
        this.data = Managers.Resources.DownloadData(kind);
        this.count = count;
        this.isWeapon = true;
        this.gunKind = kind;
    }

}
