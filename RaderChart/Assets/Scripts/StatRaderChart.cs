using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatRaderChart : MonoBehaviour
{
    private Stat _stats;

    private CanvasRenderer _radarMeshCanvasRenderer;
    [SerializeField] private Material _radarMaterial;
    //[SerializeField] private Texture2D _radarTexture2D;

    private void Awake()
    {
        _radarMeshCanvasRenderer = transform.Find("RadarMesh").GetComponent<CanvasRenderer>();
    }

    public void SetStats(Stat stats)
    {
        this._stats = stats;
        UpdateStatsVisual();
    }

    private void UpdateStatsVisual()
    {
        //transform.Find("Wealth_Bar").localScale = new Vector3(1, stats.GetStatAmountNormalized(Stat.Type.Wealth));
        //transform.Find("Career_Bar").localScale = new Vector3(1, stats.GetStatAmountNormalized(Stat.Type.Career));

        Mesh mesh = new Mesh();

        //set up the mesh
        Vector3[] vertices = new Vector3[6];
        int[] triangles = new int[3 * 5];
        Vector2[] uv = new Vector2[6];

        float angleIncrement = 360f / 5;
        //the maximum length of the mesh
        float radarChartSize = 145f;

        //calculate each stat vertex, so need the rotation angle and stat value 
        Vector3 wealthVertex = Quaternion.Euler(0, 0, angleIncrement * 0) * Vector3.up * radarChartSize * _stats.GetStatAmountNormalized(Stat.Type.Wealth);
        int wealthVertexIndex = 1;
        Vector3 careerVertex = Quaternion.Euler(0, 0, -angleIncrement * 1) * Vector3.up * radarChartSize * _stats.GetStatAmountNormalized(Stat.Type.Career);
        int careerVertexIndex = 2;
        Vector3 healthVertex = Quaternion.Euler(0, 0, -angleIncrement * 2) * Vector3.up * radarChartSize * _stats.GetStatAmountNormalized(Stat.Type.Health);
        int healthVertexIndex = 3;
        Vector3 marriageVertex = Quaternion.Euler(0, 0, -angleIncrement * 3) * Vector3.up * radarChartSize * _stats.GetStatAmountNormalized(Stat.Type.Marriage);
        int marriageVertexIndex = 4;
        Vector3 relationVertex = Quaternion.Euler(0, 0, -angleIncrement * 4) * Vector3.up * radarChartSize * _stats.GetStatAmountNormalized(Stat.Type.Relation);
        int relationVertexIndex = 5;

        //get the corresponding vertex for each stat
        vertices[0] = Vector3.zero;
        vertices[wealthVertexIndex] = wealthVertex;
        vertices[careerVertexIndex] = careerVertex;
        vertices[healthVertexIndex] = healthVertex;
        vertices[marriageVertexIndex] = marriageVertex;
        vertices[relationVertexIndex] = relationVertex;

        //assign the index of the vertices that form up a triangle (wealthVertexIndex = vertices[1])
        //for example, triangles[0] is the origin vertex, from there connect to wealth vertex, 
        //then connect to career vertex, finally connect back to origin vertex to form a triangle
        //in total of 5 triangles (15 vertices) to form the full rader chart mesh
        triangles[0] = 0;
        triangles[1] = wealthVertexIndex;
        triangles[2] = careerVertexIndex;

        triangles[3] = 0;
        triangles[4] = careerVertexIndex;
        triangles[5] = healthVertexIndex;

        triangles[6] = 0;
        triangles[7] = healthVertexIndex;
        triangles[8] = marriageVertexIndex;

        triangles[9] = 0;
        triangles[10] = marriageVertexIndex;
        triangles[11] = relationVertexIndex;

        triangles[12] = 0;
        triangles[13] = relationVertexIndex;
        triangles[14] = wealthVertexIndex;

        //assign a texture position to the uv index (uv index = vertex index)
        //Vector2.one means the top right of the texture (0, 0: bottom left)(0, 1: top left)(1, 1: top right)(1, 0: bottom right)
        /*uv[0] = Vector2.zero;
        uv[wealthVertexIndex] = Vector2.one;
        uv[careerVertexIndex] = Vector2.one;
        uv[healthVertexIndex] = Vector2.one;
        uv[marriageVertexIndex] = Vector2.one;
        uv[relationVertexIndex] = Vector2.one;*/

        //////////////////////version 2 updates///////////////////////
        //assign a material position to each vertices (assign a color to each categories)
        uv[0] = new Vector2(0.5f, 0.5f); // get the middle uv of the material(RadarGraph_Mat)
        uv[wealthVertexIndex] = Vector2.up; // get the top left uv (Yellow)
        uv[careerVertexIndex] = Vector2.one; // get the top right uv (Red)
        uv[healthVertexIndex] = Vector2.zero; // get the bottom left uv (Green)
        uv[marriageVertexIndex] = Vector2.one; // get the top right uv (Red)
        uv[relationVertexIndex] = Vector2.right; // get the bottom right uv (Blue)

        //upload the values to the mesh
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        //render the mesh
        _radarMeshCanvasRenderer.SetMesh(mesh);
        //////////////////////version 2 updates///////////////////////
        //_radarMeshCanvasRenderer.SetMaterial(_radarMaterial, _radarTexture2D);
        _radarMeshCanvasRenderer.SetMaterial(_radarMaterial, null);
    }
}
