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
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        //fpsCam.GetComponent<SimpleCameraShakeCinemachine>().Shake();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            HealthManager hm = hit.transform.GetComponent<HealthManager>();
            if (hm)
            {
                hm.TakeDamage(damage);
            }
        }
    }
}