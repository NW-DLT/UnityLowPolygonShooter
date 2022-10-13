using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbSpawner : MonoBehaviour
{
    [SerializeField] GameObject _orb;
    float _spawnInterval = 1;
    float _spawnStartDelay = 1;
    //Vector3 _screenBounds;
    //float _screenBoundsX;
    //float _screenBoundsY;
    //float _screenBoundsZ = 2;
    private void Start()
    {
        InvokeRepeating("SpawnOrb", _spawnStartDelay, _spawnInterval);
        //_screenBounds = this.transform.position;
        //_screenBoundsX = Mathf.Abs(_screenBounds.x) - 1;
        //_screenBoundsY = Mathf.Abs(_screenBounds.y);
    }
    void SpawnOrb()
    {
        Instantiate(_orb, new Vector3(2,2,2) /*new Vector3(Random.Range(-_screenBoundsX, _screenBoundsX), Random.Range(-_screenBoundsY + 10, _screenBoundsY), _screenBoundsZ)*/, transform.rotation);
    }
}
