using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    // Direction from the Zombie to the Player
    Vector3 direction;

    Transform playerTransform;
    HealthManager playerHealthManager;

    bool dealDamage;

    [SerializeField] float damage;

    Rigidbody rb;

    Animator animator;

    public float speed = 5f;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Look for the Player in the scene
        playerTransform = GameObject.Find("Player").transform;
        playerHealthManager = playerTransform.GetComponent<HealthManager>();
    }
    // Update is called once per frame
    void Update()
    {
        // If there is no player don't move
        if (!playerTransform)
        {
            direction = Vector3.zero;
            return;
        }

        // Calculate the direction from the Zombie to the Player
        direction = playerTransform.position - transform.position;

        // Make sure the zombie only follows on the XZ plane
        direction.y = 0;

        // Rotate the Zombie
        rb.rotation = Quaternion.LookRotation(direction);

        // Make sure it doesn't spin when hitting objects
        rb.angularVelocity = Vector3.zero;

        // Move the Zombie
        rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);

        // Animate the Zombie
        animator.SetFloat("MoveSpeed", 1);

        if (playerHealthManager && dealDamage)
            playerHealthManager.TakeDamage(damage * Time.deltaTime);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
            dealDamage = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
            dealDamage = false;
    }
}
