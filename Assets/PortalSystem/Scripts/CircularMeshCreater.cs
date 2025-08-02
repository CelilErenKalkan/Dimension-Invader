using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CircularMeshCreater : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private int segments = 32;
    [SerializeField] private GameObject frameObject;


    void Start()
    {
        GetComponent<MeshFilter>().mesh = GenerateCircle(radius, segments);

        transform.position = frameObject.transform.position;
       // transform.localScale.z = frameObject.transform.rotation;
        //transform.localScale = Vector3.one;
        // Parent'ýn Renderer merkezine yerleþ
        //if (transform.parent != null)
        //{
        //    Renderer parentRenderer = transform.parent.GetComponentInChildren<Renderer>();
        //    if (parentRenderer != null)
        //    {
        //        Vector3 center = parentRenderer.bounds.center;
        //        transform.position = new Vector3(center.x, parentRenderer.bounds.min.y + 0.01f, center.z);
        //    }
        //    else
        //    {
        //        // Fallback: parent pozisyonu

        //    }

        //    // Scale'ý sýfýrla, çünkü sadece pozisyon ayarlansýn istiyoruz
        //    
        //}
    }

    Mesh GenerateCircle(float radius, int segments)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[segments + 1];
        int[] triangles = new int[segments * 3];

        vertices[0] = Vector3.zero; // Merkez
        float angleStep = 360f / segments;

        for (int i = 1; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * angleStep * i;
            vertices[i] = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
        }

        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = (i + 2 > segments) ? 1 : i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }
}
