using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cube", menuName = "Cube")]
public class Cube : ScriptableObject
{
    public float length;
    public float height;
    public float width;

    public Vector3 transformPosition;

    public Vector3[] frontSide
    {
        get
        {
            return new Vector3[]
            {
                new Vector3(transformPosition.x +(width/2), transformPosition.y +(height/2), transformPosition.z + (length/2)),
                new Vector3(transformPosition.x +(width/2), transformPosition.y -(height/2), transformPosition.z + (length/2)),
                new Vector3(transformPosition.x -(width/2), transformPosition.y -(height/2), transformPosition.z + (length/2)),
                new Vector3(transformPosition.x -(width/2), transformPosition.y +(height/2), transformPosition.z + (length/2))
            };
        }
    }

    public Vector3[] backSide
    {
        get
        {
            return new Vector3[]
            {
                new Vector3(transformPosition.x +(width/2), transformPosition.y +(height/2), transformPosition.z - (length/2)),
                new Vector3(transformPosition.x +(width/2), transformPosition.y -(height/2), transformPosition.z - (length/2)),
                new Vector3(transformPosition.x -(width/2), transformPosition.y -(height/2), transformPosition.z - (length/2)),
                new Vector3(transformPosition.x -(width/2), transformPosition.y +(height/2), transformPosition.z - (length/2))
            };
        }
    }

    public Vector3[] leftSide
    {
        get
        {
            return new Vector3[]
            {
                frontSide[0], frontSide[1], backSide[1], backSide[0]
            };
        }
    }

    public Vector3[] rightSide
    {
        get
        {
            return new Vector3[]
            {
                frontSide[2], frontSide[3], backSide[3], backSide[2]
            };
        }
    }
}
