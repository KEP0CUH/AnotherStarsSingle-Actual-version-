using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShipInventory : IShipInventory
{
    private List<GunState> guns;
    private int maxNumGuns;

    /// <summary>
    /// Конструктор оборудоваемых слотов корабля. Изначально все ячейки заполняются пустышками, которые будут заменены при взаимодействии.
    /// </summary>
    /// <param name="maxNumGuns">Максимальное число оружий одеваемых на корабль.</param>
    public ShipInventory(int maxNumGuns)
    {
        this.maxNumGuns = maxNumGuns;
        guns = new List<GunState>();

        for (int i = 0; i < maxNumGuns; i++)
        {
            guns.Add(CreateEmptyGunState());
        }
        ShowInventory();
    }

    /// <summary>
    /// Вызывается при попытке одеть пушку на корабль, как правило при начальной инициализации корабля.
    /// </summary>
    /// <param name="state">Состояние пушки.</param>
    public void TrySetGun(GunState state)
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

    /// <summary>
    /// Вызывается при попытке одеть пушку из инвентаря игрока на корабль.
    /// </summary>
    /// <param name="state"></param>
    /// <param name="inventory"></param>
    public void TrySetGun(GunState state, IInventory inventory)
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
                    inventory.RemoveItem(newState.Data.ItemKind);
                    ShowInventory();
                    return;
                }
            }
        }
    }


    /// <summary>
    /// Вызывается при попытке одеть пушку на корабль.
    /// </summary>
    /// <param name="gunKind"></param>
    public void TrySetGun(ItemKind gunKind)
    {
        if (guns.Count < maxNumGuns)
        {
            var newState = CreateGunStateObject(gunKind);

            this.guns.Add(newState);
            ShowInventory();
        }
        else
        {
            for (int i = 0; i < maxNumGuns; i++)
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

    /// <summary>
    /// Получить список пушек, одетых на корабль.
    /// </summary>
    /// <returns></returns>
    public List<GunState> GetGuns()
    {
        return this.guns;
    }

    /// <summary>
    /// Выкинуть\снять пушку с корабля.
    /// </summary>
    /// <param name="state"></param>
    public void TryUnsetGun(GunState state)
    {
        guns.Remove(state);
        GameObject.Destroy(state.gameObject);
        while(guns.Count < maxNumGuns)
        {
            guns.Add(CreateEmptyGunState());
        }
        ShowInventory();
    }

    public void RemoveAllEquipmentFromShip()
    {
        foreach (var gun in this.guns)
        {
            Managers.Player.Controller.Inventory.AddItem(gun.Data.ItemKind,gun);
        }

        guns.Clear();
        while(guns.Count < maxNumGuns)
        {
            guns.Add(CreateEmptyGunState());
        }
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

    private GunState CreateEmptyGunState()
    {
        return CreateGunStateObject(ItemKind.EmptyItem);
    }
}
