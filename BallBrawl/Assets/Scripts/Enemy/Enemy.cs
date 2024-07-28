using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float currentSpeed;
    private Rigidbody enemyRb;
    private GameObject player;
    public int points = 5;
    

    public void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = PlayerController.instance.gameObject;
    }

    void Update()
    {

        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
    }

    private void OnDestroy()
    {
        PlayerController.AddScore(points);
    }

}

