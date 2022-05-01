using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AsteroidField", fileName = "newAsteroidField", order = 54)]
public class AsteroidFieldData : ScriptableObject
{
    [SerializeField] private string title;

    [SerializeField] private AsteroidFieldType fieldType;
    [SerializeField] private Sprite iconField;
    [SerializeField] private ItemData dropItemData;

    public string Title => title;
    public AsteroidFieldType Type => fieldType;
    public Sprite Icon => iconField;

    private void OnValidate()
    {
        iconField = Resources.Load<Sprite>("Images/Asteroids/AsteroidField");
        string dropPath = $"ScriptableObjects/Items/Minerals/";
        switch (fieldType)
        {
            case AsteroidFieldType.GoldField:
                title = "Золотое поле астероидов";
                dropItemData = Resources.Load<ItemData>(dropPath + "Gold");
                break;
            case AsteroidFieldType.FerrumField:
                title = "Железное поле астероидов";
                dropItemData = Resources.Load<ItemData>(dropPath + "Ferrum");
                break;

        }
    }
}
