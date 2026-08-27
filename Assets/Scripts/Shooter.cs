using UnityEngine;
using UnityEngine.InputSystem;

public class BallShooter : MonoBehaviour
{
    [Header("References")]
    public GameObject ballPrefab;
    public Transform shootPoint;

    [Header("Shooter Settings")]
    [Tooltip("Bazowa siła wyrzutu kulki.")]
    [SerializeField]
    private float launchForce;

    [Tooltip("Shooter angle deg (0 = horizontal, 90 = vertical).")]
    [Range(0f, 85f)]
    [SerializeField]
    private float launchAngle;

    [Header("Force dispersion")]
    [Range(0f, 0.2f)]
    [SerializeField]
    private float forceRandomness;

    [Tooltip("Shooter spread angle")]
    [Range(0f, 10f)]
    [SerializeField]
    private float spreadAngle;

    [Header("Cooldown")]
    [SerializeField]
    private float cooldown;

    private float lastShootTime;

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.spaceKey.wasPressedThisFrame && Time.time >= lastShootTime + cooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    void Shoot()
    {
        if (ballPrefab == null || shootPoint == null) return;

        // 1. Obliczenie bazowego wektora lotu z uwzględnieniem kąta podniesienia lufy
        // Obracamy lokalną oś 'forward' obiektu ShootPoint w górę wokół osi 'right'
        Quaternion angleOffset = Quaternion.AngleAxis(-launchAngle, shootPoint.right);
        Vector3 baseDirection = angleOffset * shootPoint.forward;

        // 2. Generowanie losowego rozrzutu kątowego (Simulated Dispersion)
        float randomPitch = Random.Range(-spreadAngle, spreadAngle);
        float randomYaw = Random.Range(-spreadAngle, spreadAngle);
        Quaternion spreadRotation = Quaternion.Euler(randomPitch, randomYaw, 0f);

        // Aplikujemy rozrzut do wyliczonego wektora
        Vector3 finalDirection = Quaternion.LookRotation(baseDirection) * (spreadRotation * Vector3.forward);

        // 3. Generowanie losowej wariacji siły (Simulated Flywheel Speed Fluctuation)
        float forceMultiplier = 1f + Random.Range(-forceRandomness, forceRandomness);
        float finalForce = launchForce * forceMultiplier;

        // 4. Wygenerowanie i wystrzelenie kulki
        GameObject newBall = Instantiate(ballPrefab, shootPoint.position, Quaternion.LookRotation(finalDirection));
        Rigidbody rb = newBall.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(finalDirection * finalForce, ForceMode.Impulse);
        }
    }
}