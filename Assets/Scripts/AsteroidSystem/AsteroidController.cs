using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(SpriteRenderer))]
public class AsteroidController : MonoBehaviour
{
    private float moveSpeed = 2.0f / Constants.TICKS_PER_SEC;
    private Vector3 originPoint = new Vector3();
    private GameObject spawner;
    private AsteroidState asteroid;


    private GameObject infoWindow = null;
    private bool isClicked = false;

    [SerializeField] private string name;

    public AsteroidState Asteroid => asteroid;


    public void Init(Transform spawner,AsteroidState asteroidState,Vector2 quarter)
    {
        this.originPoint = spawner.transform.position;
        this.spawner = spawner.gameObject;
        this.asteroid = asteroidState;

        xMax += originPoint.x + 150 * quarter.x;
        xMin += originPoint.x + 150 * quarter.x;
        yMax += originPoint.y + 150 * quarter.y;
        yMin += originPoint.y + 150 * quarter.y;

        this.transform.position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);

        this.GetComponent<SpriteRenderer>().sprite = asteroid.Data.Icon;
        this.GetComponent<SphereCollider>().isTrigger = true;
    }

    public void CloseInfoWindow()
    {
        if(infoWindow != null)
        {
            Destroy(infoWindow.gameObject);
            infoWindow = null;
            isClicked = false;
        }
    }

    #region Movement
    // Координаты, в пределах которых осуществляется движение
    public float xMax = +50;
    public float xMin = -50;
    public float yMax = 50;
    public float yMin = -50;

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Bullet>())
        {
            var bullet = other.GetComponent<Bullet>();
            this.GetComponent<AsteroidState>().ChangeHealth(-bullet.Strength);
            Destroy(other.gameObject);
        }    
        Debug.Log($"Trigger of asteroid and {other.gameObject.name}");
        this.GetComponent<SpriteRenderer>().color = UnityEngine.Color.magenta;
    }



    private void OnMouseEnter()
    {
        var data = this.gameObject.GetComponent<AsteroidState>();
        Debug.Log($"Это объект: {data.Data.Title} {data.Data.Description} {data.Health}/{data.MaxHealth}");

        if(infoWindow == null)
        {
            infoWindow = new GameObject("InfoWindow");
            infoWindow.AddComponent<InfoWindow>().Init(this.gameObject.GetComponent<AsteroidController>());
        }
    }

    private void OnMouseDown()
    {
        isClicked = true;
    }

    private void OnMouseExit()
    {
        if(infoWindow != null && !isClicked)
        {
            Destroy(infoWindow.gameObject);
            infoWindow = null;
        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            spawner.gameObject.GetComponent<AsteroidFieldView>().DestroyAsteroid(this.gameObject.GetComponent<AsteroidState>().Id);

            if (infoWindow != null && !isClicked)
            {
                Destroy(infoWindow.gameObject);
                infoWindow = null;
            }
        }
    }
}

