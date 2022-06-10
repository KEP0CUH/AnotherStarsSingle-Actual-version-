using UnityEngine;
using UnityEngine.UI;

public class ItemWindow : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text title;
    [SerializeField] private Text size;
    [SerializeField] private Text type;
    [SerializeField] private Button buttonTake;

    private ItemViewGame view;

    public void Init(ItemViewGame view,ItemState state)
    {
        this.icon.sprite = state.Data.Icon;
        this.title.text = state.Data.Title;
        this.view = view;

        buttonTake.onClick.AddListener(TakeItem);
    }

    private void TakeItem()
    {
        this.view.NeedTake();
    }
}
