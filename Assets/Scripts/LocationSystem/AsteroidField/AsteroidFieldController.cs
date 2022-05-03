using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AsteroidFieldView))]
public class AsteroidFieldController : MonoBehaviour
{
    private AsteroidFieldState state;
    private LocationController locationController;
    private AsteroidFieldType type;
    private int offset;



    public AsteroidFieldView view;

    public void Init(LocationController controller,AsteroidFieldType type,int offset)
    {
        this.locationController = controller;
        this.offset = offset;
        this.type = type;

        this.view = gameObject.GetComponent<AsteroidFieldView>();
        this.view.Init(this.transform, type);

        
        this.gameObject.AddComponent<SphereCollider>();
        this.gameObject.transform.parent = locationController.gameObject.transform;
        this.gameObject.transform.position = new Vector3(offset, offset, 0) + new Vector3(locationController.transform.position.x, locationController.transform.position.y, 0);
    }

    private void Start()
    {
        
        //this.gameObject.transform.RotateAround(locationController.transform.position, new Vector3(0, 0, 1), Random.Range(0, 360));
    }
}
