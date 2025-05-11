using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VolcanoJetting))]
public class VolcanoSFX : MonoBehaviour
{
    private VolcanoJetting jetting;
    [SerializeField] private ParticleSystem eruptionParticles;

    void Awake()
    {
        if (!TryGetComponent(out jetting)) {
            throw new MissingComponentException("VolcanoJetting component is missing on " + gameObject.name);
        }

        if (eruptionParticles == null) {
            Debug.LogWarning("Eruption particle system not assigned on " + gameObject.name);
        }
    }

    void LateUpdate()
    {
        if (eruptionParticles == null) return;

        if (jetting.State == VolcanoState.Jetting)
        {
            if (!eruptionParticles.isPlaying)
                eruptionParticles.Play();
        } else {
            if (eruptionParticles.isPlaying)
                eruptionParticles.Stop();
        }
    }
}
