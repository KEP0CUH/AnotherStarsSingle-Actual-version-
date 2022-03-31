using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(LocationState))]

public class LocationController : MonoBehaviour
{
    private LocationState state;
    private LocationController controller;

    public LocationState State => state;

    private void Start()
    {
        this.controller = GetComponent<LocationController>();

        this.state = this.gameObject.GetComponent<LocationState>();
        this.state.Init(controller,Location.Krinul);

        this.gameObject.name = this.state.Data.Title;
        this.GetComponent<SpriteRenderer>().sprite = this.state.Data.Icon;
    }
}
