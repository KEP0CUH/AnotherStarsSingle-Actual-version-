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
    #endregion

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
                Object.Destroy(state.gameObject);
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
                Object.Destroy(state.gameObject);
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
                Object.Destroy(state.gameObject);
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
                Object.Destroy(state.gameObject);
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
                Managers.Player.Controller.Inventory.RemoveItem(state);
                Object.Destroy(state.gameObject);
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
                Object.Destroy(state.gameObject);
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
                Object.Destroy(state.gameObject);
                ShowInventory();
                return;
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
                Object.Destroy(state.gameObject);
                ShowInventory();
                return;
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


    #region Снять всё оборудования с корабля. Обычно используется при покупке нового корабля.
    public void RemoveAllEquipmentFromShip()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            if(guns[i].Data.ItemKind != ItemKind.weaponEmpty)
            {
                TryUnsetGun(guns[i]);
            }
        }
        for(int i = 0; i < devices.Count;i++)
        {
            if(devices[i].Data.ItemKind != ItemKind.deviceEmpty)
            {
                TryUnsetDevice(devices[i]);
            }
        }

        for(int i = 0; i < devices.Count;i++)
        {
            Object.Destroy(devices[i].gameObject);
        }
        for (int i = 0; i < guns.Count; i++)
        {
            Object.Destroy(guns[i].gameObject);
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
            Object.Destroy(state.gameObject);
        }
        else
        {
            var item = CreateDrop(state);
            Managers.Player.Controller.Inventory.RemoveItem(state);
            if(state.Count <= 0)
            {
                Object.Destroy(state.gameObject);
            }
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
        item.transform.position = shipState.gameObject.transform.position;

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
