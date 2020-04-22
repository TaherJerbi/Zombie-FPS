using UnityEngine;

public class FPSController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] CapsuleCollider col;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float gravity = -9.8f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = Vector3.up * gravity;
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");


        Vector3 direction = transform.right * x + transform.forward * y;


        rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);

        rb.angularVelocity = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Space) && groundCheck())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool groundCheck()
    {
        return Physics.CheckCapsule(
            col.bounds.center,
            new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z),
            col.radius * 0.9f,
            groundLayer
            );
    }
}
