using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private MobController controller;

    public MobMovement Init(MobController controller)
    {
        this.controller = controller;
        this.moveSpeed = controller.State.ShipState.Data.Speed * Constants.SPEED_KOEFFICIENT;

        return this;
    }

    #region MOVEMENT
    //  оординаты, в пределах которых осуществл€етс€ движение
    public float xMax = +15;
    public float xMin = -15;
    public float yMax = 15;
    public float yMin = -15;

    // –адиус и углы не используютс€, но в будущем возможно генераци€ позиции будет именно по окружности, а не по пр€моугольнику.
    public int radius = 50;
    public float angle = 0f;

    // –азличие между текущей позицией и целевой. Ёто нужно дл€ движени€ от и до.
    Vector2 difference;
    Vector2 targetPos;

    // ѕрибыл ли враг в целевую точку?
    bool moveEnd = true;

    // ÷елевые координаты - генерируютс€ рандомно.
    private float xTarget;
    private float yTarget;

    // ћаксимальное врем€ полета. ≈сли враг раньше прибудет в целевую точку, то он сразу отправитс€ в другую.
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

            difference = new Vector2(xTarget - transform.localPosition.x, yTarget - transform.localPosition.y);

            Vector3 targetDir = difference;
            float angle = Mathf.Atan2(difference.y, targetDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);

            Debug.DrawLine(transform.localPosition, targetPos, UnityEngine.Color.yellow, 10);

            StartCoroutine(Delay());
        }

        transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPos,
            moveSpeed);
        transform.localPosition -= 10 * Vector3.forward;

        if (Mathf.Abs(transform.localPosition.x - difference.x) < 0.1 && Mathf.Abs(transform.localPosition.y - difference.y) < 0.1)
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
