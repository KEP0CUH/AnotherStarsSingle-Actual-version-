using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private float moveSpeed;
    private AsteroidController controller;
    private Transform spawner;
    private Vector2 quarter;
    public AsteroidMovement Init(AsteroidController controller,Transform spawner,Vector2 quarter)
    {
        this.controller = controller;
        this.moveSpeed = controller.State.MoveSpeed * Constants.SPEED_KOEFFICIENT;
        this.spawner = spawner;
        this.quarter = quarter;

        ConfigMovementParameters();

        return this;
    }

    private void ConfigMovementParameters()
    {
        var originPoint = spawner.transform.localPosition;
        xMax += originPoint.x + 150 * quarter.x;
        xMin += originPoint.x + 150 * quarter.x;
        yMax += originPoint.y + 150 * quarter.y;
        yMin += originPoint.y + 150 * quarter.y;

        this.transform.position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
    }

    #region MOVEMENT
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
}
