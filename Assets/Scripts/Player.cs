using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;

    public float _movSpeed = 0f;

    
    [SerializeField] 
    private GameObject _ballPrefab;
    
    [SerializeField]
    private int _numBalls = 3;

    private bool _activeBall = false;

    [SerializeField] 
    private UIManager _uiManager;

    [SerializeField] 
    private SpawnManager _spawnManager;

    private int _maxPoints;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, -4.5f, 0f);
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
            _uiManager.showEndGame(_maxPoints);
            
            DestroyImmediate(_ballPrefab.gameObject, true);
            DestroyImmediate(_spawnManager.gameObject, true);
            DestroyImmediate(this.gameObject, true);
        }

    }
    void ShootBall()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _numBalls>0 && !_activeBall)
        {
            //Debug.Log(message:"space bar pressed");
            Instantiate(_ballPrefab, transform.position + new Vector3(0f,0.7f,0f), Quaternion.identity);
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
        if (transform.position.x > 7.34f)
        {
            transform.position = new Vector3(7.34f, this.transform.position.y, 0f);
            _movSpeed = 0f;
        }
        else if (transform.position.x < -7.34f)
        {
            transform.position = new Vector3(-7.34f, this.transform.position.y, 0f);
            _movSpeed = 0f;
        }
    }

    public void setMaxPoints(int maxPoints)
    {
        _maxPoints = maxPoints;
    }

    public void EnlargePlayer()
    {
        this.transform.localScale += new Vector3(2,0,0);
        StartCoroutine(ReverseEnlarge());
    }

    IEnumerator ReverseEnlarge()
    {
        yield return new WaitForSeconds(4);
        this.transform.localScale -= new Vector3(2, 0, 0);
    }
}
