using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
   [Header("Stats")]
    public float health = 100f;
    public float moveSpeed = 2f;
    public float attackDamage = 10f;
    public float attackCooldown = 2f;
    public float detectionRange = 5f;

    [Header("Effects")]
    public ParticleSystem hitVFX;
    public AudioClip hitSound;

    [Header("References")]
    public Slider healthSlider;
    public Transform attackPoint;
    public LayerMask playerLayer;

    private Transform _player;
    private float _attackTimer;
    private bool _isDead;
    private AudioSource _audioSource;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        healthSlider.maxValue = health;
        healthSlider.value = health;
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_isDead) return;

        // Face player
        if (_player.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

        // Movement
        float distance = Vector2.Distance(transform.position, _player.position);
        if (distance <= detectionRange && distance > 1f)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                _player.position,
                moveSpeed * Time.deltaTime
            );
        }

        // Attack
        _attackTimer += Time.deltaTime;
        if (distance <= 1.5f && _attackTimer >= attackCooldown)
        {
            Attack();
            _attackTimer = 0f;
        }
    }

    void Attack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(
            attackPoint.position, 
            0.8f, 
            playerLayer
        );

        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(float damage)
    {
        // Play hit effects
        if (hitVFX != null) hitVFX.Play();
        if (hitSound != null) _audioSource.PlayOneShot(hitSound);
        
        health -= damage;
        healthSlider.value = health;

        if (health <= 0) Die();
    }

    void Die()
    {
        _isDead = true;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.5f);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, 0.8f);
    }
}