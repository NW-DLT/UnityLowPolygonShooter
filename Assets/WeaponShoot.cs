using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] float damage;
    public float fireRate;
    public float range;
    [SerializeField] int max_bullets;
    [SerializeField] int bullets_count;
    [SerializeField] int bullets_count_all;
    //public float force;
    public ParticleSystem muzzleFlash;
    public Transform bulletSpawn;
    public AudioClip shotSFX;
    public AudioSource _audioSource;
    public GameObject hitEffect;

    private Camera _cam;
    private float nextFire = 0f;
    Animator animator;

    public static Action<GameObject> OnGameObjectClick;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        WeaponPickUp.HandWeaponUse += tryShoot;
        WeaponPickUp.WeaponWasPickUp += set_cam;
        WeaponPickUp.HandWeaponReload += Reload;
    }


    private void set_cam(Camera cam)
    {
        this._cam = cam;
        ScoreViewer.instance.updateWeaponBullets($"{this.bullets_count} / {this.bullets_count_all}");
    }

    void Reload(GameObject gun)
    {
        if(gun == this.gameObject && this.bullets_count_all >= this.max_bullets) 
        {
            animator.SetBool("reload", true);
            this.bullets_count_all-=this.max_bullets;
            this.bullets_count = this.max_bullets;
            ScoreViewer.instance.updateWeaponBullets($"{this.bullets_count} / {this.bullets_count_all}");
            //animator.SetBool("reload", false);
        }
    }
    void tryShoot(GameObject gun)
    {
        //Debug.Log("Bullets: " + this.bullets_count);
        if (Time.time > nextFire && gun == this.gameObject && bullets_count > 0)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
            if(_audioSource != null)
                _audioSource.PlayOneShot(shotSFX);
            if(muzzleFlash != null)
                muzzleFlash.Play();
            Debug.Log(muzzleFlash);

            RaycastHit hit;

            if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, range))
            {
                //Debug.Log("Попал " + hit.collider);
                if (hitEffect != null)
                {
                    GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impact, 0.1f);
                }   
                OnGameObjectClick(hit.transform.gameObject);

            }
            else
            {
                ScoreViewer.instance.RemovePoint();
            }
            this.bullets_count -= 1;
            ScoreViewer.instance.updateWeaponBullets($"{this.bullets_count} / {this.bullets_count_all}");

    }

}
