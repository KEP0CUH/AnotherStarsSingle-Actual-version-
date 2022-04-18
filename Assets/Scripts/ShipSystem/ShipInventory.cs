using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShipInventory : IShipInventory
{
    private List<GunState> guns;
    private int maxNumGuns;

    public ShipInventory(int maxNumGuns)
    {
        this.maxNumGuns = maxNumGuns;
        guns = new List<GunState>();

        for (int i = 0; i < maxNumGuns; i++)
        {
            var gunEmpty = CreateGunStateObject(ItemKind.EmptyItem);
            guns.Add(gunEmpty);
        }
        ShowInventory();
    }


    public void AddItem(GunState state)
    {
        if (guns.Count < maxNumGuns)
        {
            var newState = CreateGunStateObject(state);
            this.guns.Add(newState);
            ShowInventory();
        }
        else
        {
            for (int i = 0; i < maxNumGuns; i++)
            {
                if(guns[i].Data.ItemKind == ItemKind.EmptyItem)
                {
                    var newState = CreateGunStateObject(state);
                    GameObject.Destroy(guns[i]);
                    guns[i] = newState;
                    ShowInventory();
                    return;
                }
            }
        }

    }

    public void AddItem(GunState state, IInventory inventory)
    {
        if (guns.Count < maxNumGuns)
        {
            var newState = CreateGunStateObject(state);

            this.guns.Add(newState);
            inventory.RemoveItem(newState.Data.ItemKind);
            ShowInventory();
        }
        else
        {
            for (int i = 0; i < maxNumGuns; i++)
            {
                if (guns[i].Data.ItemKind == ItemKind.EmptyItem)
                {
                    var newState = CreateGunStateObject(state);
                    GameObject.Destroy(guns[i]);
                    guns[i] = newState;
                    ShowInventory();
                    return;
                }
            }
        }
    }

    public void AddItem(ItemKind gunKind)
    {
        if (guns.Count < maxNumGuns)
        {
            var newState = CreateGunStateObject(gunKind);

            this.guns.Add(newState);
            ShowInventory();
        }
        else
        {
            for (int i = 0; i<maxNumGuns; i++)
            {
                if (guns[i].Data.ItemKind == ItemKind.EmptyItem)
                {
                    var newState = CreateGunStateObject(gunKind);
                    GameObject.Destroy(guns[i]);
                    guns[i] = newState;
                    ShowInventory();
                    return;
                }
            }
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
        foreach (var gun in this.guns)
        {
            Debug.Log("Снятие предмета с корабля...");
            Managers.Player.AddItemInventory(gun);
        }

        guns.Clear();
        ShowInventory();
    }

    public void ShowInventory()
    {
        /*        foreach(var gun in guns)
                {
                    if(gun == null)
                    {
                        gun = new BaseItemData;
                    }

                }*/
        CanvasUI.Inventory.ShowInventory(this, guns);
    }



    private GunState CreateGunStateObject(GunState state)
    {
        BaseItemState newItemState;

        var newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(GunState));
        newItemState = newItemStateObj.GetComponent<GunState>();

        newItemState.Init(state.Data.ItemKind, state.Count);
        return (GunState)newItemState;
    }
    private GunState CreateGunStateObject(ItemKind kind)
    {
        var gunDefault = new GameObject("DefaultGun", typeof(GunState));
        var gunState = gunDefault.GetComponent<GunState>();
        gunState.Init(kind, 1);

        return (GunState)gunState;
    }
}
