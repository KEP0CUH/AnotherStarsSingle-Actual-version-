using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalMapUI : MonoBehaviour, IUIModule
{
    private GameObject globalMap;

    public ManagerStatus Status { get; private set; }
    public UIModuleKind Kind { get;private set; }

    public void Disable()
    {
        globalMap.SetActive(false);
    }

    public void Enable()
    {
        globalMap.SetActive(true);
    }

    public void Startup(ICanvas canvas)
    {
        Debug.Log("GlobalMap initializing...");
        Status = ManagerStatus.Initializing;
        Kind = UIModuleKind.GlobalMap;


        CreateGlobalMap(canvas);

        Debug.Log("RadarUI started.");
        Status = ManagerStatus.Started;
    }

    public void AddLocationOnMap(LocationState location)
    {
        var newLocation = new GameObject($"{location.Data.Title}", typeof(RectTransform), typeof(Image), typeof(Button));

        var rect = newLocation.GetComponent<RectTransform>();
        rect.SetParent(globalMap.transform);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);

        var locGlobalPos = new Vector2(location.transform.position.x,location.transform.position.y);

        rect.offsetMin = new Vector2(-8,-8) + locGlobalPos;
        rect.offsetMax = new Vector2(8, 8) + locGlobalPos;


        var image = newLocation.GetComponent<Image>();
        image.sprite = location.Data.Icon;




    }

    private void CreateGlobalMap(ICanvas canvas)
    {

        globalMap = new GameObject("GlobalMap", typeof(RectTransform));
        canvas.AddModule(globalMap);

        globalMap.AddComponent<RawImage>();
        var raw = globalMap.GetComponent<RawImage>();
        raw.raycastTarget = false;
        raw.texture = Resources.Load<Texture>("Textures/GlobalMap");
        raw.color = new UnityEngine.Color(166f, 166f, 166f, 154f) / 256f;

        var rect = globalMap.GetComponent<RectTransform>();
        /*        rect.anchorMax = new Vector2(1, 1);
                rect.anchorMin = new Vector2(1, 1);
                rect.pivot = new Vector2(1, 1);

                rect.offsetMax = new Vector2(-10, -10);
                rect.offsetMin = new Vector2(-260, -260);*/

        rect.pivot = new Vector2(1, 1);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f,0.5f);
        rect.offsetMin = new Vector2(-200, -200);
        rect.offsetMax = new Vector2(200, 200);
    }
}
