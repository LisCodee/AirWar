using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float speed = 10;
    public float roundSpeed = 5;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {

            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(0, -roundSpeed* Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(0, roundSpeed * Time.deltaTime, 0);
        }
    }
}
