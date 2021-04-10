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
        //because the object is rotated Vector3.left makes the object move downwards
        transform.Translate(Vector3.left * Time.deltaTime*5);
        Passed();
    }

    //destroys the object after it passed by the player
    void Passed()
    {
        if (transform.position.y < -5.2f)
        {
            Destroy(this.gameObject);
        }
    }
    
    //activates the right effect when the Power-up is collected
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

    //receives the information about the type of Power-up that is generated
    public void setPowerUpType(int type)
    {
        _type = type;
    }
}
