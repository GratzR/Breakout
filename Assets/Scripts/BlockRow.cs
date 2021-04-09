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
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _numBlocks; i++)
        {
            SpawnBlock(i);
        }
    }

    public void setTier(int tier)
    {
        _tier = tier;
    }

    private void SpawnBlock(int numBlock)
    {
        GameObject newObject = Instantiate(_blockPrefab, new Vector3(-7.7f + numBlock*1.7f, this.transform.position.y, 0), Quaternion.identity, this.transform);
        Block block = newObject.GetComponent<Block>();
        block.setupBlock(_tier);
    }
}
