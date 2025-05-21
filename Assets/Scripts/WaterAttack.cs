using UnityEngine;

public class WaterAttack : IAttackStrategy
{
    public float Damage => 20f;
    public ParticleSystem AttackVFX { get; }
    public AudioClip AttackSound { get; }

    public WaterAttack(ParticleSystem vfx, AudioClip sound)
    {
        AttackVFX = vfx;
        AttackSound = sound;
    }

    public void PerformAttack(Transform playerTransform)
    {
        if (AttackVFX != null)
        {
            ParticleSystem vfxInstance = GameObject.Instantiate(
                AttackVFX,
                playerTransform.position + playerTransform.forward,
                Quaternion.identity
            );
            vfxInstance.Play();
            GameObject.Destroy(vfxInstance.gameObject, vfxInstance.main.duration);
        }
    }
}