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
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(0, 0);
        rect.pivot = new Vector2(0, 0);

        var locGlobalPos = new Vector2(location.transform.position.x,location.transform.position.y) / 5;

        rect.offsetMin = new Vector2(-8,-8) + locGlobalPos;
        rect.offsetMax = new Vector2(8, 8) + locGlobalPos;


        var image = newLocation.GetComponent<Image>();
        image.sprite = Managers.Resources.DownloadData(location.Data.SunType);


        var button = newLocation.GetComponent<Button>();
        button.onClick.AddListener(()=> TeleportPlayer(location.transform.position));

    }

    private void CreateGlobalMap(ICanvas canvas)
    {

        globalMap = new GameObject("GlobalMap", typeof(RectTransform));
        canvas.AddModule(globalMap);

        globalMap.AddComponent<RawImage>();
        var raw = globalMap.GetComponent<RawImage>();
        raw.raycastTarget = false;
        raw.texture = Resources.Load<Texture>("Textures/GlobalMap");
        raw.color = new UnityEngine.Color(166f, 166f, 166f, 255) / 256f;

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

        CreateButtonClose(rect);
    }

    private void CreateButtonClose(RectTransform rectParent)
    {
        var buttonClose = new GameObject("CloseGlobalMap", typeof(RectTransform), typeof(Image),typeof(Button));

        var rect = buttonClose.GetComponent<RectTransform>();
        rect.SetParent(rectParent, false);

        rect.anchorMin = new Vector2(1, 1);
        rect.anchorMax = new Vector2(1,1);
        rect.pivot = new Vector2(1, 1);
        rect.offsetMin = new Vector2(-60, -60);
        rect.offsetMax= new Vector2(-10, -10);

        buttonClose.GetComponent<Image>().sprite = Managers.Resources.DownloadData(IconType.CloseWindow);

        var button = buttonClose.GetComponent<Button>();
        button.onClick.AddListener(CloseGlobalMap);
    }

    private void CloseGlobalMap()
    {
        CanvasUI.GlobalMap.Disable();
    }

    private void TeleportPlayer(Vector3 targetPos)
    {
        Managers.Player.Controller.gameObject.transform.position = targetPos;
        Managers.Player.Controller.UpdateCameraPosition();
    }
}
