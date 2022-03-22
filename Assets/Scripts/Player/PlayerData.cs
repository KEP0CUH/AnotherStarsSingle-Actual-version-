using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int id = 1;

    [SerializeField] private string title;
    [SerializeField] private string name;
    [SerializeField] private Sprite icon;
    [SerializeField] private string ship;

    public Sprite Icon => icon;

    public void Init()
    {
        id = 1;
        title = "Игрок";
        icon = Resources.Load<Sprite>("Images/Ships/Frigate");
    }

}
