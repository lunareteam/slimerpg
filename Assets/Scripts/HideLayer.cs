using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLayer : MonoBehaviour
{
    [SerializeField] GameObject hideable;
    [SerializeField] GameObject showable;

    public void Hide(){
       hideable.SetActive(false); 
    }

    public void Unhide(){
        showable.SetActive(true);
    }
}
