using UnityEngine;
using UnityEngine.UI;

public class RadarUI : MonoBehaviour, IUIModule
{
    private GameObject radar;

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
        Status = ManagerStatus.Initializing;
        Kind = UIModuleKind.Radar;
        Debug.Log("RadarUI initializing...");

        radar = new GameObject("Radar",typeof(RectTransform));
        canvas.AddModule(radar);

        radar.AddComponent<RawImage>();
        var raw = radar.GetComponent<RawImage>();
        raw.texture = Resources.Load<Texture>("Textures/Radar");
        raw.color = new UnityEngine.Color(166f,166f,166f,154f) / 256f;

        var rect = radar.GetComponent<RectTransform>();
        rect.anchorMax = new Vector2(1, 1);
        rect.anchorMin = new Vector2(1, 1);
        rect.pivot = new Vector2(1, 1);

        rect.offsetMax = new Vector2(-10, -10);
        rect.offsetMin = new Vector2(-260, -260);

        //CreateRadar();

        Debug.Log("RadarUI started.");
        Status = ManagerStatus.Started;
    }

    private void CreateRadar()
    {

    }
}
