///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

public interface IUIModule
{
    public          ManagerStatus           Status { get;}
    public          UIModuleKind            Kind { get; }
    public          void                    Startup(ICanvas canvas);

    public          void                    Enable();
    public          void                    Disable();
}
