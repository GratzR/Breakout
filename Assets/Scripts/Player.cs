using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player properties")]
    [SerializeField]
    private float _speed = 10f;

    public float _movSpeed = 0f;

    private float _horizontalBoundary = 7.34f;

    [Header("Prefabs")]
    [SerializeField] 
    private UIManager _uiManager;

    [SerializeField] 
    private SpawnManager _spawnManager;
    
    [SerializeField] 
    private GameObject _ballPrefab;
    
    [Header("Further info")]
    [SerializeField]
    private int _numBalls = 3;

    private bool _activeBall = false;
    
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

    //coordinates everything in the event of losing a ball
    public void LoseBall(int numBalls)
    {
        _numBalls -= numBalls;
        _activeBall = false;
        _uiManager.BallCount(-numBalls);
        //the game is ended when there are no balls left
        if (_numBalls == 0)
        {
            _uiManager.showEndGame();
            
            DestroyImmediate(_ballPrefab.gameObject, true);
            DestroyImmediate(_spawnManager.gameObject, true);
            DestroyImmediate(this.gameObject, true);
        }
    }
    
    //called when te Power-up of an additional ball gets collected
    public void addBall()
    {
        _numBalls += 1;
        _uiManager.BallCount(1);
    }

    //gives the player the chance to initialize a ball
    void ShootBall()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _numBalls>0 && !_activeBall)
        {
            Instantiate(_ballPrefab, transform.position + new Vector3(0f,0.7f,0f), Quaternion.identity);
            _activeBall = true;
        }
    }

    //controls the movement and boundaries of the player
    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //calculates the speed with which the player changes the position (also relevant for the ball)
        _movSpeed =  Time.deltaTime * _speed * horizontalInput;
        transform.Translate(Vector3.right*_movSpeed);
        
        // Boundaries
        if (transform.position.x > _horizontalBoundary)
        {
            transform.position = new Vector3(_horizontalBoundary, this.transform.position.y, 0f);
            _movSpeed = 0f;
        }
        else if (transform.position.x < -_horizontalBoundary)
        {
            transform.position = new Vector3(-_horizontalBoundary, this.transform.position.y, 0f);
            _movSpeed = 0f;
        }
    }

    //gives the UIManager the maximum available points for the end screen
    public void setMaxPoints(int maxPoints)
    {
        _maxPoints = maxPoints;
        _uiManager.setMaxPoints(maxPoints);
    }

    //Two of the Power-ups: increases/decreases the breadth of the player 
    public void EnlargePlayer(float duration, float size)
    {
        this.transform.localScale += new Vector3(size,0,0);
        _horizontalBoundary -= size/2;
        StartCoroutine(ReverseEnlarge(duration, size));
    }

    //Ends/reverses the Power-up
    IEnumerator ReverseEnlarge(float duration, float size)
    {
        yield return new WaitForSeconds(duration);
        this.transform.localScale -= new Vector3(size, 0, 0);
        _horizontalBoundary += size/2;
    }

    //One of the Power-ups: spawns a second ball immediately
    public void multiball()
    {
        Instantiate(_ballPrefab, transform.position + new Vector3(0f,0.7f,0f), Quaternion.identity);
        addBall();
    }
}
