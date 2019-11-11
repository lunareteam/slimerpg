using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spritemovechg : MonoBehaviour
{
    [SerializeField] GameObject Player;
    bool a;
    public bool Geta()
    {
        return a;
    }
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = Player.GetComponent<Animator>();
    }
    
    IEnumerator IsMoving()
    {
        Vector3 prevPos = Player.transform.position;
        yield return new WaitForSeconds(0.01f);
        Vector3 actualPos = Player.transform.position;
         a= (prevPos != actualPos);
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(IsMoving());
        Player.GetComponent<Animator>().SetBool("move", a);
       
    }
}
