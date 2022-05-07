using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName="Location",fileName ="NewGalaxe",order = 54)]
public class LocationData : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private Location location;


    public string Title => title;

    private void OnValidate()
    {
        switch (location)
        {
            case Location.Krinul:
                title = "Кринул";
                description = "Центр вселенной Иные Звёзды";
                break;
            case Location.Lambda:
                title = "Лямбда";
                description = "";
                break;

        }
    }

}
