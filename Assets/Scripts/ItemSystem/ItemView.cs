///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class ItemView : MonoBehaviour
{
    [SerializeField]
    protected           ItemState               state;
    public              ItemState               State => state;

    private             ItemController          controller;

    private             bool                    triggerWorked   = false;
    private             bool                    needTake        = false;

    private static      GameObject              infoWindow      = null;
    private static      bool                    isClicked       = false;

    public virtual      ItemView                Init(ItemKind kind, int count)
    {
        var data = Managers.Resources.DownloadData(kind);
        if (data.IsWeapon())
        {
            CreateGun(kind, count);
        }
        else if (data.IsDevice())
        {
            CreateDevice(kind, count);
        }
        else
        {
            CreateBaseItem(kind, count);
        }

        needTake = false;

        return this;
    }

    public virtual      ItemView                Init(ItemController controller)
    {
        this.controller = controller;
        this.state = controller.State;

        this.GetComponent<SpriteRenderer>().sprite = controller.State.Data.Icon;
        this.GetComponent<SphereCollider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;

        return this;
    }

    public virtual      void                    TakeItem()
    {
        triggerWorked = false;
        CloseInfoWindow();
    }

    public virtual      void                    NeedTake()
    {
        this.needTake = true;
    }

    private             void                    CreateGun(ItemKind kind, int ammoCount)
    {
        gameObject.AddComponent<GunState>();
        state = gameObject.GetComponent<GunState>();
        this.state.Init(kind, ammoCount);
        this.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        this.GetComponent<SphereCollider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    private             void                    CreateDevice(ItemKind kind, int count)
    {
        gameObject.AddComponent<DeviceState>();
        state = gameObject.GetComponent<DeviceState>();
        this.state.Init(kind, count);
        this.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        this.GetComponent<SphereCollider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    private             void                    CreateBaseItem(ItemKind kind, int itemCount)
    {
        gameObject.AddComponent<ItemState>();
        state = gameObject.GetComponent<ItemState>();
        this.state.Init(kind, itemCount);
        gameObject.GetComponent<SphereCollider>().isTrigger = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    protected           void                    OnTriggerEnter(Collider other)
    {
        if (triggerWorked == false)
        {
            if(other.GetComponent<IItemHandler>() != null)
            {
                if(needTake)
                {
                    triggerWorked = true;
                    other.GetComponent<IItemHandler>().PickupItem(this.state);
                }
            }
        }
    }

    protected           void                    OnTriggerStay(Collider other)
    {
        TakeItem();
    }

    protected           void                    OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            triggerWorked = false;
        }
        if(other.GetComponent<IItemHandler>() != null)
        {
            triggerWorked = false;
        }
    }

    protected           void                    OnMouseDown()
    {
        isClicked = true;
        CreateInfoWindow();
    }

    private             void                    CreateInfoWindow()
    {
        if (infoWindow != null)
        {
            Object.Destroy(infoWindow.gameObject);
        }

        infoWindow = Instantiate(Managers.Resources.DownloadData(ObjectType.ItemWindow));
        infoWindow.GetComponent<ItemWindow>().Init(this, this.state);
        Managers.Canvas.AddModule(infoWindow);
    }

    public              void                    CloseInfoWindow()
    {
        if (infoWindow != null)
        {
            Destroy(infoWindow.gameObject);
            infoWindow = null;
            isClicked = false;
        }
    } 
}
