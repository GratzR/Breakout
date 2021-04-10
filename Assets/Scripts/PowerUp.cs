using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] 
    private float _duration = 4f;

    private const int ENLARGE = 1;
    private const int EXTRA_BALL = 2;

    private int _type;
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
            switch (_type)
            {
                case ENLARGE:
                    other.GetComponent<Player>().EnlargePlayer(_duration);
                    break;
                case EXTRA_BALL:
                    other.GetComponent<Player>().addBall();
                    break;    
            }
            Destroy(this.gameObject);
        }
    }

    public void setPowerUpType(int type)
    {
        _type = type;
    }
}
