using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    [SerializeField] private Material _lineMaterial;
    [SerializeField] private float _focalLength;
    [SerializeField] private Shape[] _shapesToDraw;
    [SerializeField] private Cube _drawCube;
    [SerializeField] private Sphere _drawSphere;
    [SerializeField] private Cylinder _drawCylinder;
    [SerializeField] private Pyramid _drawPyramid;


    private void OnPostRender()
    {
        GL.PushMatrix();
        GL.Begin(GL.LINES);
        _lineMaterial.SetPass(0);

        GL.Color(_lineMaterial.color);

        DrawShapes();

        GL.End();
        GL.PopMatrix();
    }

    private void DrawShapes()
    {
        foreach (Shape shape in _shapesToDraw)
        {
            var shapeZ = shape.transformPosition.z;
            var actualPerspective = _focalLength / (shapeZ + _focalLength);
            //DrawLine(shape.actualPoints, actualPerspective);
        }

        DrawLine(_drawCube.frontSide);
        DrawLine(_drawCube.backSide);
        DrawLine(_drawCube.leftSide);
        DrawLine(_drawCube.rightSide);

        /*DrawSphere(_drawSphere.vertices);*/
        DrawSphere(_drawSphere.centerPosition, _drawSphere.radius, 30, 15);
        DrawCylinder(_drawCylinder.transformPosition, _drawCylinder.radius, _drawCylinder.height, _drawCylinder.rotation);
        DrawPyramid(_drawPyramid.baseVertices, _drawPyramid.apexVertex);
    }

    private void DrawLine(Vector3[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            var nextPointIndex = (i + 1) % points.Length;
            var point1 = ProjectPointToScreen(points[i]);
            var point2 = ProjectPointToScreen(points[nextPointIndex]);

            GL.Vertex3(point1.x, point1.y, 0);
            GL.Vertex3(point2.x, point2.y, 0);
        }
    }

    private void DrawSphere(Vector3 center, float radius, int longitudeSegments, int latitudeSegments)
    {
        for (int lat = 0; lat < latitudeSegments; lat++)
        {
            for (int lon = 0; lon < longitudeSegments; lon++)
            {
                int currentVert = lat * (longitudeSegments + 1) + lon;
                int nextVert = currentVert + 1;
                int topVert = currentVert + longitudeSegments + 1;
                int nextTopVert = topVert + 1;

                Vector3 point1 = GetPointOnSphere(center, radius, lat, lon, latitudeSegments, longitudeSegments);
                Vector3 point2 = GetPointOnSphere(center, radius, lat, lon + 1, latitudeSegments, longitudeSegments);
                Vector3 point3 = GetPointOnSphere(center, radius, lat + 1, lon, latitudeSegments, longitudeSegments);
                Vector3 point4 = GetPointOnSphere(center, radius, lat + 1, lon + 1, latitudeSegments, longitudeSegments);

                // Draw two triangles for each segment
                DrawTriangle(point1, point3, point2);
                DrawTriangle(point2, point3, point4);
            }
        }
    }

    private Vector3 GetPointOnSphere(Vector3 center, float radius, int lat, int lon, int latitudeSegments, int longitudeSegments)
    {
        float theta = (float)lat / latitudeSegments * Mathf.PI;
        float phi = (float)lon / longitudeSegments * 2 * Mathf.PI;

        float x = center.x + radius * Mathf.Sin(theta) * Mathf.Cos(phi);
        float y = center.y + radius * Mathf.Cos(theta);
        float z = center.z + radius * Mathf.Sin(theta) * Mathf.Sin(phi);

        return new Vector3(x, y, z);
    }

    private void DrawTriangle(Vector3 point1, Vector3 point2, Vector3 point3)
    {
        Vector3 projectedPoint1 = ProjectPointToScreen(point1);
        Vector3 projectedPoint2 = ProjectPointToScreen(point2);
        Vector3 projectedPoint3 = ProjectPointToScreen(point3);

        // Draw the three lines connecting the triangle vertices
        GL.Vertex3(projectedPoint1.x, projectedPoint1.y, 0);
        GL.Vertex3(projectedPoint2.x, projectedPoint2.y, 0);

        GL.Vertex3(projectedPoint2.x, projectedPoint2.y, 0);
        GL.Vertex3(projectedPoint3.x, projectedPoint3.y, 0);

        GL.Vertex3(projectedPoint3.x, projectedPoint3.y, 0);
        GL.Vertex3(projectedPoint1.x, projectedPoint1.y, 0);
    }

    private void DrawCylinder(Vector3 position, float radius, float height, Vector3 rotation)
    {
        Cylinder cylinder = ScriptableObject.CreateInstance<Cylinder>();
        cylinder.radius = radius;
        cylinder.height = height;
        cylinder.transformPosition = position;

        // Apply rotation to cylinder vertices
        Vector3[] rotatedTopCircle = RotateVertices(cylinder.topCircle, rotation);
        Vector3[] rotatedBottomCircle = RotateVertices(cylinder.bottomCircle, rotation);
        Vector3[] rotatedSideVertices = RotateVertices(cylinder.sideVertices, rotation);

        // Draw rotated vertices
        DrawLine(rotatedTopCircle);
        DrawLine(rotatedBottomCircle);
        DrawLine(rotatedSideVertices);
    }

    private Vector3[] RotateVertices(Vector3[] vertices, Vector3 rotation)
    {
        Quaternion quaternionRotation = Quaternion.Euler(rotation);
        Vector3[] rotatedVertices = new Vector3[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            rotatedVertices[i] = quaternionRotation * vertices[i];
        }

        return rotatedVertices;
    }

    private void DrawPyramid(Vector3[] baseVertices, Vector3 apexVertex)
    {
        DrawTriangle(baseVertices[0], baseVertices[1], apexVertex);
        DrawTriangle(baseVertices[1], baseVertices[2], apexVertex);
        DrawTriangle(baseVertices[2], baseVertices[3], apexVertex);
        DrawTriangle(baseVertices[3], baseVertices[0], apexVertex);
    }

    private Vector3 ProjectPointToScreen(Vector3 point)
    {
        return new Vector3(point.x, point.y, 0) * (_focalLength / (point.z + _focalLength));
    }
}

