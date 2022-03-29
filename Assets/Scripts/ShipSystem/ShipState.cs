using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipState : MonoBehaviour
{
    [SerializeField] private ShipData data;
    [SerializeField] private List<GunState> guns;

    private int maxNumGuns = 4;

    public ShipData Data => data;
    public List<GunState> Guns => guns;

    public ShipState Init(ShipKind kind)
    {
        this.data = Managers.Resources.DownloadData(kind);
        guns = new List<GunState>();
        return this;
    }

    

    public void SetGun(GunState gun)
    {
        if(IsCanSetGun())
        {
            AddGun(gun);
        }

    }

    public void SetGun(GunKind gunKind)
    {
        if(IsCanSetGun())
        {
            AddGun(gunKind);
        }
    }

    private void AddGun(GunState state)
    {
        GameObject newItemStateObj;
        BaseItemState newItemState;

        newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(GunState));
        newItemState = newItemStateObj.GetComponent<GunState>();

        newItemState.Init(state.GunKind, state.Count);
        this.guns.Add((GunState)newItemState);
    }

    private void AddGun(GunKind gunKind)
    {
        var gunDefault = new GameObject("DefaultGun", typeof(GunState));
        var gunState = gunDefault.GetComponent<GunState>();
        gunState.Init(GunKind.weaponKinetic, 1);

        this.guns.Add(gunState);
    }

    private bool IsCanSetGun()
    {
        if(guns.Count < maxNumGuns)
        {
            return true;
        }
        return false;
    }
}
