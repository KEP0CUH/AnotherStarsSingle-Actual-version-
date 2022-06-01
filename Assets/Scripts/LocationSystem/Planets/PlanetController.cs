using UnityEngine;

[RequireComponent(typeof(PlanetState))]
[RequireComponent(typeof(PlanetView))]

public class PlanetController : MonoBehaviour
{
    private PlanetState state;
    private PlanetView view;
    private LocationController locationController;
    private int offset;

    private ItemShopController itemShopController = null;


    public PlanetState State => state;
    public PlanetView View => view;
    public ItemShopController ItemShopController => itemShopController;

    public void Init(LocationController controller,Planet kind,int offset)
    {
        this.locationController = controller;
        this.state = this.GetComponent<PlanetState>().Init(this,kind);
        this.view = this.GetComponent<PlanetView>().Init(this);
        this.offset = offset;
    }

    public void CloseInfoWindow()
    {
        view.CloseInfoWindow();
    }

    public void OnLand()
    {
        
    }

    public void OnOpenItemShop()
    {
        if(itemShopController == null)
        {
            var obj = new GameObject("ItemShopController", typeof(ItemShopController));
            obj.GetComponent<Transform>().SetParent(this.gameObject.transform);
            itemShopController = obj.GetComponent<ItemShopController>().Init(state.Data.ItemShopType, state.Id);
        }
        else
        {
            itemShopController.SwitchItemShop();
        }
    }

    public void OnRise()
    {
        if(itemShopController != null)
        {
            itemShopController.CloseItemShop();
        }
    }

    private void Start()
    {
        this.gameObject.AddComponent<SphereCollider>();
        this.gameObject.transform.SetParent(locationController.transform, true);
        this.gameObject.transform.localPosition = new Vector3(Random.Range(2,offset + 2), Random.Range(2, offset + 2), 0);
     
        SetRandomPositionAroundSun();
    }

    private void FixedUpdate()
    {
        SaveRotationAboutSun();
    }

    private void SetRandomPositionAroundSun()
    {
        this.gameObject.transform.RotateAround(locationController.transform.position, locationController.transform.forward, Random.Range(0, 360));
    }

    private void SaveRotationAboutSun()
    {
        transform.RotateAround(locationController.transform.position, locationController.transform.forward, 4.0f * Time.fixedDeltaTime);
    }
}
