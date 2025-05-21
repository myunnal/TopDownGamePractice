using UnityEngine;

public class NormalAttack : IAttackStrategy
{
    public float Damage => 10f;
    public ParticleSystem AttackVFX { get; }
    public AudioClip AttackSound { get; }

    public NormalAttack(ParticleSystem vfx, AudioClip sound)
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