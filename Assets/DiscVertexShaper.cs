﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class DiscVertexShaper : MonoBehaviour
{
    Vector3[] vertices;
    Vector2[] uv;
    int[] polys;
    [SerializeField] int detail;
    [SerializeField] float length;
    [SerializeField] float totalAngle = 360;
    float initialAngle;
    float currentAngle;
    float anglePerPoly;
    Mesh m;
    Vector3 prevData;
    public DiscVertexShaper(float _rotation)
    {
        initialAngle = _rotation;
        currentAngle = initialAngle;
    }
    void Start()
    {
        //create mesh data
         m = new Mesh();
        GetComponent<MeshFilter>().mesh = m;
        
        //start mesh generation
        StartGen();
        
        UpdateMesh();
    }
    void StartGen()//creates the disc
    {
        //top face
        currentAngle = 0;
        anglePerPoly = totalAngle / detail;
        vertices = new Vector3[detail*2+1+1+1+1];
        uv = new Vector2[vertices.Length];

        polys = new int[((detail * 3)*4 + (4 * 3*2))];
        vertices[0] = Vector2.zero;
        int vIndex = 1;
        int pIndex = 0;
        for (int i = 0; i <= detail; i++)//for every triangle in the arc, loop around the angle till the final angle, the higher the detail variable, the higher the poly count, the smoother the circle
        {
            Vector3 vertex = Vector3.zero + GetVectorFromAngle(currentAngle) * length;
            vertices[vIndex] = vertex;
            if (i > 0)
            {
                polys[pIndex] = 0;
                polys[pIndex + 1] = vIndex - 1;
                polys[pIndex + 2] = vIndex;
                pIndex += 3;
            }
            vIndex++;
            currentAngle -= anglePerPoly;
        }
        //bottom face
        vertices[detail + 2] = new Vector3(vertices[0].x, vertices[0].y - 3, vertices[0].z);
        vIndex = detail +3;
        pIndex = 0;
        currentAngle = 180;
        int[] temp= new int[polys.Length/4];
        for (int i = 0; i <= detail; i++)//for every triangle in the arc, loop around the angle till the final angle, the higher the detail variable, the higher the poly count, the smoother the circle
        {
            Vector3 vertex = Vector3.zero + GetVectorFromAngle(currentAngle+180) * length;
            vertices[vIndex] = vertex + Vector3.down * ConstantValues.DISC_THICKNESS;

            if (i > 0)
            {
                temp[pIndex] = detail + 2;
                temp[pIndex + 1] = vIndex - 1;
                temp[pIndex + 2] = vIndex;
                pIndex += 3;
                
            }

            
            vIndex++;
            
            currentAngle -= anglePerPoly;
        }
        temp.Reverse().ToArray().CopyTo(polys, polys.Length / 4); // invert the face
        //middle
        vIndex = 1;
        pIndex = 0;
        temp = new int[(detail*3)*2+(4*3)];
        for(int i = 0; i <= detail; i++)
        {
            print(pIndex);
            print(vIndex);
            if (i > 0)
            {
                temp[pIndex] = vIndex - 1+1;
                temp[pIndex + 1] = vIndex + detail + 1;
                temp[pIndex + 2] = vIndex + detail + 1 + 1;

                temp[pIndex + 3] = vIndex ;
                temp[pIndex + 4] = vIndex - 1 ;
                temp[pIndex + 5] = vIndex + detail + 1 ;

                
                

                pIndex += 6;
            }
            
            
            vIndex++;
        }
        //manually set origin sides
        temp[pIndex] = detail * 2 + 3;
        temp[pIndex +1] = detail + 2; 
        temp[pIndex+2] = 0;

        temp[pIndex + 3] = 0;
        temp[pIndex + 4] = detail+1;
        temp[pIndex + 5] = detail*2+3;

        temp[pIndex+6] = detail  + 3;
        temp[pIndex + 7] = 1;
        temp[pIndex +8] = 0;

        temp[pIndex + 11] = detail + 3;
        temp[pIndex + 10] = detail+2;
        temp[pIndex + 9] = 0; 

        temp.CopyTo(polys, polys.Length / 2);
    }
    void UpdateMesh()//updates the mesh if any changes are made
    {
        m.Clear();
        m.vertices = vertices;
        m.uv = uv;
        m.triangles = polys;
       
    }
    private void Update()//live updating of mesh
    {
        if(prevData!=null&&prevData!=new Vector3(detail, length, totalAngle))
        {
            StartGen();
            UpdateMesh();
            print("Updating mesh");
        }
        prevData = new Vector3(detail, length, totalAngle);
        
    }
    Vector3 GetVectorFromAngle(float a)//turns an angle intro a vector direction, used for making a polygon in a certain direction
    {
        float angleInRadians = a * (Mathf.PI / 180);
        return new Vector3(Mathf.Cos(angleInRadians), 0, Mathf.Sin(angleInRadians));
    }
}
