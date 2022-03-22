using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }

    public void Startup()
    {
        Debug.Log("Player manager starting...".SetColor(Color.Yellow));



        Status = ManagerStatus.Started;
        Debug.Log("Player manager started.".SetColor(Color.Green));
    }

}
