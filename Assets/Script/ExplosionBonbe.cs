using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBonbe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void OnCollisionEnter2D(Collision2D infoCollision)
    {
        if(infoCollision.gameObject.name =="Megaman")
        {
           // print("hello");
            GetComponent<Animator>().enabled = true;
            Destroy(gameObject, 1f);
        }
    }
}
