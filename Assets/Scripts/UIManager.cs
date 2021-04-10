using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Relevant properties")]
    private int _ballsLeft = 3;
    private int _maxPoints;
    private int _score;

    [Header("Texts to be displayed")]
    [SerializeField] 
    private Text _ballText;
    
    [SerializeField]
    private Text _scoreText;

    [SerializeField] 
    private Text _endGameText;
    // Start is called before the first frame update
    void Start()
    {
        _ballText.text = "Balls: " + _ballsLeft;
        _scoreText.text = "Score: " + _score;
    }

    private void Update()
    {
        //The game ends if all blocks are destroyed and all points were granted
        if (_score == _maxPoints)
        {
            showEndGame();
        }
    }

    public void setMaxPoints(int maxPoints)
    {
        _maxPoints = maxPoints;
    }
    
    public void BallCount(int _ball)
    {
        _ballsLeft += _ball;
        _ballText.text = "Balls: " + _ballsLeft;
    }

    public void AddScore(int _points)
    {
        _score += _points;
        _scoreText.text = "Score: " + _score;
    }

    //handles the scrren at the end of the game
    public void showEndGame()
    {
        //Victory screen
        if (_score == _maxPoints)
        {
            //display the text in case of victory
            _endGameText.text = "You won! \n You reached " + _score + " / " + _maxPoints + " points";
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

            //destroys all balls that are in the game
            foreach (GameObject ball in balls)
            {
                Destroy(ball.GetComponent<Ball>().gameObject);
            }
            
            //destroys the Power-ups if the last blocks contained any
            foreach (GameObject powerUp in powerUps)
            {
                Destroy(powerUp.GetComponent<PowerUp>().gameObject);
            }
            //destroys the player
            Destroy(player.gameObject);
            
            //stops the update.function from endlessly calling the end screen
            _maxPoints = -1;
        }
        //Game ends because the player ran out of balls
        else
        {
            _endGameText.text = "Game over! \n You reached " + _score + " / " + _maxPoints + " points";
        }
        Destroy(_ballText.gameObject);
        Destroy(_scoreText.gameObject);
    }

}
