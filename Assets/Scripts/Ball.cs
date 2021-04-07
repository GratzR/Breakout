using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float _vSpeed = 8f;
    [SerializeField] 
    private float _hSpeed = 0f;
    
    private bool _faceUp = true;
    // Update is called once per frame
    void Update()
    {
        Wall();
        if (_faceUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * _vSpeed 
                                + Vector3.right * Time.deltaTime * (_hSpeed*3));
        }
        else
        {
            transform.Translate(Vector3.down* Time.deltaTime * _vSpeed 
                                + Vector3.right * Time.deltaTime * (_hSpeed*3));
        }

        if (transform.position.y > 5.5f)
        {
            _faceUp = false;
        }
        else if (transform.position.y < -5.5f)
        {
            Destroy(this.gameObject);
        }
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            //hit at the side
            _faceUp = true;
            _hSpeed += other.GetComponent<Player>()._movSpeed;
        }
        else if (other.CompareTag("Block"))
        {
            _faceUp = !_faceUp;
        }
    }

    void Wall()
    {
        if (transform.position.x > 10 || transform.position.x < -10)
        {
            _hSpeed = -_hSpeed;
        }
    }
}
