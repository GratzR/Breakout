using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    [Header("Block Properties")]
    private int _tier = 1;
    private int _currentLives = 1;
    private int _numberPoints = 1;
    private int _powerUpType = 0;
    //lookup table for the tier/lives and corresponding points
    private List<List<int>> _tierData = new List<List<int>>
    {
        new List<int>{1,1},
        new List<int>{2,3},
        new List<int>{3,5},
        new List<int>{4,10}
    };
    
    [Header("Block Materials")]
    [SerializeField]
    private Material _tier1_mat;
    [SerializeField]
    private Material _tier2_mat;
    [SerializeField]
    private Material _tier3_mat;
    [SerializeField]
    private Material _tier4_mat;
    
    [SerializeField]
    private Texture _albedo_powerUp;
    
    [Header("PoweUp Prefab")]
    [SerializeField] 
    private PowerUp _powerUp;
    
    //creates the blocks
    public int setupBlock(int tier, bool spawnPowerupInBlock)
    {
        //looks up the information about the block depending on the tier
        List<int> currentTier = _tierData[tier - 1];

        _tier = tier;
        _currentLives = currentTier[0];
        _numberPoints = currentTier[1];
        
        //if the block should contain a Power-up a random Power-up is assigned
        if (spawnPowerupInBlock)
        {
            _powerUpType = Random.Range(1,5);
        }
        
        setColorToRemainingLives(_currentLives);

        return _numberPoints;
    }

    //controls what happens when a ball hits the block
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            _currentLives--;

            setColorToRemainingLives(_currentLives);

            if (_currentLives <= 0)
            {
                //generates the collectable Power-up
                if (_powerUpType > 0)
                {
                    PowerUp powerUp = Instantiate(_powerUp, this.transform.position, Quaternion.Euler(0,0,90), this.transform.parent.gameObject.transform);
                    powerUp.setPowerUpType(_powerUpType);
                }
                
                //raises the score and destroys the block
                GameObject.FindWithTag("UIManager").GetComponent<UIManager>().AddScore(_numberPoints);
                Destroy(this.gameObject);
            }
        }
    }

    //controls the visual appearance of each block
    void setColorToRemainingLives(int currentLives)
    {
        switch (currentLives)
        {
            case 1:
                GetComponent<Renderer>().material = _tier1_mat;
                break;
            case 2:
                GetComponent<Renderer>().material = _tier2_mat;
                break;
            case 3:
                GetComponent<Renderer>().material = _tier3_mat;
                break;
            case 4:
                GetComponent<Renderer>().material = _tier4_mat;
                break;
        }

        //creates the frame that indicates blocks containing Power-ups
        if (_powerUpType > 0)
        {
            GetComponent<Renderer>().material.mainTexture = _albedo_powerUp;
        }
    }
}
