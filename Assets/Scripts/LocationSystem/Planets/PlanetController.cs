///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

[RequireComponent(typeof(PlanetState))]
[RequireComponent(typeof(PlanetView))]

public class PlanetController : MonoBehaviour
{
    private             PlanetState             state;
    private             PlanetView              view;
    private             Transform               parent;
    private             int                     orbitNumber;

    private             ItemShopController      itemShopController      = null;

    public              PlanetState             State => state;
    public              PlanetView              View => view;
    public              ItemShopController      ItemShopController => itemShopController;

    public              void                    Init(Transform parent,Planet kind,int orbitNumber)
    {
        this.parent = parent;
        this.state = this.GetComponent<PlanetState>().Init(this,kind);
        this.view = this.GetComponent<PlanetView>().Init(this);
        this.orbitNumber = orbitNumber;
    }

    public              void                    CloseInfoWindow()
    {
        view.CloseInfoWindow();
    }

    public              void                    OnOpenItemShop()
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

    public              void                    OnRise()
    {
        if(itemShopController != null)
        {
            itemShopController.CloseItemShop();
        }
    }

    private             void                    Start()
    {
        this.gameObject.AddComponent<SphereCollider>();
        this.gameObject.transform.SetParent(parent, true);
        this.gameObject.transform.localPosition = CalculateOrbitalPosition(orbitNumber);
     
        SetRandomPositionAroundSun();
    }

    private             void                    FixedUpdate()
    {
        SaveRotationAboutSun();
    }

    private             void                    SetRandomPositionAroundSun()
    {
        this.gameObject.transform.RotateAround(parent.position, parent.forward, Random.Range(0, 360));
    }

    private             void                    SaveRotationAboutSun()
    {
        transform.RotateAround(parent.position, parent.forward, 4.0f * Time.fixedDeltaTime);
    }

    private             Vector3                 CalculateOrbitalPosition(int    orbitNumber)
    {
        Vector3 startPosition = new Vector3(0,0,0);

        int temp        = Random.Range(-1,1);
        int temp1       = -2 - orbitNumber * 2;         // Генерация позиций относительно Солнца, которая
        int temp2       = +2 + orbitNumber * 2;         // отстоит как минимум на 2 клетки от солнца по х и у координатам
        
        if(temp >= 0)
            startPosition.x   = temp1;
        else startPosition.x  = temp2;

        temp            = Random.Range(-1,1);
        temp1           = -2 - orbitNumber * 2;
        temp2           = +2 + orbitNumber * 2;
        if (temp >= 0)
            startPosition.y   = temp1;
        else startPosition.y  = temp2;

        return startPosition;
    }
}
