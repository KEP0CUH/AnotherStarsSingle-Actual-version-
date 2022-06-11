using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    private List<GunState> guns = new List<GunState>();

    private PlayerState playerState;
    private InventoryController inventoryController;
    private InventoryInside inventoryInside;
    private ShipController shipController;

    private float timer = 0;
    private float shootDelay = 0.5f;
    private int numOfFires = 5;
    private int firesMade = 0;

    [SerializeField]
    private float moveSpeed = 1 / Constants.TICKS_PER_SEC;
    private Camera mainCamera;
    private Camera radarCamera;
    private Camera globalMapCamera;

    public PlayerState State => playerState;
    public InventoryController Inventory => inventoryController;
    public ShipController ShipController => shipController;

    public PlayerController Init()
    {
        this.inventoryController = new InventoryController(this.transform);
        this.shipController = this.gameObject.AddComponent<ShipController>().Init(ShipKind.GreenLinkor,this.inventoryController);
        this.playerState = new PlayerState(this,shipController);

        this.inventoryInside = Instantiate(Managers.Resources.DownloadData(ObjectType.InventoryInside))
                                        .GetComponent<InventoryInside>().Init(this);
        
        Managers.Canvas.AddModule(this.inventoryInside.gameObject);
        Managers.Player.Init(this);

        SetupCamera();
        SetupRadar();
        SetupGlobalMap();
        UpdateState();

        return this;
    }
    public void UpdateState()
    {
        if (this.playerState != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = playerState.ShipController.State.Data.Icon;
        }
    }
    public void UpdateCameraPosition()
    {
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        radarCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -40);
        globalMapCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -40);
    }

    public void ShowInventory()
    {
        this.inventoryInside.ShowInventory();
    }

    #region MOVEMENT_FOR_CLICK
    /// <summary>
    /// Осуществление движения игрока к месту клика мышкой.
    /// </summary>
    /// <param name="clickPosition">Место клика мышкой.</param>
    /// <param name="currentPosition">Текущая позиция игрока.</param>
    private void MoveToClick(Vector2 clickPosition, Vector3 currentPosition)
    {
        if (clickPosition.x != currentPosition.x && clickPosition.y != currentPosition.y)
        {
            // Вычисляем вектор с направлением от текущего положения к клику, но с длиной единица - нормализуем.
            Vector2 _difference = (clickPosition - new Vector2(currentPosition.x, currentPosition.y)).normalized;

            // Поворачиваем игрока. Вычисление угла через тангенс.
            float angle = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);


            // Если текущие координаты x и y игрока сильно отличаются от целевых,то движение продолжается
            if (Math.Abs(clickPosition.x - currentPosition.x) >= 0.1f || Math.Abs(clickPosition.y - currentPosition.y) >= 0.1f)
            {
                // Чтобы не было ошибки при делении на нуль,если клик осуществляется в текущие координаты. Возможно даже не нужна
                if (_difference.magnitude != 0)
                {
                    transform.position += new Vector3(_difference.x * moveSpeed, _difference.y * moveSpeed, 0);
                }
            }
            UpdateCameraPosition();
        }
    }
    #endregion

    /// <summary>
    /// Создаёт главную камеру игрока и настраивает её на работу.
    /// </summary>
    private void SetupCamera()
    {
        GameObject camera = new GameObject("MainCamera", typeof(Camera), typeof(AudioListener));

        mainCamera = camera.GetComponent<Camera>();
        mainCamera.backgroundColor = UnityEngine.Color.gray;
        mainCamera.orthographic = true;
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        mainCamera = camera.GetComponent<Camera>();
    }

    private void SetupRadar()
    {
        GameObject radarCam = new GameObject("RadarCamera");
        radarCam.AddComponent<Camera>();

        var cam = radarCam.GetComponent<Camera>();
        cam.orthographicSize = 20;
        cam.backgroundColor = UnityEngine.Color.grey;
        cam.orthographic = true;
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        cam.targetTexture = Resources.Load<RenderTexture>("Textures/Radar");
        radarCamera = cam;
    }

    private void SetupGlobalMap()
    {
        GameObject globalMapCameraObj = new GameObject("GlobalMap");
        globalMapCameraObj.AddComponent<Camera>();

        var cam = globalMapCameraObj.GetComponent<Camera>();
        cam.cullingMask = 6;
        cam.orthographicSize = 20;
        cam.backgroundColor = UnityEngine.Color.grey;
        cam.orthographic = true;
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        cam.targetTexture = Resources.Load<RenderTexture>("Textures/GlobalMap");
        globalMapCamera = cam;
    }



    private void Shoot()
    {
        this.guns = this.playerState.ShipController.State.Inventory.GetGuns();
        foreach (var gun in this.guns)
        {
            gun.Shoot(this.gameObject.transform, gun);
        }
    }

    private void Start()
    {
        this.Init();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryInside.Reswitch();
        }
    }

    /// <summary>
    /// Обработка ввода.
    /// </summary>
    private void FixedUpdate()
    {

        if (Managers.Player.IsLanded == false)
        {

            if (Input.GetKey(KeyCode.Mouse1))
            {
                Vector2 clickPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                MoveToClick(clickPosition, transform.position);
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                timer += Time.fixedDeltaTime;
                if (timer > shootDelay)
                {
                    if (numOfFires > firesMade)
                    {
                        Shoot();
                        firesMade++;
                        timer -= 12 * Time.fixedDeltaTime;
                    }
                    if (numOfFires <= firesMade)
                    {
                        timer = 0;
                        firesMade = 0;
                    }
                }
            }

            if (Input.GetKey(KeyCode.M))
            {
                CanvasUI.Radar.Enable();
            }

            if(Input.GetKey(KeyCode.L))
            {
                CanvasUI.GlobalMap.Enable();
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Managers.Canvas.DisableAllModules();
        }
    }


   
}
