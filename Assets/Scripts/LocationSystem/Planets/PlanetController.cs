using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlanetState))]

public class PlanetController : MonoBehaviour
{
    private PlanetState state;
    private LocationController controller;
    private int offset;

    private GameObject infoWindow = null;
    private bool isClicked = false;

    public PlanetState State => state;

    public void Init(LocationController controller,int offset)
    {
        this.controller = controller;
        this.offset = offset;
    }

    private void Start()
    {
        this.state = gameObject.GetComponent<PlanetState>();
        this.state.Init(Planet.Arcea);

        this.gameObject.name = this.state.Data.Title;
        this.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
        this.gameObject.AddComponent<SphereCollider>();
        this.gameObject.transform.parent = controller.gameObject.transform;
        this.gameObject.transform.position = new Vector3(Random.Range(2,offset + 2), Random.Range(2, offset + 2), 0);
        this.gameObject.transform.RotateAround(controller.transform.position, new Vector3(0, 0, 1), Random.Range(0,360));

    }

    private void FixedUpdate()
    {
        transform.RotateAround(controller.transform.position, new Vector3(0,0,1), 4.0f * Time.deltaTime);
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

    private void OnMouseExit()
    {
        //RemoveInfoWindow();
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
