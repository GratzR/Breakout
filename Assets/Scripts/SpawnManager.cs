using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] 
    private GameObject _blockRowPrefab;
    
    [SerializeField] 
    private int _numBlockRows = 6;
    // [SerializeField] private float _delay = 2f;

    // private bool _spawningOn = true;
    void Start()
    {
        var tiers = new List<int> {4,3,2,2,1,1};
        for (int i = 0; i < _numBlockRows; i++)
        {
            SpawnBlockRow(i, tiers[i]);
        }
        // StartCoroutine(SpawnSystem());
    }

    // public void OnPlayerDeath()
    // {
    //     _spawningOn = false;
    // }

    private void SpawnBlockRow(int numBlockRows, int tier)
    {
        GameObject newObject = Instantiate(_blockRowPrefab, new Vector3(0f, 3.75f - numBlockRows*0.4f, 0), Quaternion.identity, this.transform);
        BlockRow blockRow = newObject.GetComponent<BlockRow>();
        blockRow.setTier(tier);
        
    }


    // IEnumerator SpawnSystem()
    // {
    //     while (_spawningOn)
    //     {
    //         Instantiate(_virusPrefab, new Vector3(Random.Range(-10f, 10f), 7f, 0f), Quaternion.identity, this.transform);
    //         yield return new WaitForSeconds(_delay);
    //     }
    //     Destroy(this.gameObject);
    //     
    // }
}
