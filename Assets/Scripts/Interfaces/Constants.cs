///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

using UnityEngine;

public class Constants : MonoBehaviour
{
    // Число тиков сервера. Миллисекунда делится на это число и получается миллисекунд на один тик.
    public          const int           TICKS_PER_SEC = 60;
    private         const int           MS_PER_TICK = 1000 / TICKS_PER_SEC;

    public          const float         SPEED_KOEFFICIENT = 0.03f * 1.0f / TICKS_PER_SEC ;
}
