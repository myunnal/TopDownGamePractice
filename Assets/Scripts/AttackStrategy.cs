using UnityEngine;

public interface IAttackStrategy
{
    float Damage { get; }
    ParticleSystem AttackVFX { get; }
    AudioClip AttackSound { get; }
    void PerformAttack(Transform playerTransform);
}