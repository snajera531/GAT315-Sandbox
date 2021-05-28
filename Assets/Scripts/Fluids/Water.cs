﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
	[Range(1.0f, 90.0f)] public float fps = 60.0f;
	[Range(0.0f, 1.0f)] public float damping = 0.04f;
	[Range(2, 80)] public int xMeshVertexNum = 2;
	[Range(2, 80)] public int zMeshVertexNum = 2;
	[Range(1.0f, 80.0f)] public float xMeshSize = 40.0f;
	[Range(1.0f, 80.0f)] public float zMeshSize = 40.0f;
	public MeshFilter meshFilter = null;
	public MeshCollider meshCollider = null;

	Mesh mesh = null;
	Vector3[] vertices;

	float time = 0.0f;
	int frame = 0;

	float[,] buffer1;
	float[,] buffer2;

	float timeStep { get => 1.0f / fps; }

	ref float[,] previous { get => ref buffer1; }
	ref float[,] current  { get => ref buffer2; }

	void Start()
	{
		mesh = meshFilter.mesh;
		MeshGenerator.Plane(meshFilter, xMeshSize, zMeshSize, xMeshVertexNum, zMeshVertexNum);
		vertices = mesh.vertices;

		buffer1 = new float[xMeshVertexNum, zMeshVertexNum];
		buffer2 = new float[xMeshVertexNum, zMeshVertexNum];
	}

	void Update()
	{
		time = time + Time.deltaTime;
		while (time > timeStep)
		{
			UpdateSimulation(ref previous, ref current, timeStep);
			frame++;

			time = time - timeStep;
		}

		// set vertices height from current buffer


		// recalculate mesh with new vertices
		mesh.vertices = vertices;
		mesh.RecalculateNormals();
		mesh.RecalculateTangents();
		mesh.RecalculateBounds();
		meshCollider.sharedMesh = mesh;
	}

	void UpdateSimulation(ref float[,] previous, ref float[,] current, float dt)
	{
		for (int x = 1; x < xMeshVertexNum-1; x++)
		{
			for (int z = 1; z < zMeshVertexNum-1; z++)
			{
				// update buffer value
				float value = 0;
				current[x, z] = value;
			}
		}
	}

	public void Touch(Ray ray, float offset)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit))
		{
			MeshCollider meshCollider = raycastHit.collider as MeshCollider;
			if (meshCollider == this.meshCollider)
			{
				int[] triangles = mesh.triangles;
				int index = triangles[raycastHit.triangleIndex * 3];
				int x = index % xMeshVertexNum;
				int z = index / xMeshVertexNum;

				if (x > 1 && x < xMeshVertexNum - 1 && z > 1 && z < zMeshVertexNum - 1)
				{
					current[x, z] = offset;
				}
			}
		}
	}
}
