using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class GunViewGame : MonoBehaviour
{
    private GunState state;
    private IInventory inventory;

    public void Init(GunData data,int ammoMax)
    {
        this.state = this.gameObject.AddComponent<GunState>().Init(data,ammoMax);

        this.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        this.GetComponent<BoxCollider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
    }
}