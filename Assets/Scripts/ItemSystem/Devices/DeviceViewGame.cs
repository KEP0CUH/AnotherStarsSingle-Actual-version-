using System;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class DeviceViewGame : ItemViewGame
{
    private GunState state;
    private IInventory inventory;

    private List<IObserver> observers = new List<IObserver>();

    public ItemState State => state;

    private bool triggerWorked = false;

    private Action<ItemKind, ItemState> onItemDrop;
    public override void Init(ItemKind kind, int ammoMax)
    {
        var data = Managers.Resources.DownloadData(kind);
        if (data.IsWeapon())
        {
            this.state = this.gameObject.AddComponent<GunState>();
            //this.state = this.state.Init(kind,ammoMax);
            Debug.Log($"{this.state.Data.Title}".SetColor(Color.Magenta));
            this.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
            this.GetComponent<BoxCollider>().isTrigger = true;
            this.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerWorked == false)
        {
            if (other.GetComponent<PlayerController>() && Input.GetKey(KeyCode.Space) && (onItemDrop != null))
            {
                inventory = other.GetComponent<PlayerController>().Inventory;
                triggerWorked = true;
                for (int i = 0; i < this.state.Count; i++)
                {
                    onItemDrop.Invoke(this.state.Data.ItemKind, state);
                }
            }
        }
    }

    public void OnDrop()
    {

    }

    public void AddObserver(IObserver observer, EventType eventType)
    {
        if (observers.Contains(observer))
        {
            return;
        }

        if (eventType == EventType.OnItemDrop)
        {
            onItemDrop += (kind, state) =>
            {
                observers.Add(observer);
                observer.Invoke(eventType, kind, state);
                Destroy(this.gameObject);
            };
        }
    }

    public void RemoveObserver(IObserver observer)
    {
        //
    }

    public void OnPickup()
    {
        throw new NotImplementedException();
    }
}
