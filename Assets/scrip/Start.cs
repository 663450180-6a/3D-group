using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;  // ตัวแปรเพื่อเก็บ Prefab ของผู้เล่น
    [SerializeField] private Transform spawnPoint;     // ตัวแปรเพื่อเก็บจุดเกิด

    void Start()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
         playerPrefab.transform.position = spawnPoint.position;
           
        }
        else
        {
            Debug.LogError("PlayerPrefab or SpawnPoint is not assigned in the Inspector!");
        }
    }
}
