using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Chunk : MonoBehaviour
{

    Vector3[] verticies;
    int[] triangles;
    Mesh mesh;
    public int xSize = 50;
    public int zSize = 50;
    public float roughness = 0.3f;
    private MeshCollider mcollider;
    public float noiseScale = 5;
    public float spacedout;
    int treerandom;

    float rotationTreeX;
    float rotationTreeZ;
    public float perlinNoiseY;
    public GameObject tree;
    List<GameObject> treeList = new List<GameObject>();
    public GameObject player;
    public float treedistance = 100;

    public float offsetX;
    public float offsetY;

    RaycastHit hit;

    Color[] colors;
    public Gradient gradient;

    float minheight = 100000000;
    float maxheight = -100000000;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mcollider = GetComponent<MeshCollider>();

        CreateShape();
        updateMesh();

        mcollider.convex = false;

        treeGen();

    }

    private void FixedUpdate()
    {
        renderupdate();
    }

    void treeGen() { 

        for(int i = 0; i < 500; i++)
        {

            if (Physics.Raycast(new Vector3(Random.Range(0, 1000), 1000, Random.Range(0, 1000)), Vector3.down, out hit))
            {

                GameObject treeobj = Instantiate(tree, hit.transform.position, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
                treeobj.transform.position = hit.point;
                treeList.Add(treeobj);
            }
        }

    }

    float exp(float x, int exp) {

        float result = 1;

        for (int i = 0; i < exp; i++) {

            result *= x;

        }
        return result;
    }

    public void CreateShape()
    {

        verticies = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int index = 0, i = 0; i <= xSize; i++)
        {
            for (int j = 0; j <= zSize; j++)
            {
                treerandom = Random.Range(1, 65);

                float n1 = (Mathf.PerlinNoise(offsetX + i * roughness*0.3f, j * roughness*0.3f + offsetY) * noiseScale);
                float n2 = (Mathf.PerlinNoise(offsetX + i * roughness*0.3f, j * roughness*0.3f + offsetY) * noiseScale*0.5f);
                float n3 = (Mathf.PerlinNoise(offsetX + i * roughness*0.3f, j * roughness*0.3f + offsetY) * noiseScale*0.25f);

                perlinNoiseY = n1*n2*n3;


                if ((i == 0) || (i == xSize) || (j == zSize) || (j == 0))
                {
                    verticies[index] = new Vector3(j * spacedout, 0, i * spacedout);
                }
                else
                {

                    verticies[index] = new Vector3(j * spacedout, perlinNoiseY, i * spacedout);

                }
                index++;


                if (perlinNoiseY > maxheight) {
                    maxheight = perlinNoiseY;
                }
                if (perlinNoiseY < minheight)
                {
                    minheight = perlinNoiseY;
                }
            }
        }

        triangles = new int[(xSize * zSize) * 6];

        int tri = 0;
        int v = 0;
        for (int a = 0; a < zSize; a++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tri + 0] = v + 0;
                triangles[tri + 1] = v + xSize + 1;
                triangles[tri + 2] = v + 1;
                triangles[tri + 3] = v + 1;
                triangles[tri + 4] = v + xSize + 1;
                triangles[tri + 5] = v + xSize + 2;

                tri += 6;
                v++;
            }
            v++;
        }

        colors = new Color[verticies.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float height = Mathf.InverseLerp(minheight, maxheight, verticies[i].y);
                colors[i] = gradient.Evaluate(height);
                i++;
            }
        }

    }
    void updateMesh()
    {

        mcollider.sharedMesh = mesh;

        mesh.Clear();

        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();
    }


    void renderupdate() {

        foreach (GameObject tree in treeList) {

            if (Mathf.Abs(tree.transform.position.x - player.transform.position.x) > treedistance || Mathf.Abs(tree.transform.position.z - player.transform.position.z) > treedistance) {
                tree.SetActive(false);
            }
            else
            {
                tree.SetActive(true);
            }

            if (tree.transform.position.y < 3.5f) {

                tree.SetActive(false);

            }

        }

    }

}