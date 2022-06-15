using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    private PlayerController controller;

    private Transform target = null;
    private Transform landPlace;
    private bool isLanded;                      // Сидит ли на планете?
    public bool IsLanded => isLanded;
    public PlayerController Controller => controller;

    public ManagerStatus Status { get; private set; }

    public void Startup()
    {
        Debug.Log("Player manager starting...".SetColor(Color.Yellow));



        Status = ManagerStatus.Started;
        Debug.Log("Player manager started.".SetColor(Color.Green));
    }

    public void TryInteractWithEquipment(ItemState state)
    {
        this.Controller.State.ShipController.TryInteractWithItem(state);
    }

    public void Init(PlayerController controller)
    {
        this.controller = controller;
    }

    public void Attack(Transform target)
    {
        this.target = target;
        controller.Shoot(target);
    }

    public void Approach(Vector3 target)
    {
        this.Controller.MoveToApproach(new Vector2(target.x,target.y));
    }

    public void Land(Transform transform)
    {
        this.landPlace = transform;
        controller.gameObject.transform.position = new Vector3(transform.position.x + 450, transform.position.y + 450, 0);
        controller.UpdateCameraPosition();
        isLanded = true;
        //CanvasUI.Radar.Disable();
    }

    public void LandOnAsteroidField(Transform transform, Vector2 quarter)
    {
        this.landPlace = transform;
        controller.gameObject.transform.localPosition = new Vector3(transform.localPosition.x + 150 * quarter.x, transform.localPosition.y + 150 * quarter.y, 0);
        controller.UpdateCameraPosition();
        //isLanded = true;
    }

    public void Rise()
    {
        controller.gameObject.transform.position = new Vector2(this.landPlace.position.x, this.landPlace.position.y);
        controller.UpdateCameraPosition();
        isLanded = false;
        //CanvasUI.Radar.Enable();
    }
}
