using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public bool hasPowerup;
    public GameObject powerupindicator;
    public GameObject projectilePrefab;
    public float JumpForce = 6f;
    public bool isJumping = false;
    public GameObject jumpButton;
    public GameObject fireButton;

    public MMFeedbacks LandingFeedback;
    public MMFeedbacks JumpingFeedback;
    public MMFeedbacks FiringFeedback;
    public MMFeedbacks ShockwaveFeedback;
    public MMFeedbacks BoosterFeedback;

    private PowerUpType currentPowerUpType;
    public static PlayerController instance;

    public bool isPlayerAlive
    {
        get => instance != null;
    }

    internal static bool IsPlayerAlive = true;
    internal ScoreManager _scoreManager { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
    }

    private void Start()
    {
        _scoreManager = GetComponent<ScoreManager>();
        playerRb = GetComponent<Rigidbody>();
        fireButton.SetActive(false);
        jumpButton.SetActive(false);
        powerupindicator.SetActive(false);
        GameManager.SetIsGamePlaying(true);
        IsPlayerAlive = true;
    }

    private void Update()
    {
        powerupindicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        
        if (hasPowerup && currentPowerUpType == PowerUpType.Missiles)
        {
            fireButton.SetActive(true);
        }
        else
        {
            fireButton.SetActive(false);
        }
        
        if (hasPowerup && currentPowerUpType == PowerUpType.ShockWave)
        {
            jumpButton.SetActive(true);
        }
        else
        {
            jumpButton.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasPowerup && currentPowerUpType is PowerUpType.Booster)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Booster.DoBoost(collision.gameObject, transform.position);
                BoosterFeedback?.PlayFeedbacks();
            }
        }
        if (hasPowerup && currentPowerUpType is PowerUpType.ShockWave)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                ShockWave.CreateShockwave(transform.position);
                LandingFeedback?.PlayFeedbacks();
                ShockwaveFeedback?.PlayFeedbacks();
                isJumping = false;
            }

        }
    }

    private void OnDisable()
    {
        IsPlayerAlive = false;
        GameManager.SetIsGamePlaying(false);
    }

    public void Jump()
    {
        if (!hasPowerup || currentPowerUpType is not PowerUpType.ShockWave || isJumping) return;

        playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        JumpingFeedback?.PlayFeedbacks();
        isJumping = true;
    }

    public void ShootProjectile()
    {
        if ( !hasPowerup || currentPowerUpType is not PowerUpType.Missiles ) return;
        
        var enemys = FindObjectsOfType<Enemy>();
        FiringFeedback?.PlayFeedbacks();
        for (int i = 0; i < enemys.Length; i++)
        {
            GameObject bullet = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            bullet.GetComponent<Bullet>().DoShoot(enemys[i].transform);
        }
    }

    public void OnPowerUpPicked( PowerUpType type )
    {
        hasPowerup = true;
        currentPowerUpType = type;
        powerupindicator.gameObject.SetActive(true);
        isJumping = false;

        if (type is PowerUpType.Inversion ) {
            CameraController.instance.isInverted = true;
        }
    }

    public void OnPowerUpDisabled( PowerUpType type ) {

        hasPowerup = false;
        powerupindicator.gameObject.SetActive(false);

        if (type is PowerUpType.Inversion)
        {
            CameraController.instance.isInverted = false;
        }
    }
    
    public static void AddScore( int scoreToAdd )
    {
        if (!IsPlayerAlive || !GameManager.IsGamePlaying ) return;

        instance._scoreManager.AddScore(scoreToAdd);
    }
}