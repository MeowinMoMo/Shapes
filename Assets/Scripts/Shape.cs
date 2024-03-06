using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shape", menuName = "Shape")]
public class Shape : ScriptableObject
{
    [SerializeField]private Vector3[] points;
    public Vector3 transformPosition;
    public Vector3[] actualPoints
    {
        get
        {
            var newPoint = new Vector3[points.Length];
            for( int i = 0; i < newPoint.Length; i++)
            {
                newPoint[i] = transformPosition - points[i];
            }

            return newPoint;
        }
    }
}
