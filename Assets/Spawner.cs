using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> _orbs = new List<GameObject> {};
    float _spawnInterval = 1;
    float _spawnStartDelay = 1;
    Vector3 _screenBounds;
    float _screenBoundsX;
    float _screenBoundsY;
    float _screenBoundsZ = 5;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnOrb", _spawnStartDelay, _spawnInterval);
        _screenBounds = this.transform.position;
        _screenBoundsX = Mathf.Abs(_screenBounds.x) - 3;
        _screenBoundsY = Mathf.Abs(_screenBounds.y) - 3;
    }
    void SpawnOrb()
    {
        Instantiate(_orbs[Random.Range(0,2)], new Vector3(Random.Range(-_screenBoundsX, _screenBoundsX), Random.Range(-_screenBoundsY + 10, _screenBoundsY), _screenBoundsZ), transform.rotation);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
