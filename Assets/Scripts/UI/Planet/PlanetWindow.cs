using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class PlanetWindow : MonoBehaviour
{
    private PlanetState planetState;
    public PlanetState PlanetState => planetState;

    public void Init(PlanetState state)
    {
        this.planetState = state;
        Managers.Canvas.AddModule(this.gameObject);

        var rect = this.gameObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(1, 1);
        rect.pivot = new Vector2(0, 0);
        rect.offsetMin = new Vector2(0, 0);
        rect.offsetMax = new Vector2(0, 0);

        CreateRiseButton();
    }
    private void Start()
    {
        this.GetComponent<Image>().sprite = planetState.Data.IconBG;
    }




    private void CreateRiseButton()
    {
            var buttonLand = new GameObject("Land", typeof(Image), typeof(Button));
            var rect = buttonLand.GetComponent<RectTransform>();
            rect.SetParent(this.gameObject.transform, false);
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 0);
            rect.pivot = new Vector2(1, 0);
            rect.offsetMin = new Vector2(5, 5);
            rect.offsetMax = new Vector2(45, 45);

            var image = buttonLand.GetComponent<Image>();
            image.sprite = Managers.Resources.DownloadData(IconType.Rise);

            var button = buttonLand.GetComponent<Button>();
            button.onClick.AddListener(OnRise);
    }


    private void OnRise()
    {
        Managers.Player.Rise();
        Close();
    }

    private void Close()
    {
        Destroy(this.gameObject);
    }
}
