using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeToObj : MonoBehaviour
{
    [SerializeField] GameObject background;
    Vector3 objectSize;

    // Start is called before the first frame update
    void Start()
    {
        SearchSize();
        Resize();
    }

    void SearchSize()
    {
        objectSize = background.GetComponent<Renderer>().bounds.size;
    }

    void Resize()
    {
        gameObject.transform.localScale = objectSize*6;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
