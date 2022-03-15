using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIModule
{
    public ManagerStatus Status { get;}
    public UIModuleKind Kind { get; }
    public void Startup(ICanvas canvas);

    public void Enable();
    public void Disable();
}
