using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGeneration : MonoBehaviour
{
    float radiusMax = 3;
    float radiusMin = 0.1f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            var angle = Random.Range(0, 360);
            var radius = Random.Range(radiusMin, radiusMax);

            var gameObj = new GameObject("New", typeof(ItemState), typeof(SpriteRenderer));
            gameObj.GetComponent<ItemState>().Init(ItemKind.GoldOre, 1);
            gameObj.GetComponent<SpriteRenderer>().sprite = gameObj.GetComponent<ItemState>().Data.Icon;
            gameObj.transform.position = new Vector3(transform.position.x + radius * Mathf.Sin(angle),
                                                     transform.position.y + radius * Mathf.Cos(angle),
                                                     0);
        }

    }
}
