using UnityEngine;
using UnityEngine.UI;

public class RadarUI : MonoBehaviour, IUIModule
{
    private GameObject radar;

    private GameObject leftPanel;
    private GameObject leftPanelList;

    public ManagerStatus Status { get; private set; }

    public UIModuleKind Kind { get; private set; }

    public void Disable()
    {
        radar.SetActive(false);
    }

    public void Enable()
    {
        radar.SetActive(true);
    }

    public void Startup(ICanvas canvas)
    {
        Debug.Log("RadarUI initializing...");
        Status = ManagerStatus.Initializing;
        Kind = UIModuleKind.Radar;


        CreateRadar(canvas);
        CreateLeftPanel();

        Debug.Log("RadarUI started.");
        Status = ManagerStatus.Started;
    }

    private void CreateRadar(ICanvas canvas)
    {
        radar = new GameObject("Radar", typeof(RectTransform));
        canvas.AddModule(radar);

        radar.AddComponent<RawImage>();
        var raw = radar.GetComponent<RawImage>();
        raw.raycastTarget = false;
        raw.texture = Resources.Load<Texture>("Textures/Radar");
        raw.color = new UnityEngine.Color(166f, 166f, 166f, 154f) / 256f;

        var rect = radar.GetComponent<RectTransform>();
        rect.anchorMax = new Vector2(1, 1);
        rect.anchorMin = new Vector2(1, 1);
        rect.pivot = new Vector2(1, 1);

        rect.offsetMax = new Vector2(-10, -10);
        rect.offsetMin = new Vector2(-260, -260);
    }

    private void CreateLeftPanel()
    {
        leftPanel = new GameObject("LeftRadarPanel", typeof(RectTransform));

        var rect = leftPanel.GetComponent<RectTransform>();
        rect.SetParent(radar.transform, false);


        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(1, 1);
        rect.offsetMin = new Vector2(-40, -250);
        rect.offsetMax = new Vector2(-20, 0);

        CreateLeftPanelList(rect);
    }

    private void CreateLeftPanelList(RectTransform rectParent)
    {
        leftPanelList = new GameObject("LeftPanelList", typeof(RectTransform), typeof(GridLayoutGroup));

        var rect = leftPanelList.GetComponent<RectTransform>();
        rect.SetParent(rectParent, false);

        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(1, 1);
        rect.offsetMin = new Vector2(0, -130);
        rect.offsetMax = new Vector2(65, 0);

        var layout = leftPanelList.GetComponent<GridLayoutGroup>();
        layout.cellSize = new Vector2(32, 32);
        layout.spacing = new Vector2(5, 5);
        layout.padding.top = 0;
        layout.padding.left = 0;

        CreateSettingsButton(rect);
        CreateOpenMapButton(rect);
    }

    private void CreateSettingsButton(RectTransform rectParent)
    {
        var buttonSettings = new GameObject("ButtonSettings", typeof(RectTransform), typeof(Image), typeof(LayoutElement), typeof(Button));

        var rect = buttonSettings.GetComponent<RectTransform>();
        rect.SetParent(rectParent, false);

        buttonSettings.GetComponent<Image>().sprite = Managers.Resources.DownloadData(IconType.ButtonSettings);
    }

    private void CreateOpenMapButton(RectTransform rectParent)
    {
        var buttonOpenMap = new GameObject("ButtonSettings", typeof(RectTransform), typeof(Image), typeof(LayoutElement), typeof(Button));

        var rect = buttonOpenMap.GetComponent<RectTransform>();
        rect.SetParent(rectParent, false);

        buttonOpenMap.GetComponent<Image>().sprite = Managers.Resources.DownloadData(IconType.ButtonOpenMap);

        var button = buttonOpenMap.GetComponent<Button>();
        button.onClick.AddListener(OpenMap);
    }

    private void OpenMap()
    {
        CanvasUI.GlobalMap.Enable();
    }


}
