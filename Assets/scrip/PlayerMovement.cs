using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public  CharacterController controller;
    public Transform playerBody;
    public float speed = 12f;
   

    private void Update()
    {
        float movex = Input.GetAxis("Horizontal");
        float movez = Input.GetAxis("Vertical");

        // คำนวณทิศทางการเดินตามมุมมองของตัวละคร
        Vector3 moveDirection = playerBody.forward * movez + playerBody.right * movex;
        controller.Move(moveDirection * speed * Time.deltaTime);
    }

}
