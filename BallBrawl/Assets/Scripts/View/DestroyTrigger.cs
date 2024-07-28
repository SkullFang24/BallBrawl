using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class DestroyTrigger : MonoBehaviour
{
    public void OnTriggerExit(Collider other)
    {
        if (!GameManager.IsGamePlaying) return;

        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            LoadSceneAfterDelay( 2 );
        }
        if (other.CompareTag("Enemy"))
        {
            SpawnManager.instance.Enemies.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }

    public async void LoadSceneAfterDelay( float delay )
    {
        await Task.Delay( (int)delay * 1000);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex);

    }
}
