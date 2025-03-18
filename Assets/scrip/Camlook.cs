using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camlook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ล็อกเมาส์ให้อยู่ในหน้าจอ
    }

    void Update()
    {
        // รับค่าการเคลื่อนที่ของเมาส์
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // หมุนตัวละครไปทางซ้าย-ขวา
        playerBody.Rotate(Vector3.up * mouseX);

        // คำนวณการหมุนขึ้น-ลง
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // จำกัดการหมุนกล้องไม่ให้เกิน 90 องศา

        // นำค่าการหมุนไปใช้กับกล้อง
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
