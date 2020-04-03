using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float health=100f;
    [SerializeField] Behaviour[] switchOnDeath;
    [SerializeField] bool dead=false;
    public void TakeDamage(float amount)
    {
        if (dead)
            return;
        health -= amount;
        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    void Die()
    {
        dead = true;
        Destroy(gameObject, 4f);
        try
        {
            GetComponent<Animator>().SetTrigger("Dead");
        }
        catch
        {
            Debug.Log("No Animator found");
        }

        foreach (Behaviour b in switchOnDeath)
            b.enabled = !b.enabled;
        
    }
    
}
