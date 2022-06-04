using UnityEngine;
using UnityEngine.UI;

public class FieldWindow : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text type;
    [SerializeField] private Button buttonClose;
    [SerializeField] private Button buttonLand;

    AsteroidFieldController controller;
    public void Init(AsteroidFieldController controller)
    {
        this.controller = controller;

        this.icon.sprite = controller.State.Data.Icon;
        this.type.text = $"<color=grey>“ËÔ</color>            " + controller.State.Data.Title;

        this.buttonLand.onClick.AddListener(Land);
        this.buttonClose.onClick.AddListener(CloseWindow);
    }

    private void Land()
    {
        if(this.controller != null)
        {
            Managers.Player.LandOnAsteroidField(this.controller.transform, this.controller.View.Quarter);

            var asteroidField = new GameObject("AsteroidFieldInside", typeof(RectTransform));
            asteroidField.GetComponent<RectTransform>().SetAsFirstSibling();
            asteroidField.AddComponent<AsteroidFieldWindow>().Init();

            this.controller.CloseInfoWindow();
        }
    }

    private void CloseWindow()
    {
        this.controller.CloseInfoWindow();
    }
}
