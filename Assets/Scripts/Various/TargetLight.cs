using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(SphereCollider))]
public class TargetLight : MonoBehaviour
{
    private static GameObject targetLight = null;
    private float radius = 1;
    public TargetLight Init(Transform parent, float radius)
    {
        Debug.Log($"Radius = {radius}");
        if (targetLight != null)
        {
            Object.Destroy(targetLight.gameObject);
            targetLight = null;
        }
        targetLight = this.gameObject;

        this.transform.SetParent(parent);
        this.transform.localPosition = new Vector3(0, 0, 5);
        this.radius = radius;
        CreateTargetLight();

        return this;
    }

    private void CreateTargetLight()
    {
        targetLight.GetComponent<SpriteRenderer>().sprite = Managers.Resources.DownloadData(IconType.TargetLight);
        targetLight.transform.localScale = new Vector3(radius, radius, radius);
    }
}
