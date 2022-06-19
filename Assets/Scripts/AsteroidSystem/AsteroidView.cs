using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AttackOnDoubleClick))]
public class AsteroidView : MonoBehaviour
{
    private AsteroidState asteroidState;

    private static TargetLight targetLight = null;
    public AsteroidView Init(AsteroidState state,Transform spawner,Vector2 quarter)
    {
        this.asteroidState = state;

        GetComponent<SpriteRenderer>().sprite = asteroidState.Data.Icon;
        GetComponent<SphereCollider>().isTrigger = true;
        ConfigMovementParameters(spawner,quarter);

        return this;
    }

    private void ConfigMovementParameters(Transform spawner,Vector2 quarter)
    {
        var originPoint = spawner.transform.localPosition;
        xMax += originPoint.x + 150 * quarter.x;
        xMin += originPoint.x + 150 * quarter.x;
        yMax += originPoint.y + 150 * quarter.y;
        yMin += originPoint.y + 150 * quarter.y;

        this.transform.position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
    }

    private void OnMouseDown()
    {
        CreateTargetLight();
    }

    private void CreateTargetLight()
    {
        if (targetLight != null)
        {
            Destroy(targetLight.gameObject);
            targetLight = null;
        }

        var radius = this.GetComponent<SphereCollider>().radius;
        targetLight = new GameObject("TargetLight").AddComponent<TargetLight>().Init(this.transform, 2.5f * radius);
    }

    #region MOVEMENT
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

        transform.position = Vector2.MoveTowards(transform.position, targetPos, this.asteroidState.MoveSpeed * Time.fixedDeltaTime);

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
