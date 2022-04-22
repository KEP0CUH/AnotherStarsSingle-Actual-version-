using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShipInventory : IShipInventory
{
    private List<GunState> guns;
    private int maxNumGuns;

    private List<DeviceState> devices;
    private int maxNumDevices;

    /// <summary>
    /// Конструктор оборудоваемых слотов корабля. Изначально все ячейки заполняются пустышками, которые будут заменены при взаимодействии.
    /// </summary>
    /// <param name="maxNumGuns">Максимальное число оружий одеваемых на корабль.</param>
    public ShipInventory(int maxNumGuns, int maxNumDevices)
    {
        this.maxNumGuns = maxNumGuns;
        guns = new List<GunState>();

        this.maxNumDevices = maxNumDevices;
        devices = new List<DeviceState>();

        for (int i = 0; i < maxNumGuns; i++)
        {
            guns.Add(CreateEmptyGunState());
        }

        for(int i = 0; i < maxNumDevices; i++)
        {
            devices.Add(CreateEmptyDeviceState());
        }

        ShowInventory();
    }

    /// <summary>
    /// Вызывается при попытке одеть пушку на корабль, как правило при начальной инициализации корабля. Part 1 \ 3
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
                    GameObject.Destroy(guns[i].gameObject);
                    guns[i] = newState;
                    ShowInventory();
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Вызывается при попытке одеть устройство на корабль, как правило при начальной инициализации корабля. Part 1 \ 3
    /// </summary>
    /// <param name="state">Состояние пушки.</param>
    public void TrySetDevice(DeviceState state)
    {
        if (devices.Count < maxNumDevices)
        {
            var newState = CreateDeviceStateObject(state);
            this.devices.Add(newState);
            ShowInventory();
        }
        else
        {
            for(int i = 0; i < maxNumDevices;i++)
            {
                if(devices[i].Data.ItemKind == ItemKind.deviceEmpty)
                {
                    var newState = CreateDeviceStateObject(state);
                    GameObject.Destroy(devices[i].gameObject);
                    devices[i] = newState;
                    ShowInventory();
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Вызывается при попытке одеть пушку из инвентаря игрока на корабль. Part 2 \ 3
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
                    GameObject.Destroy(guns[i].gameObject);
                    guns[i] = newState;
                    inventory.RemoveItem(newState.Data.ItemKind);
                    ShowInventory();
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Вызывается при попытке одеть устройство из инвентаря игрока на корабль. Part 2 \ 3
    /// </summary>
    /// <param name="state"></param>
    /// <param name="inventory"></param>
    public void TrySetDevice(DeviceState state, IInventory inventory)
    {
        if (devices.Count < maxNumDevices)
        {
            var newState = CreateDeviceStateObject(state);

            this.devices.Add(newState);
            inventory.RemoveItem(newState.Data.ItemKind);
            ShowInventory();
        }
        else
        {
            for (int i = 0; i < maxNumDevices; i++)
            {
                if(devices[i].Data.ItemKind == ItemKind.deviceEmpty)
                {
                    var newState = CreateDeviceStateObject(state);
                    GameObject.Destroy(devices[i].gameObject);
                    devices[i] = newState;
                    inventory.RemoveItem(newState.Data.ItemKind);
                    ShowInventory();
                    return;
                }
            }
        }
    }


    /// <summary>
    /// Вызывается при попытке одеть пушку на корабль.  Part 3 \ 3
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
                    GameObject.Destroy(guns[i].gameObject);
                    guns[i] = newState;
                    ShowInventory();
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Вызывается при попытке одеть устройство на корабль.  Part 3 \ 3
    /// </summary>
    /// <param name="gunKind"></param>
    public void TrySetDevice(ItemKind deviceKind)
    {
        if (devices.Count < maxNumDevices)
        {
            var newState = CreateDeviceStateObject(deviceKind);

            this.devices.Add(newState);
            ShowInventory();
        }
        else
        {
            for(int i = 0; i < maxNumDevices; i++)
            {
                if(devices[i].Data.ItemKind == ItemKind.deviceEmpty)
                {
                    var newState = CreateDeviceStateObject(deviceKind);
                    GameObject.Destroy(devices[i].gameObject);
                    devices[i] = newState;
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

    public List<DeviceState> GetDevices()
    {
        return this.devices;
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

    /// <summary>
    /// Выкинуть\снять устройство с корабля.
    /// </summary>
    /// <param name="state"></param>
    public void TryUnsetDevice(DeviceState state)
    {
        devices.Remove(state);
        GameObject.Destroy(state.gameObject);
        while (devices.Count < maxNumDevices)
        {
            devices.Add(CreateEmptyDeviceState());
        }
        ShowInventory();
    }

    public void RemoveAllEquipmentFromShip()
    {
        for(int i = 0; i < guns.Count;i++)
        {
            if(guns[i].Data.ItemKind == ItemKind.EmptyItem)
            {
                GameObject.Destroy(guns[i].gameObject);
                guns[i] = null;
            }
            else
            {
                Managers.Player.Controller.Inventory.AddItem(guns[i].Data.ItemKind, guns[i]);
            }
        }

        for(int i = 0; i < devices.Count;i++)
        {
            if(devices[i].Data.ItemKind == ItemKind.deviceEmpty)
            {
                GameObject.Destroy(devices[i].gameObject);
                devices[i] = null;
            }
            else
            {
                Managers.Player.Controller.Inventory.AddItem(devices[i].Data.ItemKind, devices[i]);
            }
        }

        guns.Clear();
        devices.Clear();
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
        CanvasUI.Inventory.ShowInventory(this, devices);
    }



    private GunState CreateGunStateObject(GunState state)
    {
        BaseItemState newItemState;

        var newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(GunState));
        newItemState = newItemStateObj.GetComponent<GunState>();

        newItemState.Init(state.Data.ItemKind, state.Count);
        return (GunState)newItemState;
    }

    private DeviceState CreateDeviceStateObject(DeviceState state)
    {
        BaseItemState newItemState;

        var newItemStateObj = new GameObject(($"{state.Data.Title}"),typeof(DeviceState));
        newItemState = newItemStateObj.GetComponent<DeviceState>();

        newItemState.Init(state.Data.ItemKind, state.Count);
        return (DeviceState)newItemState;
    }

    private GunState CreateGunStateObject(ItemKind kind)
    {
        var gunDefault = new GameObject("DefaultGun", typeof(GunState));
        var gunState = gunDefault.GetComponent<GunState>();
        gunState.Init(kind, 1);

        return gunState;
    }

    private DeviceState CreateDeviceStateObject(ItemKind kind)
    {
        var deviceDefault = new GameObject("DefaultDevice",typeof(DeviceState));
        var deviceState = deviceDefault.GetComponent<DeviceState>();
        deviceState.Init(kind, 1);

        return deviceState;
    }

    private GunState CreateEmptyGunState()
    {
        return CreateGunStateObject(ItemKind.EmptyItem);
    }

    private DeviceState CreateEmptyDeviceState()
    {
        return CreateDeviceStateObject(ItemKind.deviceEmpty);
    }

    
}
