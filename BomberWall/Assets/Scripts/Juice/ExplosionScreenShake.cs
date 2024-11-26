using Cinemachine;
using UnityEngine;

public class ExplosionScreenShake : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Bomb>().BombExplodeNotify += OnExplosionScreenShake;
    }

    public void OnExplosionScreenShake()
    {
        GetComponent<CinemachineImpulseSource>().GenerateImpulseWithForce(1);
    }
}