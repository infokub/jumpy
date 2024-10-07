using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxHeight;
    [SerializeField] float maxLength;

    float vspeed;
    float gravity;
    bool isFloored;
    Rigidbody rb;
    Animator animCtrl;


    // Start is called before the first frame update
    void Start()
    {
        vspeed = 0;
        isFloored = true;
        rb = GetComponent<Rigidbody>();

        animCtrl = GetComponentInChildren<Animator>();
        animCtrl.SetBool("running", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFloored && Input.GetKey(KeyCode.Space))
        {
            vspeed = JumpDefinition.InitSpeed(maxHeight, maxLength, Runner.instance.speed);
            gravity = JumpDefinition.Gravity(maxHeight, maxLength, Runner.instance.speed);
            isFloored = false;
            animCtrl.SetTrigger("jump");
        }
    }

    private void FixedUpdate()
    {
        if (isFloored)
            return;

        rb.MovePosition(transform.position + Vector3.up * vspeed * Time.fixedDeltaTime);
        vspeed += gravity * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (vspeed < 0)
        {
            vspeed = 0;
            isFloored = true;
            animCtrl.SetTrigger("grounded");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Hit"))
        {
            if (!Runner.instance.Hit())
            {
                enabled = false;
                animCtrl.SetBool("running", false);
            }
            else
            {
                animCtrl.SetTrigger("contact");
            }
        }
    }
}
