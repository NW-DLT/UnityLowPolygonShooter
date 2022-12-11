using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public Camera camera;
    public float distance = 2f;
    GameObject currentWeapon;
    bool canPickUp;

    public static Action<GameObject> HandWeaponUse;
    public static Action<Camera> WeaponWasPickUp;
    public static Action<GameObject> HandWeaponReload;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) PickUp();
        if(Input.GetKeyDown(KeyCode.Q)) Drop();
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(currentWeapon!= null)
            {
                HandWeaponReload(currentWeapon);
            }
        }
        //if(Input.GetKeyDown(KeyCode.Mouse0) && currentWeapon != null) HandWeaponUse(currentWeapon);
        if (Input.GetButton("Fire1"))
        {
            if(currentWeapon!= null)
            {
                HandWeaponUse(currentWeapon);
            }
        }
    }

    void PickUp()
    {
        RaycastHit hit;

        if(Physics.Raycast(camera.transform.position, camera.transform.forward,out hit, distance))
        {
            if(hit.transform.tag == "Weapon")
            {
                if (canPickUp) Drop();

                currentWeapon = hit.transform.gameObject;
                currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
                currentWeapon.transform.parent = transform;
                currentWeapon.transform.localPosition = Vector3.zero;
                currentWeapon.transform.localEulerAngles = new Vector3(5f, 180f, 0f);
                canPickUp = true;
                WeaponWasPickUp(camera);
            }
        }
    }

    void Drop()
    {
        if(currentWeapon != null)
        {
            currentWeapon.transform.parent = null;
            currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
            currentWeapon.GetComponent<Rigidbody>().AddForce(1, 1, camera.transform.rotation.y, ForceMode.Impulse);
            canPickUp = false;
            currentWeapon = null;
            ScoreViewer.instance.dropWeapon();
        }
    }
}
