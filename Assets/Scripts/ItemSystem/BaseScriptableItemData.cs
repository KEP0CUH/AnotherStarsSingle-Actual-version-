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
                title = "������";
                description = "������ � ������� ���������� ����������� ������������. ������ ������������� �� ��������� \"���� �����\"";
                break;
            case ItemKind.rudaGold:
                name = "Gold";
                icon = Resources.Load<Sprite>("Icons/Items/Minerals/" + name);
                title = "������";
                description = "������ � ��� ������ ������, ��������� ������������ � ������� �����.�������� ���������� ����� ����������� ����� ����.";
                break;
            case ItemKind.rudaNickel:
                name = "Nickel";
                icon = Resources.Load<Sprite>("Icons/Items/Minerals/" + name);
                title = "������";
                break;
            case ItemKind.rudaTitan:
                name = "Titan";
                icon = Resources.Load<Sprite>("Icons/Items/Minerals/" + name);
                title = "�����";
                break;

            case ItemKind.weaponKinetic:
                {
                    name = "������������";
                    break;
                }
            case ItemKind.weaponLaser:
                {
                    name = "��������";
                    break;
                }
            case ItemKind.weaponEnergetic:
                {
                    name = "��������������";
                    break;
                }
        }
    }
}
