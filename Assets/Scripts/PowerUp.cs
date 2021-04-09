using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime*5);
        Passed();
    }

    void Passed()
    {
        if (transform.position.y < -5.2f)
        {
            Destroy(this.gameObject);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().EnlargePlayer();
            Destroy(this.gameObject);

        }
    }
}
