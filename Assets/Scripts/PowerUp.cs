using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, 4f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime*2);
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
        // else if (other.CompareTag("Ball"))
        // {
        //     negateVerticalDirection();
        // }
    }
}
