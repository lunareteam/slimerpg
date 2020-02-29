using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeHUD : MonoBehaviour
{
    private GameObject[] ui;
    private BoxCollider2D[] boxColliders;
    private RectTransform[] rectTransforms;

    void Start()
    {
        // Resize hub container to screen size
        RectTransform hud = transform.parent.gameObject.GetComponent<RectTransform>();
        transform.gameObject.GetComponent<RectTransform>().sizeDelta = hud.sizeDelta;

        // Resize objects in hud
        ui = new GameObject[] {
            transform.Find("Health").gameObject,
            transform.Find("Mana").gameObject,
            transform.Find("Pause Button").gameObject,
            transform.Find("Skill Holder/Skill 1").gameObject,
            transform.Find("Skill Holder/Skill 2").gameObject,
            transform.Find("Skill Holder/Skill 3").gameObject
        };

        boxColliders = new BoxCollider2D[ui.Length];
        rectTransforms = new RectTransform[ui.Length];

        for(int i = 0; i < ui.Length; i++){
            boxColliders[i] = ui[i].GetComponent<BoxCollider2D>();
            rectTransforms[i] = ui[i].GetComponent<RectTransform>();
        }

        for(int i = 0; i < ui.Length; i++)
        {
            boxColliders[i].size = new Vector2(rectTransforms[i].sizeDelta.x, rectTransforms[i].sizeDelta.y);
        }
    }

    void Update()
    {        
        foreach (BoxCollider2D collider in boxColliders)
        {
            collider.offset = new Vector2(1, 1);
            collider.offset = new Vector2(0, 0);
        }
    }
}
