using UnityEngine;

public class DeviceView : ItemView
{
    public override ItemView Init(ItemKind kind, int count)
    {
        var data = Managers.Resources.DownloadData(kind);
        this.state = this.gameObject.AddComponent<DeviceState>();
        this.state.Init(kind, count);
        this.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        this.GetComponent<SphereCollider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;

        return this;
    }
}
