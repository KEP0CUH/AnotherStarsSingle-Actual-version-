using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    // Число тиков сервера. Миллисекунда делится на это число и получается миллисекунд на один тик.
    public const int TICKS_PER_SEC = 10;
    private const int MS_PER_TICK = 1000 / TICKS_PER_SEC;
}
