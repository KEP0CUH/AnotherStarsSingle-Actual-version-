///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipShop : MonoBehaviour
{
    private             List<ShipData>          shipData;
    private             ShipData                selectedShip;

    [SerializeField] 
    private             Image                   icon;
    [SerializeField] 
    private             Text                    cost;
    [SerializeField] 
    private             Text                    title;
    [SerializeField] 
    private             Text                    size;
    [SerializeField] 
    private             Text                    numGuns;
    [SerializeField] 
    private             Text                    numDevices;
    [SerializeField] 
    private             Text                    armor;
    [SerializeField] 
    private             Text                    shields;
    [SerializeField] 
    private             Text                    structure;
    [SerializeField] 
    private             Text                    speed;
    [SerializeField] 
    private             Text                    energy;
    [SerializeField] 
    private             Text                    cpu;
    [SerializeField] 
    private             Text                    radar;

    [SerializeField] 
    private             Button                  buyShip;

    [SerializeField]
    private             Transform               shipListTransform;

    public              void                    Init()
    {
        shipData = new List<ShipData>();
        ShowShips();
        this.buyShip.onClick.AddListener(BuyShip);
    }

    public              void                    UpdateSelectedShip(ShipData     data)
    {
        this.selectedShip = data;
        this.icon.sprite = data.Icon;
        this.cost.text       = $"      Цена         <color=blue>{data.Cost}</color>";
        this.title.text      = $"      Название     <color=blue>{data.Title}</color>";
        this.size.text       = $"      Размер       <color=blue>{data.Size}</color>";
        this.numGuns.text    = $"      Орудия       <color=blue>{data.MaxGuns}</color>";
        this.numDevices.text = $"      Устройства   <color=blue>{data.MaxDevices}</color>";
        this.armor.text      = $"      Броня        <color=blue>{data.Armor}</color>";
        this.shields.text    = $"      Щиты         <color=blue>{data.Shields}%</color>";
        this.structure.text  = $"      Структура    <color=blue>{data.Structure}</color>";
        this.speed.text      = $"      Скорость     <color=blue>{data.Speed}</color>";
        this.energy.text     = $"      Энергия      <color=blue>{data.Energy}</color>";
        this.cpu.text        = $"      ЦП           <color=blue>{data.Cpu}</color>";
        this.radar.text      = $"      Радар        <color=blue>{data.Radar}</color>";
    }

    private             void                    ShowShips()
    {
        shipData.Add(Managers.Resources.DownloadData(ShipKind.GreenKorvet));
        shipData.Add(Managers.Resources.DownloadData(ShipKind.GreenFrigate));
        shipData.Add(Managers.Resources.DownloadData(ShipKind.GreenLinkor));

        foreach (var ship in shipData)
        {
            var shipCell = Instantiate(Managers.Resources.DownloadData(ObjectType.ShipCell));
            shipCell.GetComponent<ShipCell>().Init(this, shipListTransform, ship);
        }
    }

    private             void                    BuyShip()
    {
        Debug.Log("Changing ship...");
        Managers.Player.Controller.State.SetShip(selectedShip.Kind);
    }

}
