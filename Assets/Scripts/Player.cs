using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;

    public float _movSpeed = 0f;

    private float _horizontalBoundary = 7.34f;

    
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

    public void LoseBall(int numBalls)
    {
        _numBalls -= numBalls;
        _activeBall = false;
        _uiManager.BallCount(-numBalls);
        if (_numBalls == 0)
        {
            _uiManager.showEndGame();
            
            DestroyImmediate(_ballPrefab.gameObject, true);
            DestroyImmediate(_spawnManager.gameObject, true);
            DestroyImmediate(this.gameObject, true);
        }
    }
    
    public void addBall()
    {
        _numBalls += 1;
        _uiManager.BallCount(1);
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

    public void setMaxPoints(int maxPoints)
    {
        _maxPoints = maxPoints;
        _uiManager.setMaxPoints(maxPoints);
    }

    public void EnlargePlayer(float duration)
    {
        this.transform.localScale += new Vector3(2,0,0);
        _horizontalBoundary -= 1;
        StartCoroutine(ReverseEnlarge(duration));
    }

    IEnumerator ReverseEnlarge(float duration)
    {
        yield return new WaitForSeconds(duration);
        this.transform.localScale -= new Vector3(2, 0, 0);
        _horizontalBoundary += 1;
    }
}
