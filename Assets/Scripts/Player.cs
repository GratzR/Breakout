﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;

    public float _movSpeed = 0f;

    
    [SerializeField] 
    private GameObject _ballPrefab;
    
    [SerializeField]
    private int _numBalls = 3;

    private bool _activeBall = false;

    [SerializeField] 
    private UIManager _uiManager;

    //[SerializeField] 
    //private SpawnManager _spawnManager;
    //private GameObject _spawnManager;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, -4f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        ShootBall();

    }

    public void LoseBall()
    {
        _numBalls -= 1;
        _activeBall = false;
        _uiManager.BallCount(-1); //alternativ _numBalls geben
        if (_numBalls == 0)
        {
            Destroy(this.gameObject);
        }

    }
    void ShootBall()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _numBalls>0 && !_activeBall)
        {
            //Debug.Log(message:"space bar pressed");
            Instantiate(_ballPrefab, transform.position + new Vector3(0,0.7f,0), Quaternion.identity);
            //LoseBall();
            _activeBall = true;
        }
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _movSpeed =  Time.deltaTime * _speed * horizontalInput;
        transform.Translate(Vector3.right*_movSpeed);
        
        // Boundries
        if (transform.position.x > 10f)
        {
            transform.position = new Vector3(10f, -4f, 0f);
            //transform.position = transform.position;
            _movSpeed = 0;
        }
        else if (transform.position.x < -10f)
        {
            transform.position = new Vector3(-10f, -4, 0f);
            _movSpeed = 0;
        }
    }
    
}
