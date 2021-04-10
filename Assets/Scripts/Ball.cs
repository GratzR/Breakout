using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _vSpeed = 8f;
    [SerializeField] private float _hSpeed = 0f;

    private bool _faceUp = true;
    
    void Start()
    {
        //give the ball a random direction when it is instantiated
        _hSpeed = Random.Range(-1f, 1f);
    }

    void Update()
    {
        Wall();
        //making sure the ball is heading in the right vertical direction
        if (_faceUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * _vSpeed
                                + Vector3.right * Time.deltaTime * (_hSpeed * 6));
        }
        else
        {
            transform.Translate(Vector3.down * Time.deltaTime * _vSpeed
                                + Vector3.right * Time.deltaTime * (_hSpeed * 6));
        }

        //setting the upper and lower boundary
        if (transform.position.y > 4.8f)
        {
            //invert the direction at the top of the screen
            _faceUp = false;
        }
        else if (transform.position.y < -5.2f)
        {
            //lose a ball at the bottom of the screen
            Destroy(this.gameObject);
            GameObject.FindWithTag("Player").GetComponent<Player>().LoseBall(1);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //hitting the player inverts the vertical and influences the horizontal direction
            _faceUp = true;
            _hSpeed += other.GetComponent<Player>()._movSpeed;
            if (_hSpeed > 6f)
            {
                _hSpeed = 6f;
            }
            else if (_hSpeed < -6f)
            {
                _hSpeed = -6f;
            }
        }
        else if (other.CompareTag("Block"))
        {
            //getting reflected by blocks
            _faceUp = !_faceUp;
        }
    }

    //sets boundaries on the left and right side of the screen
    void Wall()
    {
        if (transform.position.x > 8.7f)
        {
            _hSpeed = Math.Abs(_hSpeed) * -1;
        }
        else if(transform.position.x < -8.7f)
        {
            _hSpeed = Math.Abs(_hSpeed);
        }
    }
}