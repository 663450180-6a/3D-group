using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;   // ความเร็วของกระสุน
    public int damage = 25;     // ความเสียหายจากกระสุน

    void Start()
    {
        // การยิงกระสุนไปข้างหน้า
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ตรวจสอบว่ากระสุนชนกับซอมบี้
        if (collision.gameObject.CompareTag("Zombie"))
        {
            ZombieController zombie = collision.gameObject.GetComponent<ZombieController>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);  // ลดพลังชีวิตของซอมบี้
            }
        }

        // ทำลายกระสุนหลังจากชน
        Destroy(gameObject);
    }
}
