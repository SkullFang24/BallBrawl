using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShockWave
{
    private static float shockWaveRadius = 10;
    private static float shockWaveForce = 25;

    internal static void CreateShockwave( Vector3 playerPosition )
    {
        Collider[] colliders = Physics.OverlapSphere(playerPosition, shockWaveRadius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                Vector3 direction = col.transform.position - playerPosition;
                col.GetComponent<Rigidbody>().AddForce(direction.normalized * shockWaveForce, ForceMode.Impulse);
            }
        }
    }
}
