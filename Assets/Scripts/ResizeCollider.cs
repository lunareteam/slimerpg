using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeCollider : MonoBehaviour
{
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;
    [SerializeField] GameObject button4;
    [SerializeField] GameObject button5;
    [SerializeField] GameObject button6;
    GameObject[] ui;

    void Start()
    {
        ui = new GameObject[] { button1, button2, button3, button4, button5, button6 };

        foreach (GameObject button in ui)
        {
            BoxCollider2D bc = button.GetComponent<BoxCollider2D>();
            RectTransform rect = button.GetComponent<RectTransform>();
            bc.size = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y);
        }
    }

    void Update()
    {        
        foreach (GameObject button in ui)
        {
            BoxCollider2D bc = button.GetComponent<BoxCollider2D>();
            RectTransform rect = button.GetComponent<RectTransform>();
            bc.offset = new Vector2(1, 1);
            bc.offset = new Vector2(0, 0);
        }
    }
}
