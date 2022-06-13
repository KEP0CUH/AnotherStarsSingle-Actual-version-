using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController playerController;

    private Vector2 clickPos;
    private float moveSpeed;
    private bool movementEnd;
    private GameObject endPoint = null;

    public PlayerMovement Init(PlayerController controller)
    {
        this.playerController = controller;
        movementEnd = true;
        this.moveSpeed = playerController.ShipController.State.Data.Speed
            * Constants.SPEED_KOEFFICIENT;

        return this;
    }

    public void Move(Vector2 targetPos)
    {
        this.clickPos = targetPos;
        CreateEndPoint();
        movementEnd = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            clickPos = playerController.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            CreateEndPoint();
            movementEnd = false;
        }

        if(movementEnd == false)
        {
            MoveToClick();
            if (endPoint != null)
                endPoint.transform.Rotate(new Vector3(0, 0, -180 * Time.fixedDeltaTime));
        }
    }

    private void MoveToClick()
    {
        Vector2 currentPosition = this.gameObject.transform.position;

        if (clickPos.x != currentPosition.x && clickPos.y != currentPosition.y)
        {
            // ¬ычисл€ем вектор с направлением от текущего положени€ к клику, но с длиной единица - нормализуем.
            Vector2 _difference = (clickPos - new Vector2(currentPosition.x, currentPosition.y)).normalized;

            // ѕоворачиваем игрока. ¬ычисление угла через тангенс.
            float angle = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);


            // ≈сли текущие координаты x и y игрока сильно отличаютс€ от целевых,то движение продолжаетс€
            if (Math.Abs(clickPos.x - currentPosition.x) >= 0.2f || Math.Abs(clickPos.y - currentPosition.y) >= 0.2f)
            {
                // „тобы не было ошибки при делении на нуль,если клик осуществл€етс€ в текущие координаты. ¬озможно даже не нужна
                if (_difference.magnitude != 0)
                {
                    transform.position += new Vector3(_difference.x * moveSpeed, _difference.y * moveSpeed, 0);
                }
            }
            else
            {
                movementEnd = true;
                Destroy(endPoint);
            }
            playerController.UpdateCameraPosition();
        }
    }

    private void CreateEndPoint()
    {
        if(endPoint != null)
        {
            Destroy(endPoint.gameObject);
        }

        endPoint = new GameObject("EndPoint");
        endPoint.transform.position = new Vector3(clickPos.x, clickPos.y, 0);
        endPoint.AddComponent<SpriteRenderer>().sprite = Managers.Resources.DownloadData(IconType.EndPoint);
    }
}
