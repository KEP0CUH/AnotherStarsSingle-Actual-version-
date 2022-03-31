using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class PlanetController : MonoBehaviour
{
    private PlanetState state;
    [SerializeField] private LocationController controller;
    public PlanetState State => state;

    public void Init(LocationController controller)
    {
        this.controller = controller;
    }

    private void Start()
    {
        this.state = gameObject.AddComponent<PlanetState>();
        this.state.Init(Planet.Arcea);

        this.gameObject.name = this.state.Data.Title;
        this.GetComponent<SpriteRenderer>().sprite = state.Data.Icon;
    }

    private void FixedUpdate()
    {
        transform.RotateAround(controller.transform.position, new Vector3(0,0,1), 4.0f * Time.deltaTime);
    }


}
