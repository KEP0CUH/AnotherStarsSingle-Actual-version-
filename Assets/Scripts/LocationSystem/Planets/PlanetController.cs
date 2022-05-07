using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlanetState))]

public class PlanetController : MonoBehaviour
{
    private PlanetState state;
    private Planet planetKind;
    private LocationController controller;
    private int offset;

    private GameObject infoWindow = null;
    private bool isClicked = false;

    public PlanetState State => state;

    public void Init(LocationController controller,Planet kind,int offset)
    {
        this.controller = controller;
        this.planetKind = kind;
        this.offset = offset;
    }

    private void Start()
    {
        this.state = gameObject.GetComponent<PlanetState>();
        this.state.Init(planetKind);

        this.gameObject.name = this.state.Data.Title;
        this.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        this.gameObject.AddComponent<SphereCollider>();
        this.gameObject.transform.SetParent(controller.transform, true);
        this.gameObject.transform.localPosition = new Vector3(Random.Range(2,offset + 2), Random.Range(2, offset + 2), 0);
        this.gameObject.transform.RotateAround(controller.transform.position, controller.transform.forward, Random.Range(0,360));

    }

    private void FixedUpdate()
    {
        transform.RotateAround(controller.transform.position, controller.transform.forward, 4.0f * Time.fixedDeltaTime);
    }

    private void OnMouseEnter()
    {
        var data = this.gameObject.GetComponent<PlanetState>();
        Debug.Log($"Это объект: {data.Data.Title}");

        if (infoWindow == null)
        {
            infoWindow = new GameObject("InfoWindow");
            infoWindow.AddComponent<InfoPlanetWindow>().Init(this.gameObject.GetComponent<PlanetController>());
        }
    }

    private void OnMouseDown()
    {
        isClicked = true;
    }

    private void OnMouseExit()
    {
        if (infoWindow != null && !isClicked)
        {
            RemoveInfoWindow();
        }
    }

    public void RemoveInfoWindow()
    {
        if (infoWindow != null)
        {
            Destroy(infoWindow.gameObject);
            infoWindow = null;
            isClicked = false;
        }
    }
}
