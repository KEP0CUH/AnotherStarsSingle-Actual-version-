using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AsteroidFieldState))]
[RequireComponent(typeof(AsteroidFieldView))]
public class AsteroidFieldController : MonoBehaviour
{
    private AsteroidFieldState fieldState;
    private AsteroidFieldView fieldView;

    private LocationController locationController;


    public AsteroidFieldState State => fieldState;
    public AsteroidFieldView View => fieldView;


    public void Init(LocationController controller,AsteroidFieldType type,int offset,Vector2 quarter)
    {
        this.locationController = controller;

        this.fieldState = gameObject.GetComponent<AsteroidFieldState>().Init(type);
        this.fieldView = gameObject.GetComponent<AsteroidFieldView>().Init(this,this.transform, type, quarter); ;

        
        this.gameObject.AddComponent<SphereCollider>();
        this.gameObject.transform.parent = locationController.gameObject.transform;
        this.gameObject.transform.position = new Vector3(offset * quarter.x, offset * quarter.y, 0) + new Vector3(locationController.transform.position.x, locationController.transform.position.y, 0);
    }
}
