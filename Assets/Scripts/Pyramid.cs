using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pyramid", menuName = "Pyramid")]
public class Pyramid : ScriptableObject
{
    public float baseLength;
    public float height;

    public Vector3 transformPosition;

    public Vector3[] baseVertices
    {
        get
        {
            return new Vector3[]
            {
                new Vector3(transformPosition.x + (baseLength / 2), transformPosition.y, transformPosition.z + (baseLength / 2)),
                new Vector3(transformPosition.x + (baseLength / 2), transformPosition.y, transformPosition.z - (baseLength / 2)),
                new Vector3(transformPosition.x - (baseLength / 2), transformPosition.y, transformPosition.z - (baseLength / 2)),
                new Vector3(transformPosition.x - (baseLength / 2), transformPosition.y, transformPosition.z + (baseLength / 2))
            };
        }
    }

    public Vector3 apexVertex
    {
        get
        {
            return new Vector3(transformPosition.x, transformPosition.y + height, transformPosition.z);
        }
    }

    public Vector3[] triangle1
    {
        get
        {
            return new Vector3[]
            {
                baseVertices[0], baseVertices[1], apexVertex
            };
        }
    }

    public Vector3[] triangle2
    {
        get
        {
            return new Vector3[]
            {
                baseVertices[1], baseVertices[2], apexVertex
            };
        }
    }

    public Vector3[] triangle3
    {
        get
        {
            return new Vector3[]
            {
                baseVertices[2], baseVertices[3], apexVertex
            };
        }
    }

    public Vector3[] triangle4
    {
        get
        {
            return new Vector3[]
            {
                baseVertices[3], baseVertices[0], apexVertex
            };
        }
    }
}
