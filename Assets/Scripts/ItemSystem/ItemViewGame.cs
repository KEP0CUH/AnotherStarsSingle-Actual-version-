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

    private Action<ItemKind,int> onItemDrop;

    public void Init(BaseItemData data, int count)
    {
        gameObject.AddComponent<BaseItemState>();
        state = gameObject.GetComponent<BaseItemState>();
        this.state.Init(data,count);
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = data.Icon;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (triggerWorked == false)
        {
            if (other.GetComponent<PlayerController>() && Input.GetKey(KeyCode.Space) && (onItemDrop != null) )
            {
                inventory = other.GetComponent<PlayerController>().Inventory;
                triggerWorked = true;
                onItemDrop.Invoke(this.state.Data.ItemKind, 1);
            }
        }
    }



    public void OnDrop()
    {
        
    }

    public void AddObserver(IObserver observer)
    {
        if (observers.Contains(observer))
        {
            return;
        }
        onItemDrop += (kind, count) =>
        {
            observers.Add(observer);
            observer.Invoke(kind, count);
            Destroy(this.gameObject);
        };

    }

    public void RemoveObserver(IObserver observer)
    {
        //
    }
    public void NotifyObservers()
    {
        Debug.Log($"Notification worked.");

        foreach (var o in observers)
        {
            //o.Invoke(this.state.Data.ItemKind, 1);
        }
    }

    public void OnPickup()
    {
        throw new NotImplementedException();
    }
}
