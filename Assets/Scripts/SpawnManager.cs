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

    [SerializeField] 
    private Player _player;

    void Start()
    {
        var tiers = new List<int> {4,3,2,2,1,1};
        
        int maxPoints = 0;
        for (int i = 0; i < _numBlockRows; i++)
        {
            maxPoints += SpawnBlockRow(i, tiers[i]);
        }
        
        _player.setMaxPoints(maxPoints);
    }

    private int SpawnBlockRow(int numBlockRows, int tier)
    {
        GameObject newObject = Instantiate(_blockRowPrefab, new Vector3(0f, 3.75f - numBlockRows*0.4f, 0), Quaternion.identity, this.transform);
        BlockRow blockRow = newObject.GetComponent<BlockRow>();
        int rowPoints = blockRow.initBlockRow(tier);

        return rowPoints;
    }
}
