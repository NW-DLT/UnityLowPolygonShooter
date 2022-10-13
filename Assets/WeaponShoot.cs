using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public float damage = 1f;
    public float fireRate = 20f;
    public float range = 15f;
    public float force = 100f;
    public ParticleSystem muzzleFlash;
    public Transform bulletSpawn;
    public AudioClip shotSFX;
    public AudioSource _audioSource;
    public GameObject hitEffect;

    public Camera _cam;
    private float nextFire = 0f;

    public static Action<GameObject> OnGameObjectClick;

    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        _audioSource.PlayOneShot(shotSFX);
        muzzleFlash.Play(); 

        RaycastHit hit;

        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward,out hit,range))
        {
            //Debug.Log("Попал " + hit.collider);

            GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 0.1f);
            OnGameObjectClick(hit.transform.gameObject);
       
        }
        else
        {
            ScoreViewer.instance.RemovePoint();
        }
    }

}
