using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHide : HideLayer
{
    void Awake()
    {
        Hide();
        Unhide();
    }
}
