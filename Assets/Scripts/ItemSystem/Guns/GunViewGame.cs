using UnityEngine;

public class GunViewGame : ItemViewGame
{
    public override ItemViewGame Init(ItemKind kind, int ammoMax)
    {
        var data = Managers.Resources.DownloadData(kind);
        this.state = this.gameObject.AddComponent<GunState>();
        this.state.Init(kind, ammoMax);
        this.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        this.GetComponent<SphereCollider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;

        return this;
    }
}
