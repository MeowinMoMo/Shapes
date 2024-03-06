using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cylinder", menuName = "Cylinder")]
public class Cylinder : ScriptableObject
{
    public float radius;
    public float height;
    public Vector3 transformPosition;
    public Vector3 rotation; // New rotation property

    // Get the vertices of the top circle of the cylinder
    public Vector3[] topCircle
    {
        get
        {
            List<Vector3> vertices = new List<Vector3>();

            for (int i = 0; i < 360; i += 10)
            {
                float angle = i * Mathf.Deg2Rad;
                float x = transformPosition.x + radius * Mathf.Cos(angle);
                float y = transformPosition.y + (height / 2);
                float z = transformPosition.z + radius * Mathf.Sin(angle);

                vertices.Add(new Vector3(x, y, z));
            }

            return vertices.ToArray();
        }
    }

    // Get the vertices of the bottom circle of the cylinder
    public Vector3[] bottomCircle
    {
        get
        {
            List<Vector3> vertices = new List<Vector3>();

            for (int i = 0; i < 360; i += 10)
            {
                float angle = i * Mathf.Deg2Rad;
                float x = transformPosition.x + radius * Mathf.Cos(angle);
                float y = transformPosition.y - (height / 2);
                float z = transformPosition.z + radius * Mathf.Sin(angle);

                vertices.Add(new Vector3(x, y, z));
            }

            return vertices.ToArray();
        }
    }

    // Get the vertices for the sides of the cylinder
    public Vector3[] sideVertices
    {
        get
        {
            List<Vector3> vertices = new List<Vector3>();

            for (int i = 0; i < topCircle.Length; i++)
            {
                vertices.Add(topCircle[i]);
                vertices.Add(bottomCircle[i]);
            }

            return vertices.ToArray();
        }
    }
}
