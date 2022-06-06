using System;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class GunViewGame : ItemViewGame
{
    public override ItemViewGame Init(ItemKind kind, int ammoMax)
    {
        var data = Managers.Resources.DownloadData(kind);
        this.state = this.gameObject.AddComponent<GunState>();
        this.state.Init(kind, ammoMax);
        this.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        this.GetComponent<BoxCollider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;

        return this;
    }
}
