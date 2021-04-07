using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;

    public float _movSpeed = 0f;

    [SerializeField] 
    private GameObject _ballPrefab;

    //[SerializeField] 
    //private SpawnManager _spawnManager;
    //private GameObject _spawnManager;
    
    [SerializeField]
    private int _numBalls = 3;
    
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

    }
    void ShootBall()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _numBalls>0)
        {
            //Debug.Log(message:"space bar pressed");
            Instantiate(_ballPrefab, transform.position + new Vector3(0,0.7f,0), Quaternion.identity);
            LoseBall();
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
