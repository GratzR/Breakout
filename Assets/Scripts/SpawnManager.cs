using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _virusPrefab;
    [SerializeField] private float _delay = 2f;

    private bool _spawningOn = true;
    void Start()
    {
        StartCoroutine(SpawnSystem());
    }

    public void OnPlayerDeath()
    {
        _spawningOn = false;
    }


    IEnumerator SpawnSystem()
    {
        while (_spawningOn)
        {
            Instantiate(_virusPrefab, new Vector3(Random.Range(-10f, 10f), 7f, 0f), Quaternion.identity, this.transform);
            yield return new WaitForSeconds(_delay);
        }
        Destroy(this.gameObject);
        
    }
}
