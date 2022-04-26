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

   /*     var defaultGun = new GameObject("GunDefault", typeof(GunState));
        defaultGun.GetComponent<GunState>().Init(ItemKind.weaponMultiblaster, 1);
        TrySetGun(defaultGun.GetComponent<GunState>());*/


        ShowInventory();
    }

    #region Одеть(Снять) определенное оборудование на(с) корабль(я)

    public void TryInteractWithItem(ItemState state)
    {
        if (state.IsWeapon)
        {
            if (state.IsSet)
            {
                TryUnsetGun((GunState)state);
            }
            else if (state.IsSet == false)
            {
                TrySetGun((GunState)state);
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
                TrySetDevice((DeviceState)state);
            }
        }
        else
        {
            Debug.Log("Ни рыба ни мясо");
            return;
        }
    }

    public void TryInteractWithItemFromInventory(ItemState state,IPlayerInventory inventory)
    {
        if (state.IsWeapon)
        {
            if (state.IsSet)
            {
                TryUnsetGun((GunState)state,inventory);
            }
            else if (state.IsSet == false)
            {
                TrySetGun((GunState)state,inventory);
            }
        }
        else if (state.IsDevice)
        {
            if (state.IsSet)
            {
                TryUnsetDevice((DeviceState)state,inventory);
            }
            else if (state.IsSet == false)
            {
                TrySetDevice((DeviceState)state,inventory);
            }
        }
        else
        {
            Debug.Log("Ни рыба ни мясо");
            return;
        }
    }

    #region TRY_SET/UNSET_GUN
    private void TrySetGun(GunState state)
    {
        for (int i = 0; i < maxNumGuns; i++)
        {
            if (guns[i].Data.ItemKind == ItemKind.weaponEmpty)
            {
                Object.Destroy(guns[i].gameObject);
                guns[i] = CreateGunStateObject(state);
                guns[i].SetIsTrue();
                Managers.Player.Controller.Inventory.RemoveItem(state);
                ShowInventory();
                return;
            }
        }
        return;
    }

    private void TrySetGun(GunState state,IPlayerInventory inventory)
    {
        for (int i = 0; i < maxNumGuns; i++)
        {
            if (guns[i].Data.ItemKind == ItemKind.weaponEmpty)
            {
                Object.Destroy(guns[i].gameObject);
                guns[i] = CreateGunStateObject(state);
                guns[i].SetIsTrue();
                state.SetIsTrue();
                inventory.RemoveItem(state);
                ShowInventory();
                return;
            }
        }
        return;
    }

    private void TryUnsetGun(GunState state)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            if (guns[i] == state)
            {
                guns[i] = CreateEmptyGunState();
                guns[i].SetIsFalse();
                state.SetIsFalse();
                Managers.Player.Controller.Inventory.AddItem(state);
                ShowInventory();
                return;
            }
        }
        return;
    }

    private void TryUnsetGun(GunState state,IPlayerInventory inventory)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            if (guns[i] == state)
            {
                guns[i] = CreateEmptyGunState();
                guns[i].SetIsFalse();
                state.SetIsFalse();
                inventory.AddItem(state);
                ShowInventory();
                return;
            }
        }
        return;
    }
    #endregion

    #region TRY_SET/UNSET_DEVICE
    private void TrySetDevice(DeviceState state)
    {
        for(int i = 0; i < maxNumDevices; i++)
        {
            if(devices[i].Data.ItemKind == ItemKind.deviceEmpty)
            {
                Object.Destroy(devices[i]);
                devices[i] = CreateDeviceStateObject(state);
                devices[i].SetIsTrue();
                Managers.Player.Controller.Inventory.RemoveItem(devices[i]);
                ShowInventory();
                return;
            }
        }
    }

    private void TrySetDevice(DeviceState state,IPlayerInventory inventory)
    {
        for (int i = 0; i < maxNumDevices; i++)
        {
            if (devices[i].Data.ItemKind == ItemKind.deviceEmpty)
            {
                Object.Destroy(devices[i]);
                devices[i] = CreateDeviceStateObject(state);
                devices[i].SetIsTrue();
                state.SetIsTrue();
                inventory.RemoveItem(state);
                ShowInventory();
                return;
            }
        }
    }

    private void TryUnsetDevice(DeviceState state)
    {
        for(int i = 0; i < devices.Count;i++)
        {
            if(devices[i] == state)
            {
                devices[i] = CreateEmptyDeviceState();
                devices[i].SetIsFalse();
                state.SetIsFalse();
                Managers.Player.Controller.Inventory.AddItem(state);
                ShowInventory();
            }
        }
    }

    private void TryUnsetDevice(DeviceState state,IPlayerInventory inventory)
    {
        for(int i = 0; i < devices.Count;i++)
        {
            if (devices[i] == state)
            {
                devices[i] = CreateEmptyDeviceState();
                devices[i].SetIsFalse();
                state.SetIsFalse();
                inventory.AddItem(state);
                ShowInventory();
            }
        }
    }
    #endregion



    /*
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
        }*/
    /*
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
                        if (devices[i] != null) Object.Destroy(devices[i].gameObject);
                        devices[i] = newState;
                        ShowInventory();
                        return;
                    }
                }
            }
        }*/
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


/*    #region Снять определенное оборудование с корабля
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
    #endregion*/


    #region Снять всё оборудования с корабля. Обычно используется при покупке нового корабля.
    public void RemoveAllEquipmentFromShip()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            if (guns[i].Data.ItemKind == ItemKind.weaponEmpty)
            {
                Object.Destroy(guns[i].gameObject);
                guns[i] = null;
            }
            else
            {
                TryUnsetGun(guns[i]);
            }
        }

        for (int i = 0; i < devices.Count; i++)
        {
            if (devices[i].Data.ItemKind == ItemKind.deviceEmpty)
            {
                Object.Destroy(devices[i].gameObject);
                devices[i] = null;
            }
            else
            {
                Managers.Player.Controller.Inventory.AddItem(devices[i]);
            }
        }

        guns.Clear();
        devices.Clear();
        ShowInventory();
    }
    #endregion

    #region ВЫБРОСИТЬ УКАЗАННОЕ ОБОРУДОВАНИЕ С КОРАБЛЯ(УСТАНОВЛЕННАЯ ПУШКА ИЛИ УСТРОЙСТВО)
    public void TryDropItemFromShip(ItemState state)
    {
        if(state.IsSet)
        {
            TryInteractWithItem(state);
            var item = CreateDrop(state);
            Managers.Player.Controller.Inventory.RemoveItem(state);
        }


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

        newItemState.Init(state);
        return (GunState)newItemState;
    }

    private DeviceState CreateDeviceStateObject(DeviceState state)
    {
        ItemState newItemState;

        var newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(DeviceState));
        newItemState = newItemStateObj.GetComponent<DeviceState>();

        newItemState.Init(state);
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

    private GameObject CreateDrop(ItemState state)
    {
        var item = new GameObject(state.Data.Title);
        if (state.IsWeapon)
        {
            item.AddComponent<GunViewGame>().Init(((GunState)state).Data.ItemKind, 1);
        }
        else if (state.IsDevice)
        {
            item.AddComponent<DeviceViewGame>().Init(((DeviceState)state).Data.ItemKind, 1);
        }
        else
        {
            item.AddComponent<ItemViewGame>().Init(state.Data.ItemKind, 1);
        }
        return item;
    }

}
