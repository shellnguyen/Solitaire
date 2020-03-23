﻿using UnityEngine;

public sealed class Utilities
{
    //Singleton Init
    private static readonly Utilities instance = new Utilities();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static Utilities()
    {
    }

    private Utilities()
    {
    }

    public static Utilities Instance
    {
        get
        {
            return instance;
        }
    }
    //

    //Functions
    //Position related
    public Vector3 SnappedPosition(Vector3 original)
    {
        Vector3 snapped;
        snapped.x = Mathf.Floor(original.x + 0.5f);
        snapped.y = Mathf.Floor(original.y + 0.5f);
        snapped.z = Mathf.Floor(original.z + 0.5f);

        return snapped;
    }

    public Vector3 GetWorldPosition(Vector3 position)
    {
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    public Vector3 GetWorldPosition2D(Vector3 position)
    {
        Camera cam = Camera.main;
        //Ray2D ray = cam.ScreenPointToRay(position);
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(position), Vector2.zero);
        if (hit)
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    public Transform GetRaycastHitObject(Vector3 position)
    {
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(position);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin,ray.direction * 100, Color.cyan, 30.0f);
            Debug.Log("Hit");
            return hit.transform;
        }

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.cyan, 30.0f);
        Debug.Log("Dit not Hit");
        return null;
    }
    //

    //Draw Rectangle
    //public void DrawRectangle(ref GameObject obj, ref Vector3[] vertices, bool IsWithMesh = false)
    //{
    //    if (IsWithMesh)
    //    {
    //        DrawRectangleWithMesh(ref obj, ref vertices);
    //    }
    //    else
    //    {
    //        DrawRectangle(ref obj, ref vertices);
    //    }
    //}

    //private void DrawRectangle(ref GameObject obj, ref Vector3[] vertices)
    //{
    //    LineRenderer rend = obj.GetComponent<LineRenderer>();

    //    rend.positionCount = 4;
    //    rend.SetPositions(vertices);
    //}

    //private void DrawRectangleWithMesh(ref GameObject obj, ref Vector3[] vertices)
    //{
    //    MeshFilter filter = obj.GetComponent<MeshFilter>();

    //    Vector2[] v2Lines = {
    //                            new Vector2(vertices[0].x, vertices[0].z),
    //                            new Vector2(vertices[1].x, vertices[1].z),
    //                            new Vector2(vertices[2].x, vertices[2].z),
    //                            new Vector2(vertices[3].x, vertices[3].z)
    //                        };

    //    Triangulator tr = new Triangulator(v2Lines);
    //    int[] indices = tr.Triangulate();

    //    Mesh msh = new Mesh();
    //    msh.vertices = vertices;
    //    msh.triangles = indices;
    //    //msh.colors = colors;

    //    msh.RecalculateNormals();
    //    msh.RecalculateBounds();
    //    msh.RecalculateTangents();

    //    filter.mesh = msh;
    //}
    //
}
