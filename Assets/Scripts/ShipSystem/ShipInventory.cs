using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShipInventory : IShipInventory
{
    private ShipState shipState;

    public ShipState ShipState => ShipState;

    private List<GunState> guns;
    private int maxNumGuns;

    private List<DeviceState> devices;
    private int maxNumDevices;

    /// <summary>
    /// Конструктор оборудоваемых слотов корабля. Изначально все ячейки заполняются пустышками, которые будут заменены при взаимодействии.
    /// </summary>
    /// <param name="maxNumGuns">Максимальное число оружий одеваемых на корабль.</param>
    public ShipInventory(ShipState shipState, int maxNumGuns, int maxNumDevices)
    {
        this.shipState = shipState;
        this.maxNumGuns = maxNumGuns;
        this.maxNumDevices = maxNumDevices;


        guns = new List<GunState>();
        for (int i = 0; i < maxNumGuns; i++)
        {
            guns.Add(CreateEmptyGunState());
        }

        devices = new List<DeviceState>();
        for (int i = 0; i < maxNumDevices; i++)
        {
            devices.Add(CreateEmptyDeviceState());
        }

        ShowInventory();
    }

    #region Одеть(Снять) определенное оборудование на(с) корабль(я)

    public void TryInteractWithItem(ItemState state)
    {
        bool success = false;
        if (state.IsWeapon)
        {
            if (state.IsSet)
            {
                TryUnsetGun((GunState)state, out success);
                if (success) state.SetIsFalse();
            }
            else if (state.IsSet == false)
            {
                TrySetGun((GunState)state, out success);
                if (success) state.SetIsTrue();
            }
        }
        else if (state.IsDevice)
        {
            if (state.IsSet)
            {
                TryUnsetDevice((DeviceState)state);
                state.SetIsFalse();
            }
            else if (state.IsSet == false)
            {
                TrySetDevice((DeviceState)state, out success);
                if (success) state.SetIsTrue();
            }
        }
        else
        {
            Debug.Log("Ни рыба ни мясо");
            return;
        }
    }

    public void TryInteractWithItemFromInventory(ItemState state,IInventory inventory)
    {
        bool success = false;
        if (state.IsWeapon)
        {
            if (state.IsSet)
            {
                TryUnsetGun((GunState)state,inventory, out success);
                if (success) state.SetIsFalse();
            }
            else if (state.IsSet == false)
            {
                TrySetGun((GunState)state,inventory, out success);
                if (success) state.SetIsTrue();
            }
        }
        else if (state.IsDevice)
        {
            if (state.IsSet)
            {
                //TryUnsetDevice((DeviceState)state,inventory);
                state.SetIsFalse();
            }
            else if (state.IsSet == false)
            {
                //TrySetDevice((DeviceState)state,inventory, out success);
                if (success) state.SetIsTrue();
            }
        }
        else
        {
            Debug.Log("Ни рыба ни мясо");
            return;
        }
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
            newState.SetIsTrue();
            ShowInventory();
        }
        else
        {
            for (int i = 0; i < maxNumGuns; i++)
            {
                if (guns[i].Data.ItemKind == ItemKind.weaponEmpty)
                {
                    var newState = CreateGunStateObject(state);
                    newState.SetIsTrue();
                    if (guns[i] != null) GameObject.Destroy(guns[i].gameObject);
                    guns[i] = newState;
                    ShowInventory();
                    return;
                }
            }
        }
    }


    #region TRY_SET/UNSET_GUN
    private void TrySetGun(GunState state, out bool success)
    {
        success = false;
        for (int i = 0; i < maxNumGuns; i++)
        {
            if (guns[i].Data.ItemKind == ItemKind.weaponEmpty)
            {
                GameObject.Destroy(guns[i].gameObject);
                guns[i] = CreateGunStateObject(state);
                guns[i].SetIsTrue();
                success = true;
                ShowInventory();
                return;
            }
        }
        return;
    }

    private void TrySetGun(GunState state,IInventory inventory,out bool success)
    {
        TrySetGun(state, out success);
        if(success)
        {
            inventory.RemoveItem(state.Data.ItemKind);
        }
        else if(success == false)
        {
            return;
        }
    }

    private void TryUnsetGun(GunState state, out bool success)
    {
        success = false;
        for (int i = 0; i < guns.Count; i++)
        {
            if (guns[i] == state)
            {
                guns[i] = CreateEmptyGunState();
                GameObject.Destroy(state.gameObject);
                success = true;
                guns[i].SetIsFalse();
                Managers.Player.Controller.Inventory.AddItem(state.Data.ItemKind,state);
                ShowInventory();
                return;
            }
        }
        return;
    }

    private void TryUnsetGun(GunState state,IInventory inventory, out bool success)
    {
        TryUnsetGun(state, out success);
        if (success)
        {
            inventory.AddItem(state.Data.ItemKind,state);
        }
        else if (success == false)
        {
            return;
        }
    }
    #endregion

    /// <summary>
    /// Вызывается при попытке одеть устройство на корабль, как правило при начальной инициализации корабля. Part 1 \ 3
    /// </summary>
    /// <param name="state">Состояние пушки.</param>
    public void TrySetDevice(DeviceState state)
    {
        if (devices.Count < maxNumDevices)
        {
            var newState = CreateDeviceStateObject(state);
            newState.SetIsTrue();
            this.devices.Add(newState);
            ShowInventory();
        }
        else
        {
            for (int i = 0; i < maxNumDevices; i++)
            {
                if (devices[i].Data.ItemKind == ItemKind.deviceEmpty)
                {
                    var newState = CreateDeviceStateObject(state);
                    newState.SetIsTrue();
                    GameObject.Destroy(devices[i].gameObject);
                    devices[i] = newState;
                    ShowInventory();
                    return;
                }
            }
        }
    }

    private void TrySetDevice(DeviceState state, out bool success)
    {
        success = false;
        for (int i = 0; i < devices.Count; i++)
        {
            if (devices[i].Data.ItemKind == ItemKind.deviceEmpty)
            {
                GameObject.Destroy(devices[i].gameObject);
                devices[i] = CreateDeviceStateObject(state);
                success = true;
                return;
            }
        }
        return;
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
            newState.SetIsTrue();
            this.guns.Add(newState);
            inventory.RemoveItem(newState.Data.ItemKind);
            ShowInventory();
        }
        else
        {
            for (int i = 0; i < maxNumGuns; i++)
            {
                if (guns[i].Data.ItemKind == ItemKind.weaponEmpty)
                {
                    var newState = CreateGunStateObject(state);
                    newState.SetIsTrue();
                    if (guns[i] != null) GameObject.Destroy(guns[i].gameObject);
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
            newState.SetIsTrue();
            this.devices.Add(newState);
            inventory.RemoveItem(newState.Data.ItemKind);
            ShowInventory();
        }
        else
        {
            for (int i = 0; i < maxNumDevices; i++)
            {
                if (devices[i].Data.ItemKind == ItemKind.deviceEmpty)
                {
                    var newState = CreateDeviceStateObject(state);
                    GameObject.Destroy(devices[i].gameObject);
                    devices[i] = newState;
                    newState.SetIsTrue();
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
            newState.SetIsTrue();
            this.guns.Add(newState);
            ShowInventory();
        }
        else
        {
            for (int i = 0; i < maxNumGuns; i++)
            {
                if (guns[i].Data.ItemKind == ItemKind.weaponEmpty)
                {
                    var newState = CreateGunStateObject(gunKind);
                    newState.SetIsTrue();
                    if (guns[i] != null) GameObject.Destroy(guns[i].gameObject);
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
            newState.SetIsTrue();
            this.devices.Add(newState);
            ShowInventory();
        }
        else
        {
            for (int i = 0; i < maxNumDevices; i++)
            {
                if (devices[i].Data.ItemKind == ItemKind.deviceEmpty)
                {
                    var newState = CreateDeviceStateObject(deviceKind);
                    newState.SetIsTrue();
                    if (devices[i] != null) GameObject.Destroy(devices[i].gameObject);
                    devices[i] = newState;
                    ShowInventory();
                    return;
                }
            }
        }
    }
    #endregion


    #region Получить списки одетого на корабль оборудования.

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
    #endregion


    #region Снять определенное оборудование с корабля
    /// <summary>
    /// Выкинуть\снять пушку с корабля.
    /// </summary>
    /// <param name="state"></param>
    public void TryUnsetGun(GunState state)
    {
        guns.Remove(state);
        if (state.gameObject != null) GameObject.Destroy(state.gameObject);
        while (guns.Count < maxNumGuns)
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
        state.SetIsFalse();
        devices.Remove(state);
        if (state.gameObject != null) GameObject.Destroy(state.gameObject);
        while (devices.Count < maxNumDevices)
        {
            devices.Add(CreateEmptyDeviceState());
        }
        ShowInventory();
    }
    #endregion


    #region Снять всё оборудования с корабля. Обычно используется при покупке нового корабля.
    public void RemoveAllEquipmentFromShip()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            if (guns[i].Data.ItemKind == ItemKind.weaponEmpty)
            {
                GameObject.Destroy(guns[i].gameObject);
                guns[i] = null;
            }
            else
            {
                Managers.Player.Controller.Inventory.AddItem(guns[i].Data.ItemKind, guns[i]);
            }
        }

        for (int i = 0; i < devices.Count; i++)
        {
            if (devices[i].Data.ItemKind == ItemKind.deviceEmpty)
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
    #endregion


    #region Показ инвентаря корабля(т.е. одетого на него оборудования)
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
    #endregion


    #region Воссоздание нового состояния итема исходя из другого состояния. 
    private GunState CreateGunStateObject(GunState state)
    {
        ItemState newItemState;

        var newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(GunState));
        newItemState = newItemStateObj.GetComponent<GunState>();

        newItemState.Init(state.Data.ItemKind, state.Count);
        return (GunState)newItemState;
    }

    private DeviceState CreateDeviceStateObject(DeviceState state)
    {
        ItemState newItemState;

        var newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(DeviceState));
        newItemState = newItemStateObj.GetComponent<DeviceState>();

        newItemState.Init(state.Data.ItemKind, state.Count);
        return (DeviceState)newItemState;
    }
    #endregion


    #region Воссоздание нового состояния итема исходя из вида итема.
    private GunState CreateGunStateObject(ItemKind kind)
    {
        var gunDefault = new GameObject("DefaultGun", typeof(GunState));
        var gunState = gunDefault.GetComponent<GunState>();
        gunState.Init(kind, 1);

        return gunState;
    }

    private DeviceState CreateDeviceStateObject(ItemKind kind)
    {
        var deviceDefault = new GameObject("DefaultDevice", typeof(DeviceState));
        var deviceState = deviceDefault.GetComponent<DeviceState>();
        deviceState.Init(kind, 1);

        return deviceState;
    }
    #endregion


    #region Создание пустых слотов для инвентаря.

    private GunState CreateEmptyGunState()
    {
        return CreateGunStateObject(ItemKind.weaponEmpty);
    }

    private DeviceState CreateEmptyDeviceState()
    {
        return CreateDeviceStateObject(ItemKind.deviceEmpty);
    }
    #endregion


}
