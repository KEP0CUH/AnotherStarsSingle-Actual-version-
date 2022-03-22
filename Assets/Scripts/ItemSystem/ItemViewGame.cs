using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class ItemViewGame : MonoBehaviour, Interactable
{
    [SerializeField]
    private BaseItemState state;
    private IInventory inventory;
    public BaseItemState State => state;

    private bool triggerWorked = false;

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
            if (other.GetComponent<PlayerController>())
            {
                inventory = other.GetComponent<PlayerController>().Inventory;
                OnPickup();
            }
        }
    }

    public void OnPickup()
    {
        Debug.Log($"Item {state.Data.Title} picked up.");
        inventory.AddItem(this.state.Data.ItemKind, 1);
        Destroy(this.gameObject);
        triggerWorked = true;
    }

    public void OnDrop()
    {
        
    }
}
