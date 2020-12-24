using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataVisualizer : MonoBehaviour
{
    ParticleSystem m_ParticleSystem;
    ParticleSystem.Particle[] m_Particles;

    public TextAsset data;
    List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();

        StringReader reader = new StringReader(data.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            string[] str = line.Split(',');
            float x = float.Parse(str[0]);
            float y = float.Parse(str[1]);
            float z = float.Parse(str[2]);
            positions.Add(new Vector3(x, y, z));
        }

        int numParticles = positions.Count;

        m_Particles = new ParticleSystem.Particle[numParticles];
        for (int i = 0; i < positions.Count; i++)
        {
            m_Particles[i].startColor = m_ParticleSystem.main.startColor.color;
            m_Particles[i].startSize = m_ParticleSystem.main.startSize.constant;
            m_Particles[i].position = positions[i];
            m_Particles[i].remainingLifetime = 1f;
        }

        m_ParticleSystem.SetParticles(m_Particles, numParticles);

    }
}
