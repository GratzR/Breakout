using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        /*transform.Translate(Vector3.down * Time.deltaTime * _decendSpeed);

        if (transform.position.y < -6.5f)
        {
            transform.position = new Vector3(Random.Range(-10f,10f), 6.5f, 0f);
                
        }*/
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            //not relevant
            other.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Ball"))
        {
            Destroy(this.gameObject);
        }
    }
}
