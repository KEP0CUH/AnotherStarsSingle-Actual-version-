using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipState : MonoBehaviour
{
    [SerializeField] private ShipData data;
    [SerializeField] private GunState gun; 

    public ShipData Data => data;
    public GunState Gun => gun;

    public ShipState Init(ShipKind kind)
    {

        this.data = Managers.Resources.DownloadData(kind);
        return this;
    }

    public void SetGun(GunState gun)
    {
        this.gun = gun;
    }
}
