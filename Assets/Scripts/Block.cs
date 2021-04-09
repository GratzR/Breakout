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

    [SerializeField]
    public Material _tier1_mat;
    [SerializeField]
    public Material _tier2_mat;
    [SerializeField]
    public Material _tier3_mat;
    [SerializeField]
    public Material _tier4_mat;
    
    private List<List<int>> _tierData = new List<List<int>>
    {
        new List<int>{1,1},
        new List<int>{2,3},
        new List<int>{3,5},
        new List<int>{4,10}
    };

    public void setupBlock(int tier)
    {
        List<int> currentTier = _tierData[tier - 1];

        _tier = tier;
        _currentLives = currentTier[0];
        _numberPoints = currentTier[1];
        
        setColorToRemainingLives(_currentLives);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            _currentLives--;

            setColorToRemainingLives(_currentLives);

            if (_currentLives <= 0)
            {
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
    }
}
