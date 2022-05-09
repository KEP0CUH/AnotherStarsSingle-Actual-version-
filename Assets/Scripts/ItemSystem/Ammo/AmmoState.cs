using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoState : MonoBehaviour
{
    private AmmoData data;
    private AmmoController controller;
    private float moveSpeed = 2f / Constants.TICKS_PER_SEC;

    public AmmoData Data => data;
    public float MoveSpeed => moveSpeed;

    public AmmoState Init(AmmoController controller,AmmoKind kind)
    {
        this.controller = controller;
        this.data = Managers.Resources.DownloadData(kind);

        return this;
    }
}
