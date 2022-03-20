using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class ItemViewGame : MonoBehaviour
{
    [SerializeField]
    private BaseItemState state;

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
                Managers.Inventory.AddItem(state.Data);
                Destroy(this.gameObject);
                triggerWorked = true;
            }
        }
    }
}
