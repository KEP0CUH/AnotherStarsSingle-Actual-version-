using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInventory : IShipInventory
{
    private List<GunState> guns;
    private int maxNumGuns;

    public ShipInventory(int maxNumGuns)
    {
        this.maxNumGuns = maxNumGuns;
        guns = new List<GunState>();
    }


    public void AddItem(GunState state)
    {
        if (guns.Count < maxNumGuns)
        {
            var newState = CreateGunStateObject(state);

            this.guns.Add(newState);
            ShowInventory();
        }
    }

    public void AddItem(GunState state,IInventory inventory)
    {
        if(guns.Count < maxNumGuns)
        {
            var newState = CreateGunStateObject(state);

            this.guns.Add(newState);
            inventory.RemoveItem(newState.Data.ItemKind);
            ShowInventory();
        }
    }

    public void AddItem(GunKind gunKind)
    {
        if(guns.Count < maxNumGuns)
        {
            var newState = CreateGunStateObject(gunKind);

            this.guns.Add(newState);
            ShowInventory();
        }
    }

    public List<GunState> GetGuns()
    {
        return this.guns;
    }

    public void RemoveItem(GunState state)
    {
        guns.Remove(state);
        GameObject.Destroy(state.gameObject);
        ShowInventory();
    }

    public void RemoveAllItems()
    {
        foreach(var gun in this.guns)
        {
            Debug.Log("Снятие предмета с корабля...");
            Managers.Player.AddItemInventory(gun);
        }

        guns.Clear();
        ShowInventory();
    }

    public void ShowInventory()
    {
/*        string list = "";
        foreach(var gun in guns)
        {
            list += $"{gun.Data.Title} ";

            Debug.Log(list);
        }*/
        CanvasUI.Inventory.ShowInventory(this, guns);
    }



    private GunState CreateGunStateObject(GunState state)
    {
        BaseItemState newItemState;

        var newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(GunState));
        newItemState = newItemStateObj.GetComponent<GunState>();

        newItemState.Init(state.GunKind, state.Count);
        return (GunState)newItemState;
    }
    private GunState CreateGunStateObject(GunKind kind)
    {
        var gunDefault = new GameObject("DefaultGun", typeof(GunState));
        var gunState = gunDefault.GetComponent<GunState>();
        gunState.Init(kind, 1);

        return (GunState)gunState;
    }
}
