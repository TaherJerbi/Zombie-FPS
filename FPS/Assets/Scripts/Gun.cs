using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float force = 1f;
    public float fireRate = 20;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    float nextTimeToFire = 0f;
    // Update is called once per frame
    private void Awake()
    {
        fpsCam = Camera.main;
    }
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if(Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
        else if(muzzleFlash.isPlaying)
            muzzleFlash.Stop();
    }

    void Shoot()
    {
        if (muzzleFlash.isStopped)
        {
            muzzleFlash.Play();
        }
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit,range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target)
            {
                target.TakeDamage(damage);
                target.GetComponent<Rigidbody>().AddForce(-hit.normal * force,ForceMode.Impulse);
            }
        }
    }
}
