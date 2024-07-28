using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBoss : Enemy
{
    public float attackForce = 6f;
    void Start()
    {
        base.Start();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Vector3 direction = collision.transform.position - transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction.normalized * attackForce, ForceMode.Impulse);
        }
    }
}
