using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items",fileName ="NewItem",order=51)]
public class BaseScriptableItemData : ScriptableObject
{
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private string title;
    [SerializeField]
    private string description;
    [SerializeField]
    private ItemKind kind;

    public Sprite Icon => icon;
    public string Title => title;
    public string Description => description;

    private void OnValidate()
    {
        switch (kind)
        {
            case ItemKind.rudaFerrum:
                name = "Ferrum";
                icon = Resources.Load<Sprite>("Icons/Items/Minerals/" + name);
                title = "Железо";
                description = "Металл с высокой химической реакционной способностью. Широко распространен во Вселенной \"Иные звёзды\"";
                break;
            case ItemKind.rudaGold:
                name = "Gold";
                icon = Resources.Load<Sprite>("Icons/Items/Minerals/" + name);
                title = "Золото";
                description = "Золото – это ценный металл, известный человечеству с древних времён.Полезное ископаемое имеет характерный жёлтый цвет.";
                break;
            case ItemKind.rudaNickel:
                name = "Nickel";
                icon = Resources.Load<Sprite>("Icons/Items/Minerals/" + name);
                title = "Никель";
                break;
            case ItemKind.rudaTitan:
                name = "Titan";
                icon = Resources.Load<Sprite>("Icons/Items/Minerals/" + name);
                title = "Титан";
                break;

            case ItemKind.weaponKinetic:
                {
                    name = "Кинетическое";
                    break;
                }
            case ItemKind.weaponLaser:
                {
                    name = "Лазерное";
                    break;
                }
            case ItemKind.weaponEnergetic:
                {
                    name = "Энергетическое";
                    break;
                }
        }
    }
}
