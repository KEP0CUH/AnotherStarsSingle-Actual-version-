using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]

public class LocationController : MonoBehaviour
{
    private LocationState state;

    public LocationState State => state;

    private void Start()
    {
        this.state = this.gameObject.AddComponent<LocationState>();
        this.state.Init(Location.Krinul);

        this.gameObject.name = this.state.Data.Title;
        this.GetComponent<SpriteRenderer>().sprite = this.state.Data.Icon;
    }
}
