using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Animation [] girl_anim;
    private Gun gun;
    public GameObject firePoint;
    // Start is called before the first frame update
    void Start()
    {
        firePoint = GameObject.FindGameObjectWithTag("FirePoint");
        girl_anim = this.GetComponentsInChildren<Animation>();
        Debug.Log(girl_anim.Length);
        gun = this.GetComponentInChildren<Gun>();
    }
    public float speed = 10;
    public float roundSpeed = 5;
    // Update is called once per frame
    void Update()
    {
        Movement();
        Fire();
    }
    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            girl_anim[0].Play();
            girl_anim[1].Play();
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(-Vector3.forward * speed * Time.deltaTime);
            girl_anim[0].Play();
            girl_anim[1].Play();
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(0, -roundSpeed * Time.deltaTime, 0);
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(0, roundSpeed * Time.deltaTime, 0);
        }
    }
    private void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Transform fp = firePoint.transform;
            gun.Firing(fp.forward);
            Debug.Log("开火");
        }
        if (Input.GetMouseButtonDown(1))
        {
            gun.UpdateAmmo();
        }
    }
}
