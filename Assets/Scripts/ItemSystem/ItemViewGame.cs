using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class ItemViewGame : MonoBehaviour
{
    [SerializeField]
    private BaseItemData data;

    public BaseItemData Data => data;


    private void Start()
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = data.Icon;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        // if other.tag == player && player.WantsToTake
        Managers.Inventory.AddItem(data);
        Destroy(this.gameObject);
    }
}
