using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab ของกระสุน
    public Transform bulletSpawnPoint; // จุดที่กระสุนออกจากปืน
    public float bulletForce = 50; // ความแรงของกระสุน

    void Start()
    {
        // เพิ่มการตรวจสอบในฟังก์ชันเริ่มต้น (ถ้าจำเป็น)
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // กดปุ่มยิง
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // ตรวจสอบว่า bulletSpawnPoint และ bulletPrefab ไม่เป็น null ก่อนที่จะแInstantiating
        if (bulletSpawnPoint != null && bulletPrefab != null)
        {
            // สร้างวัตถุใหม่
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            // ดึงคอมโพเนนท์ Rigidbody
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            // forward up down left right back ForceMode.Impulse เพิ่มความเร็วให้กับกระสุน
            rb.AddForce(bulletSpawnPoint.forward * bulletForce, ForceMode.Impulse); // ยิงกระสุนออกไป
        }
        else
        {
            Debug.LogError("Bullet Spawn Point หรือ Bullet Prefab หายไป.");
        }
    }
}
