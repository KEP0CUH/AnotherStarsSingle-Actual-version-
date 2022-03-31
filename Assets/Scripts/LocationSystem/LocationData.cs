using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName="Location",fileName ="NewGalaxe",order = 54)]
public class LocationData : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private Location location;
    [SerializeField] private Sprite iconSun;


    public string Title => title;
    public Sprite Icon => iconSun;

    private void OnValidate()
    {
        switch (location)
        {
            case Location.Krinul:
                title = "Кринул";
                description = "Центр вселенной Иные Звёзды";
                iconSun = Resources.Load<Sprite>("Icons/Planets/YellowSun");
                break;
        }
    }

}
