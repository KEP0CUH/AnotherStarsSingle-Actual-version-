using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoAsteroidWindow : MonoBehaviour
{
    private AsteroidFieldState asteroidFieldState;
    private GameObject controller;

    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject buttonClose;
    [SerializeField] private GameObject buttonLand;



    public void Init(AsteroidFieldController controller)
    {
        this.controller = controller.gameObject;
        asteroidFieldState = controller.State;
        SettingIcon(asteroidFieldState.Data.Icon);
        SettingTitle(asteroidFieldState.Data.Title);
        SettingButtonClose();
        SettingButtonLand();
    }

    private void SettingIcon(Sprite content)
    {
        icon.GetComponent<Image>().sprite = content;
    }

    private void SettingTitle(string content)
    {
        var text = title.GetComponent<Text>().text = content;
    }

    private void SettingButtonClose()
    {
        var buttonComponent = buttonClose.GetComponent<Button>();
        buttonComponent.onClick.AddListener(Close);
    }

    private void SettingButtonLand()
    {
        var image = buttonLand.GetComponent<Image>();
        image.sprite = Managers.Resources.DownloadData(IconType.Land);

        var button = buttonLand.GetComponent<Button>();
        button.onClick.AddListener(OnLand);
    }

    private void OnLand()
    {
        if (controller.GetComponent<AsteroidFieldView>())
        {
            Managers.Player.LandOnAsteroidField(controller.transform, controller.GetComponent<AsteroidFieldView>().Quarter);

            var asteroidField = new GameObject("AsteroidFieldInside", typeof(RectTransform));
            asteroidField.GetComponent<RectTransform>().SetAsFirstSibling();
            asteroidField.AddComponent<AsteroidFieldWindow>().Init();

            Close();
        }
    }

    private void Close()
    {
        if (controller.GetComponent<AsteroidFieldView>())
        {
            controller.GetComponent<AsteroidFieldView>().CloseInfoWindow();
        }
    }
}
