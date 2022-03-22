using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private List<Bullet> bullets = new List<Bullet>();

    private PlayerState state;
    private PlayerInventory inventory;

    private float timer = 0;
    private float shootDelay = 0.5f;
    private int numOfFires = 5;
    private int firesMade = 0;

    [SerializeField]
    private float moveSpeed = 1 / Constants.TICKS_PER_SEC;
    private Camera mainCamera;
    private Camera radarCamera;

    public PlayerState State => state;
    public PlayerInventory Inventory => inventory;

    private void Start()
    {
        this.gameObject.AddComponent<PlayerData>().Init();
        state = new PlayerState(this.gameObject.GetComponent<PlayerData>());
        inventory = new PlayerInventory();

        SetupCamera();
        SetupRadar();
    }

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Vector2 clickPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            MoveToClick(clickPosition, transform.position);
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            timer += Time.fixedDeltaTime;
            if(timer > shootDelay)
            {
                if(numOfFires > firesMade)
                {
                    Shoot();
                    firesMade++;
                    timer -= 12 * Time.fixedDeltaTime;
                }
                if(numOfFires <= firesMade)
                {
                    timer = 0;
                    firesMade = 0;
                }

            }
        }

        if(Input.GetKey(KeyCode.I))
        {
            CanvasUI.Inventory.Enable();
        }

        if(Input.GetKey(KeyCode.M))
        {
            CanvasUI.Radar.Enable();
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Managers.Canvas.DisableAllModules();
        }

        //MoveBullets();
    }

    // Передвигаем клиента к координатам, на которые он кликнул мышью
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

    /// <summary>
    /// Создаёт главную камеру игрока и настраивает её на работу.
    /// </summary>
    private void SetupCamera()
    {
        GameObject camera = new GameObject("MainCamera",typeof(Camera),typeof(AudioListener));

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

    private void UpdateCameraPosition()
    {
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        radarCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -40);
    }
    
    private void Shoot()
    {
        Debug.Log("Стреляю");
        GameObject bullet = new GameObject("Bullet");
        bullet.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
        bullet.transform.localEulerAngles = new Vector3(0, 0, this.transform.localEulerAngles.z);
        bullet.AddComponent<Bullet>();
        Destroy(bullet, 10);
    }

    public void OnItemDrop()
    {

    }

    public void OnItemPickup()
    {

    }
}
