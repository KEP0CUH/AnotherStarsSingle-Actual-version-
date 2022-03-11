using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1 / Constants.TICKS_PER_SEC;
    private Camera mainCamera;

    private void Start()
    {
        SetupCamera();
    }

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Vector2 clickPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            MoveToClick(clickPosition, transform.position);
        }

        //MoveBullets();
    }

    // ����������� ������� � �����������, �� ������� �� ������� �����
    /// <summary>
    /// ������������� �������� ������ � ����� ����� ������.
    /// </summary>
    /// <param name="clickPosition">����� ����� ������.</param>
    /// <param name="currentPosition">������� ������� ������.</param>
    private void MoveToClick(Vector2 clickPosition, Vector3 currentPosition)
    {
        if (clickPosition.x != currentPosition.x && clickPosition.y != currentPosition.y)
        {
            // ��������� ������ � ������������ �� �������� ��������� � �����, �� � ������ ������� - �����������.
            Vector2 _difference = (clickPosition - new Vector2(currentPosition.x, currentPosition.y)).normalized;

            // ������������ ������. ���������� ���� ����� �������.
            float angle = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);


            // ���� ������� ���������� x � y ������ ������ ���������� �� �������,�� �������� ������������
            if (Math.Abs(clickPosition.x - currentPosition.x) >= 0.1f || Math.Abs(clickPosition.y - currentPosition.y) >= 0.1f)
            {
                // ����� �� ���� ������ ��� ������� �� ����,���� ���� �������������� � ������� ����������. �������� ���� �� �����
                if (_difference.magnitude != 0)
                {
                    transform.position += new Vector3(_difference.x * moveSpeed, _difference.y * moveSpeed, 0);
                }
            }
            UpdateCameraPosition();
        }
    }

    /// <summary>
    /// ������ ������� ������ ������ � ����������� � �� ������.
    /// </summary>
    private void SetupCamera()
    {
        GameObject camera = new GameObject("MainCamera2");
        camera.AddComponent<Camera>();

        mainCamera = camera.GetComponent<Camera>();
        mainCamera.backgroundColor = Color.gray;
        mainCamera.orthographic = true;
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        mainCamera = camera.GetComponent<Camera>();
    }

    private void UpdateCameraPosition()
    {

        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
