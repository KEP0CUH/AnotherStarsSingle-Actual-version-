using UnityEngine;
using UnityEngine.UI;

public class PlanetWindow : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text title;
    [SerializeField] private Text organization;
    [SerializeField] private Text size;
    [SerializeField] private Button buttonLand;

    private PlanetController controller;
    public void Init(PlanetController controller)
    {
        this.controller = controller;

        this.icon.sprite = controller.State.Data.Icon;
        this.title.text = $"<color=grey>Название</color><color=green>    {controller.State.Data.Title}</color>";
        //this.size.text = controller.State.Data.Size;

        this.buttonLand.onClick.AddListener(Land);
    }

    private void Land()
    {
        Managers.Player.Land(controller.transform);

        var planetInside = new GameObject("PlanetInside", typeof(RectTransform));
        planetInside.AddComponent<PlanetInside>().Init(controller);

        controller.CloseInfoWindow();
    }
}
