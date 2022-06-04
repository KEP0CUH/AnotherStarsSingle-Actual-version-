using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class ItemViewGame : MonoBehaviour, Interactable, IObservable
{
    [SerializeField]
    protected ItemState state;
    private IPlayerInventory inventory;
    private List<IObserver> observers = new List<IObserver>();
    public ItemState State => state;

    private bool triggerWorked = false;

    private Action<ItemKind,ItemState> onItemDrop;
    private Action<ItemKind, int> inItemAddedInventory;

    private static GameObject infoWindow = null;
    private static bool isClicked = false;

    public virtual void Init(ItemKind kind, int count)
    {
        var data = Managers.Resources.DownloadData(kind);
        if (data.IsWeapon())
        {
            CreateGun(kind,count);
        }
        else if(data.IsDevice())
        {
            CreateDevice(kind, count);
        }
        else
        {
            CreateBaseItem(kind, count);
        }
    }

    private void CreateGun(ItemKind kind, int ammoCount)
    {
        gameObject.AddComponent<GunState>();
        state = gameObject.GetComponent<GunState>();
        this.state.Init(kind, ammoCount);
        this.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        this.GetComponent<BoxCollider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void CreateDevice(ItemKind kind, int count)
    {
        gameObject.AddComponent<DeviceState>();
        state = gameObject.GetComponent<DeviceState>();
        this.state.Init(kind, count);
        this.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        this.GetComponent<BoxCollider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void CreateBaseItem(ItemKind kind, int itemCount)
    {
        gameObject.AddComponent<ItemState>();
        state = gameObject.GetComponent<ItemState>();
        this.state.Init(kind, itemCount);
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (triggerWorked == false)
        {
            if (other.GetComponent<PlayerController>() && Input.GetKey(KeyCode.Space) && (onItemDrop != null) )
            {
                triggerWorked = true;
                inventory = other.GetComponent<PlayerController>().Inventory;
                for(int i = 0; i < this.state.Count;i++)
                {
                    onItemDrop.Invoke(this.state.Data.ItemKind, state);
                }
            }
        }
    }

    public void AddObserver(IObserver observer,EventType eventType)
    {
        if (observers.Contains(observer))
        {
            return;
        }

        if(eventType == EventType.OnItemDropped)
        {
            onItemDrop += (kind, state) =>
            {
                observers.Add(observer);
                observer.Invoke(eventType,kind, state);
                Destroy(this.gameObject);
            };
        }
    }

    public void OnPickup()
    {
        throw new NotImplementedException();
    }

    public void OnDrop()
    {
        throw new NotImplementedException();
    }

    public void RemoveObserver(IObserver observer)
    {
        throw new NotImplementedException();
    }

    private void OnMouseEnter()
    {
        CreateInfoWindow();
    }

    private void OnMouseDown()
    {
        isClicked = true;
    }

    private void OnMouseExit()
    {
        if (isClicked == false)
        {
            CloseInfoWindow();
        }
    }

    private void CreateInfoWindow()
    {
        if(infoWindow == null)
        {
            infoWindow = new GameObject("InfoWindow", typeof(ItemInfoWindow));
            infoWindow.GetComponent<ItemInfoWindow>().Init(this,this.state);
        }
        else
        {
            CloseInfoWindow();
            CreateInfoWindow();
        }
    }

    public void CloseInfoWindow()
    {
        if (infoWindow != null)
        {
            Destroy(infoWindow.gameObject);
            infoWindow = null;
            isClicked = false;
        }
    }
}
