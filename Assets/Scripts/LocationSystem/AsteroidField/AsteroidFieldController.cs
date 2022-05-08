using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AsteroidFieldView))]
public class AsteroidFieldController : MonoBehaviour
{
    private LocationController locationController;

    public AsteroidFieldView view;

    public void Init(LocationController controller,AsteroidFieldType type,int offset,Vector2 quarter)
    {
        this.locationController = controller;

        this.view = gameObject.GetComponent<AsteroidFieldView>();
        this.view.Init(this.transform, type,quarter);

        
        this.gameObject.AddComponent<SphereCollider>();
        this.gameObject.transform.parent = locationController.gameObject.transform;
        this.gameObject.transform.position = new Vector3(offset * quarter.x, offset * quarter.y, 0) + new Vector3(locationController.transform.position.x, locationController.transform.position.y, 0);
    }

    private void Start()
    {
        
        //this.gameObject.transform.RotateAround(locationController.transform.position, new Vector3(0, 0, 1), Random.Range(0, 360));
    }
}
