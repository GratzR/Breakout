using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRow : MonoBehaviour
{
    [SerializeField]
    private GameObject _blockPrefab;

    [SerializeField] 
    private int _numBlocks = 10;

    private int _tier = 1;
    
    public int initBlockRow(int tier)
    {
        _tier = tier;
        
        int rowPoints = 0;
        for (int i = 0; i < _numBlocks; i++)
        {
            rowPoints += SpawnBlock(i);
        }

        return rowPoints;
    }

    private int SpawnBlock(int numBlock)
    {
        GameObject newObject = Instantiate(_blockPrefab, new Vector3(-7.7f + numBlock*1.7f, this.transform.position.y, 0), Quaternion.identity, this.transform);
        Block block = newObject.GetComponent<Block>();
        int blockPoints = block.setupBlock(_tier);

        return blockPoints;
    }
}
