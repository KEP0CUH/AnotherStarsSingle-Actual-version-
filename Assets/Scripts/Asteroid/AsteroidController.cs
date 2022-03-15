using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SpriteRenderer))]
public class AsteroidController : MonoBehaviour
{
    private float moveSpeed = 2.0f / Constants.TICKS_PER_SEC;

    [SerializeField] private AsteroidType asteroidType;
    [SerializeField] private string name;


    public enum AsteroidType
    {
        gold,
        mineral,
        empty
    }

    private void OnValidate()
    {
        switch (asteroidType)
        {
            case AsteroidType.gold:
                name = "Золотой астероид";
                break;
            case AsteroidType.mineral:
                name = "Минеральный астероид";
                break;
            case AsteroidType.empty:
                name = "Пустой астероид";
                break;
        }
    }

    #region Movement
    // Координаты, в пределах которых осуществляется движение
    public int xMax = 50;
    public int xMin = -50;
    public int yMax = 50;
    public int yMin = -50;

    // Радиус и углы не используются, но в будущем возможно генерация позиции будет именно по окружности, а не по прямоугольнику.
    public int radius = 50;
    public float angle = 0f;

    // Различие между текущей позицией и целевой. Это нужно для движения от и до.
    Vector2 difference;
    Vector2 targetPos;

    // Прибыл ли враг в целевую точку?
    bool moveEnd = true;

    // Целевые координаты - генерируются рандомно.
    private float xTarget;
    private float yTarget;

    // Максимальное время полета. Если враг раньше прибудет в целевую точку, то он сразу отправится в другую.
    [SerializeField]
    private float delayTime = 10f;

    private void Awake()
    {
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Asteroids/Asteroid_TestType");
        this.GetComponent<BoxCollider>().isTrigger = true;
    }

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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision of asteroid and {collision.gameObject.name}");
        this.GetComponent<SpriteRenderer>().color = UnityEngine.Color.magenta;

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger of asteroid and {other.gameObject.name}");
        this.GetComponent<SpriteRenderer>().color = UnityEngine.Color.magenta;

    }

}

