using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sphere", menuName = "Sphere")]
public class Sphere : ScriptableObject
{
    public float radius;
    public Vector3 centerPosition;

    public Vector3[] vertices
    {
        get
        {
            List<Vector3> verts = new List<Vector3>();

            int numLongitudes = 30; // Adjust this for smoother or rougher sphere
            int numLatitudes = 15; // Adjust this for smoother or rougher sphere

            for (int lat = 0; lat <= numLatitudes; lat++)
            {
                float theta = lat * Mathf.PI / numLatitudes;
                float sinTheta = Mathf.Sin(theta);
                float cosTheta = Mathf.Cos(theta);

                for (int lon = 0; lon <= numLongitudes; lon++)
                {
                    float phi = lon * 2 * Mathf.PI / numLongitudes;
                    float sinPhi = Mathf.Sin(phi);
                    float cosPhi = Mathf.Cos(phi);

                    float x = centerPosition.x + radius * sinTheta * cosPhi;
                    float y = centerPosition.y + radius * sinTheta * sinPhi;
                    float z = centerPosition.z + radius * cosTheta;

                    verts.Add(new Vector3(x, y, z));
                }
            }

            return verts.ToArray();
        }
    }
}
