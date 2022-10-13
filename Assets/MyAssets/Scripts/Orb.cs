using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Orb : MonoBehaviour
{
    float _movementDuration = 0.3f;
    float _elapseedTime;
    Vector3 _orbSize;
    // Start is called before the first frame update
    void OnEnable()
    {
        WeaponShoot.OnGameObjectClick += OrbWasHit;
        Ground.GameObjFallToGround += OrbWasFallToGround;
    }

    private void OrbWasFallToGround(GameObject orb)
    {
        if (orb == this.gameObject)
        {
            StartCoroutine("GetSmaller");
            ScoreViewer.instance.RemovePoint();
        }
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        _orbSize = new Vector3(4, 4, 4);
        Debug.Log(_orbSize);
    }

    void OrbWasHit(GameObject orb)
    {
        //WeaponShoot.OnGameObjectClick -= OrbWasHit;
        if (orb == this.gameObject)
        {
            StartCoroutine("GetSmaller");
            ScoreViewer.instance.AddPoint();
        }
    }
    private void OnDestroy()
    {
        WeaponShoot.OnGameObjectClick -= OrbWasHit;
        Ground.GameObjFallToGround -= OrbWasFallToGround;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this);
        }
    }

    IEnumerator GetSmaller()
    {
        while (_elapseedTime < _movementDuration)
        {
            //Debug.Log(_orbSize);
            transform.localScale = Vector3.Lerp(_orbSize, new Vector3(0, 0, 0), _elapseedTime / _movementDuration);
            _elapseedTime += Time.deltaTime;
        }
        Destroy(gameObject);
        yield return null;
    }
}
