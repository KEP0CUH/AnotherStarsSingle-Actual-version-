using System;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class GunViewGame : ItemViewGame
{
    public GunState State => (GunState)this.state;


    public override void Init(ItemKind kind, int ammoMax)
    {
        var data = Managers.Resources.DownloadData(kind);
        this.state = this.gameObject.AddComponent<GunState>();
        this.state.Init(kind, ammoMax);
        this.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        this.GetComponent<BoxCollider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
    }
}
