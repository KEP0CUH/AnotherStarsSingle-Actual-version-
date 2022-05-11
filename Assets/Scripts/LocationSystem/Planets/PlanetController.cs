using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlanetState))]
[RequireComponent(typeof(PlanetView))]

public class PlanetController : MonoBehaviour
{
    private PlanetState planetState;
    private PlanetView planetView;
    private LocationController controller;
    private int offset;
    public PlanetState State => planetState;

    public void Init(LocationController controller,Planet kind,int offset)
    {
        this.controller = controller;
        this.planetState = this.GetComponent<PlanetState>().Init(this,kind);
        this.planetView = this.GetComponent<PlanetView>().Init(this,planetState);
        this.offset = offset;
    }
    public void RemoveInfoWindow()
    {
        planetView.BreakInfoPlanetWindow();
    }

    private void Start()
    {
        this.gameObject.AddComponent<SphereCollider>();
        this.gameObject.transform.SetParent(controller.transform, true);
        this.gameObject.transform.localPosition = new Vector3(Random.Range(2,offset + 2), Random.Range(2, offset + 2), 0);
     
        SetRandomPositionAroundSun();
    }

    private void FixedUpdate()
    {
        SaveRotationAboutSun();
    }
    private void SetRandomPositionAroundSun()
    {
        this.gameObject.transform.RotateAround(controller.transform.position, controller.transform.forward, Random.Range(0, 360));
    }
    private void SaveRotationAboutSun()
    {
        transform.RotateAround(controller.transform.position, controller.transform.forward, 4.0f * Time.fixedDeltaTime);
    }
}
