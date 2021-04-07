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

    [SerializeField] 
    private SpawnManager _spawnManager;
    //private GameObject _spawnManager;
    
    [SerializeField]
    private int _lives = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, -4f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        Vaccinate();

    }

    public void Damage()
    {
        _lives -= 1;

    }
    void Vaccinate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log(message:"space bar pressed");
            Instantiate(_ballPrefab, transform.position + new Vector3(0,0.7f,0), Quaternion.identity);
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
        }
        else if (transform.position.x < -10f)
        {
            transform.position = new Vector3(-10f, -4, 0f);
        }
    }
    
}
