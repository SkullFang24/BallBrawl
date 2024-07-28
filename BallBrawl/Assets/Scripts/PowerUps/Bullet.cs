using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float impactForce = 10f;
    public float homingRange = 10f;
    public float rotationSpeed = 5f;
    public float maxLifespan = 5f;
    public float moveSpeed = 10;

    private Rigidbody bulletRigidbody;
    private Transform target;
    private float currentLifespan;
    private bool hasFired = false;

    private void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (hasFired)
        {
            currentLifespan += Time.deltaTime;

            if (currentLifespan >= maxLifespan)
            {
                Destroy(gameObject);
                hasFired = false;
            }

            if (target != null)
            {
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                transform.position += moveSpeed * Time.deltaTime * directionToTarget;
                transform.LookAt(target);

            }
            else {
                Destroy(gameObject);
                
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = target.GetComponent<Rigidbody>();
            Vector3 hitDirection = (target.position - transform.position).normalized;

            enemyRigidbody.AddForce(hitDirection * impactForce, ForceMode.Impulse);
            hasFired = false;
            Destroy(gameObject);

        }
    }


    public void DoShoot(Transform enemy)
    {
        target = enemy;
        hasFired = true;
    }
}