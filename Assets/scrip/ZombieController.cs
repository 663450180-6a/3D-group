using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;  // เพิ่มการใช้งาน SceneManager

public class ZombieController : MonoBehaviour
{
    private int health = 100;
    private float attackRange =  10f;
    private int attackDamage = 50;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isWalking = false;

    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Attack = Animator.StringToHash("Attack");

    /*[SerializeField] PlayerHealth playerhealth;*/
    public PlayerHealth playerhealth;
    [SerializeField] Transform player;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.updateRotation = false; // ปิดการหมุนอัตโนมัติของ NavMeshAgent
        FindPlayer();
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);
        /*Debug.Log(distance);*/

        if (distance > attackRange)
        {
            // ซอมบี้ยังอยู่ห่างจากผู้เล่น จึงเดินไปหาผู้เล่น
            MoveTowardsPlayer();
        }
        else
        {
            // ซอมบี้อยู่ในระยะโจมตี
            StopMoving();
            AttackPlayer();
        }

        // อัปเดตการเดินของซอมบี้
        animator.SetBool(Walking, isWalking);
    }

    void FindPlayer()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObject != null)
        {
            player = playerGameObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }
    }

    void MoveTowardsPlayer()
    {
        if (navMeshAgent != null && player != null)
        {
            navMeshAgent.SetDestination(player.position);
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = 3f; // ปรับความเร็วซอมบี้
            isWalking = true;

            // บังคับให้ซอมบี้หันหน้าไปทางผู้เล่น (หมุนเฉพาะแกน Y)
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // ล็อคค่า Y ไม่ให้เงย/ก้ม
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void StopMoving()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.isStopped = true;
            isWalking = false;
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Zombie attacking the player!");  // debug เพื่อเช็คว่าเข้าสู่ฟังก์ชันนี้
        /*animator.SetTrigger(Attack); // เรียก Trigger การโจมตี*/

        // ลดพลังชีวิตของผู้เล่น (ตรวจสอบว่า `playerhealth` ไม่เป็น null ก่อน)
        if (playerhealth != null)
        {
            playerhealth.TakeDamage(attackDamage);
            Debug.Log("Player's health reduced!");
        }
    }

    // ฟังก์ชันสำหรับรับความเสียหายจากกระสุน
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Zombie is dead!");
        animator.SetTrigger("Die"); // เรียก Trigger การตายของอนิเมชั่น
        navMeshAgent.isStopped = true; // หยุดการเคลื่อนไหวของซอมบี้
        Destroy(gameObject, 2f); // ลบซอมบี้หลังจาก 2 วินาที (เวลาให้อนิเมชั่นตายทำงาน)
    }

    // ฟังก์ชันสำหรับตรวจจับการชน
    private void OnCollisionEnter(Collision collision)
    {
        // ตรวจสอบว่าเป็นการชนกับผู้เล่น
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Zombie collided with Player!");  // Debug เมื่อชนกับผู้เล่น
            AttackPlayer();  // เรียกใช้ฟังก์ชันโจมตี

            // โหลดฉากใหม่เมื่อซอมบี้ชนกับผู้เล่น
            SceneManager.LoadScene("YourSceneName");  // ใช้ชื่อของฉากที่คุณต้องการโหลด
        }

        // ตรวจสอบว่าเป็นการชนกับกระสุน
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);  // ลดพลังชีวิตจากกระสุน
                Debug.Log("Zombie took damage from bullet!");
            }
            Destroy(collision.gameObject);  // ทำลายกระสุนหลังจากชน
        }
    }
    /////
}

