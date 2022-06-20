using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AttackOnDoubleClick))]
public class AsteroidView : MonoBehaviour
{
    private AsteroidState asteroidState;

    private static TargetLight targetLight = null;
    public AsteroidView Init(AsteroidState state)
    {
        this.asteroidState = state;

        GetComponent<SpriteRenderer>().sprite = asteroidState.Data.Icon;
        GetComponent<SphereCollider>().isTrigger = true;

        return this;
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
}
