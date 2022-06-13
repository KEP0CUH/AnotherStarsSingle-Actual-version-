using UnityEngine;

public class TargetLight : MonoBehaviour
{
    private float radius;

    public TargetLight Init(float radius)
    {
        this.radius = radius;
        this.gameObject.AddComponent<SpriteRenderer>().sprite = Managers.Resources.DownloadData(IconType.TargetLight);

        this.gameObject.transform.localScale = new Vector3(radius, radius, radius);

        return this;
    }
}
