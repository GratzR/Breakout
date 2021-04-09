using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _vSpeed = 8f;
    [SerializeField] private float _hSpeed = 0f;
    
    void Start()
    {
        _hSpeed = Random.Range(-1f, 1f);
    }
    //[SerializeField] 
    //private UIManager _uiManager;
    // Update is called once per frame

    void Update()
    {
        Wall();
        
        transform.Translate(Vector3.up * Time.deltaTime * _vSpeed
                                + Vector3.right * Time.deltaTime * (_hSpeed * 6));

        if (transform.position.y > 4.8f)
        {
            negateVerticalDirection();
        }
        else if (transform.position.y < -5.2f)
        {
            Destroy(this.gameObject);
            GameObject.FindWithTag("Player").GetComponent<Player>().LoseBall();
            //FindObjectOfType<Player>().LoseBall();
            //_uiManager.SubBall();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //hit at the side
            negateVerticalDirection();
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
            negateVerticalDirection();
        }
    }

    void Wall()
    {
        if (transform.position.x > 8.7f || transform.position.x < -8.7f)
        {
            negatehorizontalDirection();
        }
    }

    void negateVerticalDirection()
    {
        _vSpeed *= -1f;
    }
    
    void negatehorizontalDirection()
    {
        _hSpeed *= -1f;
    }
}