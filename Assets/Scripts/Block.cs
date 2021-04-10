using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    private int _tier = 1;
    private int _currentLives = 1;
    private int _numberPoints = 1;
    private int _powerUpType = 0;

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
    
    [SerializeField] 
    private PowerUp _powerUp;
    
    private List<List<int>> _tierData = new List<List<int>>
    {
        new List<int>{1,1},
        new List<int>{2,3},
        new List<int>{3,5},
        new List<int>{4,10}
    };

    public int setupBlock(int tier, bool spawnPowerupInBlock)
    {
        List<int> currentTier = _tierData[tier - 1];

        _tier = tier;
        _currentLives = currentTier[0];
        _numberPoints = currentTier[1];
        if (spawnPowerupInBlock)
        {
            _powerUpType = Random.Range(1,5);
        }
        
        setColorToRemainingLives(_currentLives);

        return _numberPoints;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            _currentLives--;

            setColorToRemainingLives(_currentLives);

            if (_currentLives <= 0)
            {
                if (_powerUpType > 0)
                {
                    PowerUp powerUp = Instantiate(_powerUp, this.transform.position, Quaternion.Euler(0,0,90), this.transform.parent.gameObject.transform);
                    powerUp.setPowerUpType(_powerUpType);
                }
                
                GameObject.FindWithTag("UIManager").GetComponent<UIManager>().AddScore(_numberPoints);
                Destroy(this.gameObject);
            }
        }
    }

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

        if (_powerUpType > 0)
        {
            GetComponent<Renderer>().material.mainTexture = _albedo_powerUp;
        }
    }
}
