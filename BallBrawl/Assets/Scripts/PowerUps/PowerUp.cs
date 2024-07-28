using UnityEngine;
using System.Threading.Tasks;

public enum PowerUpType
{
    None,
    Booster,
    ShockWave,
    Inversion,
    Missiles
}

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public float destroyDelay;
    public float disableTimer;

    private bool isAlive = true;

    private void Start()
    {
        DestroyPowerupAsynchronously();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();

        if( player != null )
        {
            player.OnPowerUpPicked(type);
            SpawnManager.instance.PowerUps.Remove(gameObject);
            DisablePowerUp();
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {

        isAlive = false;
    }

    private async void DestroyPowerupAsynchronously()
    {
        await Task.Delay( (int)destroyDelay * 1000 );

        if (!isAlive) return;

        SpawnManager.instance.PowerUps.Remove(gameObject);
        Destroy(gameObject);
    }

    private async void DisablePowerUp()
    {
        await Task.Delay((int)disableTimer * 1000);
        PlayerController.instance.OnPowerUpDisabled(type);
    }
}
