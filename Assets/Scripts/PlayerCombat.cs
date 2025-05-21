using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    [Header("UI")]
    public Slider healthSlider;
    public TMP_Text attackTypeText;

    [Header("Attack Settings")]
    public ParticleSystem normalAttackVFX;
    public ParticleSystem waterAttackVFX;
    public AudioClip normalAttackSound;
    public AudioClip waterAttackSound;

    [Header("Stats")]
    public float health = 100f;

    private IAttackStrategy currentAttack;
    private bool isAttacking;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetAttackStrategy(new NormalAttack(normalAttackVFX, normalAttackSound));
    }

    void Update()
    {
        HandleAttackInput();
        UpdateUI();
    }

    void HandleAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetAttackStrategy(new NormalAttack(normalAttackVFX, normalAttackSound));
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetAttackStrategy(new WaterAttack(waterAttackVFX, waterAttackSound));

        if (Input.GetMouseButtonDown(0) && !isAttacking)
            StartCoroutine(AttackRoutine());
    }

    void SetAttackStrategy(IAttackStrategy strategy)
    {
        currentAttack = strategy;
        attackTypeText.text = $"Attack: {strategy.GetType().Name}";
    }

    System.Collections.IEnumerator AttackRoutine()
    {
        isAttacking = true;
        
        // Play VFX
        currentAttack.PerformAttack(transform);
        
        // Play Sound
        if (currentAttack.AttackSound != null)
            audioSource.PlayOneShot(currentAttack.AttackSound);

        // Damage logic
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent<Enemy>(out Enemy enemyComponent))
                enemyComponent.TakeDamage(currentAttack.Damage);
        }

        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health;
        
        if (health <= 0)
        {
            // Handle player death
            Debug.Log("Player Died!");
        }
    }

    void UpdateUI() => healthSlider.value = health / 100f;
}