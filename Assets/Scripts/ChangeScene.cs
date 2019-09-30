using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string sceneName;

    public void Change()
    {
        Application.LoadLevel(sceneName);
    }
}
