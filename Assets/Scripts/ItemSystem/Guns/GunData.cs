using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Guns",fileName ="NewGun",order =53)]
public class GunData : BaseItemData
{
    public override bool IsWeapon()
    {
        return true;
    }

    private void OnValidate()
    {
        base.OnValidate();
        if(Managers.Resources != null)
        {
            //this.data = Managers.Resources.DownloadData(ammoKind);
        }

    }

}
