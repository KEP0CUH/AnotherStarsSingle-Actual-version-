using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class DamageText : MonoBehaviour
{
    public Text text; // поле для UI текста
    private Transform parent;

    public void Init(Transform parent,string content)
    {
        var camera = Managers.Player.Controller.MainCamera;

        this.parent = parent;

        var cameraPos = camera.transform.position;

        var cameraScreenPos = camera.WorldToScreenPoint(cameraPos);

        var difference = new Vector2(this.parent.position.x - cameraPos.x,this.parent.position.y - cameraPos.y);
        Debug.Log(difference);

        Debug.Log(parent.transform.position);
        difference = camera.ScreenToWorldPoint(parent.transform.position);
        Debug.Log(difference);



        var widthKoef = Screen.width;
        var heightKoef = Screen.height;

        var rect = this.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f + difference.x / Screen.width, 0.5f + difference.y / Screen.height);
        rect.anchorMax = new Vector2(0.5f + difference.x / Screen.width, 0.5f + difference.y / Screen.height);
        rect.offsetMin = new Vector2(- 20,  - 20);
        rect.offsetMax = new Vector2( + 20, + 20);
        //rect.

    }

    void Update()
    {
    }
}
