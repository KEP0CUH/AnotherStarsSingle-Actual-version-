using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class ItemViewGame : MonoBehaviour, Interactable, IObservable
{
    [SerializeField]
    private BaseItemState state;
    private IInventory inventory;
    private List<IObserver> observers = new List<IObserver>();
    public BaseItemState State => state;

    private bool triggerWorked = false;

    private Action<ItemKind,BaseItemState> onItemDrop;
    private Action<ItemKind, int> inItemAddedInventory;

    public void Init(ItemKind kind, int count)
    {
        gameObject.AddComponent<BaseItemState>();
        state = gameObject.GetComponent<BaseItemState>();
        this.state.Init(kind,count);
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Init(GunKind kind, int ammo)
    {
        gameObject.AddComponent<GunState>();
        state = gameObject.GetComponent<GunState>();
        this.state.Init(kind, ammo);
        Debug.Log($"{this.state.Data.Title}".SetColor(Color.Magenta));
        this.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        this.GetComponent<BoxCollider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (triggerWorked == false)
        {
            if (other.GetComponent<PlayerController>() && Input.GetKey(KeyCode.Space) && (onItemDrop != null) )
            {
                inventory = other.GetComponent<PlayerController>().Inventory;
                triggerWorked = true;
                for(int i = 0; i < this.state.Count;i++)
                {
                    onItemDrop.Invoke(this.state.Data.ItemKind, state);
                }
            }
        }
    }

    public void OnDrop()
    {
        
    }

    public void AddObserver(IObserver observer,EventType eventType)
    {
        if (observers.Contains(observer))
        {
            return;
        }

        if(eventType == EventType.OnItemDrop)
        {
            onItemDrop += (kind, state) =>
            {
                observers.Add(observer);
                observer.Invoke(eventType,kind, state);
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
