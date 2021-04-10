using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
        
        //value is twice as big as the number of blocks, so we have a 50/50 chance for a powerup in each row
        int powerupBlock = Random.Range(0, _numBlocks * 2);
        
        int rowPoints = 0;
        for (int i = 0; i < _numBlocks; i++)
        {
            bool spawnPowerupInBlock = powerupBlock == i ? true : false;
            rowPoints += SpawnBlock(i, spawnPowerupInBlock);
        }

        return rowPoints;
    }

    private int SpawnBlock(int numBlock, bool spawnPowerupInBlock)
    {
        GameObject newObject = Instantiate(_blockPrefab, new Vector3(-7.7f + numBlock*1.7f, this.transform.position.y, 0), Quaternion.identity, this.transform);
        Block block = newObject.GetComponent<Block>();
        int blockPoints = block.setupBlock(_tier, spawnPowerupInBlock);

        return blockPoints;
    }
}
