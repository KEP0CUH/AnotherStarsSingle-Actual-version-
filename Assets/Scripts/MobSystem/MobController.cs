using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(MobState))]
public class MobController : MonoBehaviour
{
    private float moveSpeed;
    private MobState mobState;

    private GameObject spawner;

    public void Init(Transform spawner,MobKind kind)
    {
        this.mobState = this.gameObject.GetComponent<MobState>();
        this.spawner = spawner.gameObject;

        this.moveSpeed = 5.0f / Constants.TICKS_PER_SEC;
        this.mobState.Init(kind);
    }

    #region Movement
    // ����������, � �������� ������� �������������� ��������
    public float xMax = +50;
    public float xMin = -50;
    public float yMax = 50;
    public float yMin = -50;

    // ������ � ���� �� ������������, �� � ������� �������� ��������� ������� ����� ������ �� ����������, � �� �� ��������������.
    public int radius = 50;
    public float angle = 0f;

    // �������� ����� ������� �������� � �������. ��� ����� ��� �������� �� � ��.
    Vector2 difference;
    Vector2 targetPos;

    // ������ �� ���� � ������� �����?
    bool moveEnd = true;

    // ������� ���������� - ������������ ��������.
    private float xTarget;
    private float yTarget;

    // ������������ ����� ������. ���� ���� ������ �������� � ������� �����, �� �� ����� ���������� � ������.
    [SerializeField]
    private float delayTime = 10f;

    private void FixedUpdate()
    {
        if (moveEnd)
        {
            moveEnd = false;
            xTarget = UnityEngine.Random.Range(xMin, xMax);
            yTarget = UnityEngine.Random.Range(yMin, yMax);
            targetPos = new Vector2(xTarget, yTarget);

            difference = new Vector2(xTarget - transform.position.x, yTarget - transform.position.y);

            Vector3 targetDir = difference;
            float angle = Mathf.Atan2(difference.y, targetDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);

            Debug.DrawLine(transform.position, targetPos, UnityEngine.Color.yellow, 10);

            StartCoroutine(Delay());
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);

        if (Mathf.Abs(transform.position.x - difference.x) < 0.1 && Mathf.Abs(transform.position.y - difference.y) < 0.1)
        {
            moveEnd = true;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);
        moveEnd = true;
    }
    #endregion
}
