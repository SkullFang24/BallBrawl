using UnityEngine;

public static class Booster
{
    private static float powerUpStrength = 15;

    internal static void DoBoost( GameObject otherObject, Vector3 playerPosition )
    {
        Rigidbody enemyRigidbody = otherObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = (otherObject.transform.position - playerPosition);
        enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
    }
}
