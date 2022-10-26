using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] float damage;
    public float fireRate;
    public float range;
    [SerializeField] int bullets_count;
    //public float force;
    public ParticleSystem muzzleFlash;
    public Transform bulletSpawn;
    public AudioClip shotSFX;
    public AudioSource _audioSource;
    public GameObject hitEffect;

    private Camera _cam;
    private float nextFire = 0f;

    public static Action<GameObject> OnGameObjectClick;

    private void OnEnable()
    {
        WeaponPickUp.HandWeaponUse += tryShoot;
        WeaponPickUp.WeaponWasPickUp += set_cam;
    }

    private void set_cam(Camera cam)
    {
        this._cam = cam;
    }

    // Update is called once per frame
    void tryShoot(GameObject gun)
    {
        Debug.Log("Bullets: " + this.bullets_count);
        if (Time.time > nextFire && gun == this.gameObject && bullets_count > 0)
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

            if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, range))
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
            this.bullets_count -= 1;

    }

}
