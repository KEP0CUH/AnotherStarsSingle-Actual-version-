using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipState : MonoBehaviour
{
    [SerializeField] private ShipData data;
    [SerializeField] private List<GunState> guns; 

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
        AddGun(gun);
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


}
