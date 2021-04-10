using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] 
    private float _duration = 4f;

    private const int ENLARGE = 1;
    private const int EXTRA_BALL = 2;
    private const int SHRINK = 3;
    private const int MULTIBALL = 4;

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
                    other.GetComponent<Player>().EnlargePlayer(_duration, 2f);
                    break;
                case EXTRA_BALL:
                    other.GetComponent<Player>().addBall();
                    break; 
                case SHRINK:
                    other.GetComponent<Player>().EnlargePlayer(_duration, -1f);
                    break; 
                case MULTIBALL:
                    other.GetComponent<Player>().multiball();
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
