using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    Vector3 direction;
    Transform playerTransform;
    Target playerTarget;
    Rigidbody rb;
    Animator animator;

    bool dealDamage = false;
    public float speed=5f;
    public float damage=10f;
    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        playerTarget = playerTransform.GetComponent<Target>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!playerTransform)
        {
            direction = Vector3.zero;
            return;
        }
        direction = playerTransform.position - transform.position;
        direction.y = 0;


        rb.rotation = Quaternion.LookRotation(direction);
        rb.angularVelocity = Vector3.zero;
        rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);

        if (dealDamage && playerTarget)
            playerTarget.TakeDamage(damage * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag == "Player")
            dealDamage = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
            dealDamage = false;
    }
}
