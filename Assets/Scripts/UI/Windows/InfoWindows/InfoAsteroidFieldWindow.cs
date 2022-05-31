using UnityEngine;
using UnityEngine.UI;

public class InfoAsteroidFieldWindow : MonoBehaviour
{
    private AsteroidFieldState fieldState;
    private AsteroidFieldController fieldController;

    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject buttonClose;
    [SerializeField] private GameObject buttonLand;
    [SerializeField] private GameObject description;

    public InfoAsteroidFieldWindow Init(AsteroidFieldController controller)
    {
        this.fieldController = controller;
        this.fieldState = controller.State;


        icon.GetComponent<Image>().sprite = fieldState.Data.Icon;
        title.GetComponent<Text>().text = fieldState.Data.Title;
        InitButtonLand();
        InitButtonClose();


        return this;
    }

    private void InitButtonLand()
    {
        var image = buttonLand.GetComponent<Image>();
        image.sprite = Managers.Resources.DownloadData(IconType.Land);

        var button = buttonLand.GetComponent<Button>();
        button.onClick.AddListener(Land);
    }

    private void InitButtonClose()
    {
        var buttonComponent = buttonClose.GetComponent<Button>();
        buttonComponent.onClick.AddListener(CloseInfoWindow);
    }

    private void Land()
    {
        if (fieldController.GetComponent<AsteroidFieldController>())
        {
            Managers.Player.LandOnAsteroidField(fieldController.transform, fieldController.GetComponent<AsteroidFieldView>().Quarter);

            var asteroidField = new GameObject("AsteroidFieldInside", typeof(RectTransform));
            asteroidField.GetComponent<RectTransform>().SetAsFirstSibling();
            asteroidField.AddComponent<AsteroidFieldWindow>().Init();

            CloseInfoWindow();
        }
    }

    private void CloseInfoWindow()
    {
        fieldController.CloseInfoWindow();
    }


}
