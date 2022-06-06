using UnityEngine;

public class ItemController : MonoBehaviour
{
    private ItemState itemState;
    private ItemViewGame itemView;

    public ItemState State => itemState;
    public ItemViewGame View => itemView;

    public void Init(ItemKind kind, int count = 1)
    {
        CreateStateAndView(kind, count = 1);
    }

    public void Init(ItemState state)
    {
        CreateStateAndView(state.Data.ItemKind, state.Count);
    }

    private void CreateStateAndView(ItemKind kind, int count = 1)
    {
        var data = Managers.Resources.DownloadData(kind);

        if(data.IsWeapon())
        {
            this.itemState = this.gameObject.AddComponent<GunState>().Init(data,count);
        }
        else if(data.IsDevice())
        {
            this.itemState = this.gameObject.AddComponent<DeviceState>().Init(data, count);
        }
        else if(data.IsItem())
        {
            this.itemState = this.gameObject.AddComponent<ItemState>().Init(data, count);
        }

        CreateView();
    }

    private void CreateView()
    {
        if(State.Data.IsWeapon())
        {
            this.itemView = gameObject.AddComponent<GunViewGame>().Init(this);
        }
        else if(State.Data.IsDevice())
        {
            this.itemView = gameObject.AddComponent<DeviceViewGame>().Init(this);
        }
        else if(State.Data.IsItem())
        {
            this.itemView = gameObject.AddComponent<ItemViewGame>().Init(this);
        }
    }
}
