using System;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;
using System.IO;

namespace UnityEngine.XR.ARFoundation
{
    public class FeaturePoint: MonoBehaviour
    {
        ARPointCloud m_PointCloud;
        Dictionary<ulong, Vector3> m_Points = new Dictionary<ulong, Vector3>();

        void Awake()
        {
            m_PointCloud = GetComponent<ARPointCloud>();
        }

        void OnPointCloudChanged(ARPointCloudUpdatedEventArgs eventArgs)
        {
            if (!m_PointCloud.positions.HasValue)
                return;

            var positions = m_PointCloud.positions.Value;

            if (m_PointCloud.identifiers.HasValue)
            {
                var identifiers = m_PointCloud.identifiers.Value;
                for (int i = 0; i < positions.Length; ++i)
                {
                    m_Points[identifiers[i]] = positions[i];
                }
            }
        }

        void SavePoints()
        {
            string filePath = Application.persistentDataPath + "/points.txt";
            Debug.Log("file save at " + filePath);

            using (StreamWriter sr = File.CreateText(filePath))
            {
                foreach (var kvp in m_Points)
                {
                    Vector3 p = kvp.Value;
                    sr.WriteLine(p.x.ToString("F3") + "," + p.y.ToString("F3") + "," + p.z.ToString("F3"));
                }
            }

        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SavePoints();
            }
        }

        void OnEnable()
        {
            m_PointCloud.updated += OnPointCloudChanged;
        }

        void OnDisable()
        {
            m_PointCloud.updated -= OnPointCloudChanged;
        }
    }
}