using System.Collections.Generic;
using UnityEngine;

public class ShipInventory : IShipInventory
{
    private Transform parent;
    private ShipState shipState;
    private InventoryController inventory;
    public ShipState ShipState => shipState;

    private List<GunState> guns;
    private int maxNumGuns;

    private List<DeviceState> devices;
    private int maxNumDevices;

    public event System.Action OnInteractWithEquipment;


    public ShipInventory(Transform parent,ShipState state,InventoryController inventory)
    {
        this.parent = parent;
        this.shipState = state;
        this.maxNumGuns = state.Data.MaxGuns;
        this.maxNumDevices = state.Data.MaxDevices;
        this.inventory = inventory;

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
        if (state.IsWeapon)
        {
            if (state.IsSet)
            {
                TryUnsetGun(state);
                OnInteractWithEquipment?.Invoke();
            }
            else if (state.IsSet == false)
            {
                TrySetGun(state);
                OnInteractWithEquipment?.Invoke();
            }
        }
        else if (state.IsDevice)
        {
            if (state.IsSet)
            {
                TryUnsetDevice(state);
                OnInteractWithEquipment?.Invoke();
            }
            else if (state.IsSet == false)
            {
                TrySetDevice(state);
                OnInteractWithEquipment?.Invoke();
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
    private void TrySetGun(ItemState state)
    {
        for (int i = 0; i < maxNumGuns; i++)
        {
            if (guns[i].Data.ItemKind == ItemKind.EmptyGun)
            {
                Object.Destroy(guns[i].gameObject);
                guns[i] = CreateGunStateObject(state);
                guns[i].SetIsTrue();
                inventory.RemoveItem(state, 1, true);
                ShowInventory();
                return;
            }
        }
        return;
    }

    private void TryUnsetGun(ItemState state)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            if (guns[i] == state)
            {
                guns[i] = CreateEmptyGunState();
                guns[i].SetIsFalse();
                state.SetIsFalse();
                inventory.AddItem(state, 1, true);
                ShowInventory();
                return;
            }
        }
        return;
    }
    #endregion

    #region TRY_SET/UNSET_DEVICE
    private void TrySetDevice(ItemState state)
    {
        for(int i = 0; i < maxNumDevices; i++)
        {
            if(devices[i].Data.ItemKind == ItemKind.EmptyDevice)
            {
                Object.Destroy(devices[i]);
                devices[i] = CreateDeviceStateObject(state);
                devices[i].SetIsTrue();
                inventory.RemoveItem(state);
                Object.Destroy(state.gameObject);
                ShowInventory();
                return;
            }
        }
    }

    private void TryUnsetDevice(ItemState state)
    {
        for(int i = 0; i < devices.Count;i++)
        {
            if(devices[i] == state)
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
            if(guns[i].Data.ItemKind != ItemKind.EmptyGun)
            {
                TryUnsetGun(guns[i]);
            }
        }
        for(int i = 0; i < devices.Count;i++)
        {
            if(devices[i].Data.ItemKind != ItemKind.EmptyDevice)
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
            inventory.RemoveItem(state);
            Object.Destroy(state.gameObject);
        }
        else
        {
            var item = CreateDrop(state);
            inventory.RemoveItem(state);
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
        OnInteractWithEquipment?.Invoke();
    }
    #endregion


    #region Воссоздание нового состояния итема исходя из другого состояния. 
    private GunState CreateGunStateObject(ItemState state)
    {
        ItemState newItemState;

        var newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(GunState));
        newItemStateObj.GetComponent<Transform>().SetParent(parent);
        newItemState = newItemStateObj.GetComponent<GunState>().Init(state);
        return (GunState)newItemState;
    }

    private DeviceState CreateDeviceStateObject(ItemState state)
    {
        ItemState newItemState;

        var newItemStateObj = new GameObject(($"{state.Data.Title}"), typeof(DeviceState));
        newItemStateObj.GetComponent<Transform>().SetParent(parent);
        newItemState = newItemStateObj.GetComponent<DeviceState>().Init(state);

        return (DeviceState)newItemState;
    }
    #endregion


    #region Воссоздание нового состояния итема исходя из вида итема.
    private GunState CreateGunStateObject(ItemKind kind)
    {
        var gunDefault = new GameObject("DefaultGun", typeof(GunState));
        gunDefault.GetComponent<Transform>().SetParent(parent);
        var gunState = gunDefault.GetComponent<GunState>();
        gunState.Init(kind, 1);
        gunState.SetIsTrue();

        return gunState;
    }

    private DeviceState CreateDeviceStateObject(ItemKind kind)
    {
        var deviceDefault = new GameObject("DefaultDevice", typeof(DeviceState));
        deviceDefault.GetComponent<Transform>().SetParent(parent);
        var deviceState = deviceDefault.GetComponent<DeviceState>();
        deviceState.Init(kind, 1);
        deviceState.SetIsTrue();

        return deviceState;
    }
    #endregion


    #region Создание пустых слотов для инвентаря.

    private GunState CreateEmptyGunState()
    {
        return CreateGunStateObject(ItemKind.EmptyGun);
    }

    private DeviceState CreateEmptyDeviceState()
    {
        return CreateDeviceStateObject(ItemKind.EmptyDevice);
    }
    #endregion

    private GameObject CreateDrop(ItemState state)
    {
        var item = new GameObject(state.Data.Title,typeof(ItemController));
        item.transform.position = shipState.gameObject.transform.position;
        item.GetComponent<ItemController>().Init(state.Data.ItemKind, 1);
        return item;
    }

}
