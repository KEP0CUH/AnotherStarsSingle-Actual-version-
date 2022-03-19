using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class ItemViewGame : MonoBehaviour
{
    [SerializeField]
    private BaseScriptableItemData data;

    public BaseScriptableItemData Data => data;

    private bool triggerWorked = false;

    public void Init(BaseScriptableItemData data)
    {
        this.data = data;
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = data.Icon;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        // if other.tag == player && player.WantsToTake

        if (triggerWorked == false)
        {
            Managers.Inventory.AddItem(data);
            Destroy(this.gameObject);
        }
        triggerWorked = true;

    }
}
