using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanvas
{
    public GameObject AddModule(GameObject gameObject, string layer = "UI");
}
