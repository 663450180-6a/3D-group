using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // เพิ่มการใช้งาน UI Text

public class PlayerHealth : MonoBehaviour 
{
    public int maxHealth = 100;  // จำนวนเลือดสูงสุดของผู้เล่น
    private int currentHealth;  // เลือดปัจจุบันของผู้เล่น

    [SerializeField] Text gameOverText;  // ตัวแปรสำหรับข้อความ Game Over UI

    void Start()
    {
        currentHealth = maxHealth;  // ตั้งค่าเลือดเริ่มต้น

        // ตรวจสอบว่า gameOverText ไม่เป็น null
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);  // ซ่อนข้อความ Game Over ตอนเริ่มต้น
        }
        /*else
        {
            Debug.LogError("Game Over Text is not assigned in the Inspector!");
        }*/
    }

    // ฟังก์ชันให้รับความเสียหาย
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // ลด HP ของผู้เล่น

        // แสดงข้อมูล HP ใน Console
        Debug.Log("Player HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();  // ถ้า HP หมดให้เรียกฟังก์ชัน Die
        }
    }

    // ฟังก์ชันสำหรับการตาย (Game Over)
    void Die()
    {
        Debug.Log("Game Over");

        // แสดงข้อความ Game Over
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);  // ทำให้ข้อความ Game Over ปรากฏ
        }

        // หยุดเวลาในเกม
        Time.timeScale = 0f;  // หยุดเวลาในเกม

        // โหลดฉากใหม่ที่เป็นฉากเกมแพ้ หรือเลือกฉากที่ต้องการ
        // SceneManager.LoadScene("GameOverScene");  // ใช้ชื่อของฉากที่ต้องการโหลด
    } 
}
