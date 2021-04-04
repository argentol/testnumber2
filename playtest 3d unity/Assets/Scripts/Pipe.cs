using UnityEngine;

public class Pipe : MonoBehaviour {

    #region Fields

    private const float PipeRadius = 1.0f; // радиус внутри трубы
	private const int PipeSegmentCount = 20; // кол-во сегментов, образующих тор
	private const float RingDistance = 1.0f; // хз

	private int curveSegmentCount_;

	public float minCurveRadius_, maxCurveRadius_; // радиус от центра тора до трубы
	public int minCurveSegmentCount_, maxCurveSegmentCount_; // кол-во сегментов, которые возьмутся из тора


    #endregion

    #region Properties

    public float CurveAngle { get; private set; }

	public float CurveRadius { get; private set; }

	public float RelativeRotation { get; private set; }

	#endregion

	#region Public Methods

	public void InitializePipe(int white, bool startOfGame)
	{
		curveSegmentCount_ = CalculateCurveSegmentCount(white, startOfGame);
		CurveRadius = CalculateCurveRadius(white, startOfGame);


		var meshFilter = GetComponent<MeshFilter>();
		meshFilter.mesh = CreateMesh();
	}

	public void AlignWith(Pipe pipe)
	{
		RelativeRotation =
			Random.Range(0, curveSegmentCount_) * 360f / PipeSegmentCount;

		transform.SetParent(pipe.transform, false);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.Euler(0f, 0f, -pipe.CurveAngle);
		transform.Translate(0f, pipe.CurveRadius, 0f);
		transform.Rotate(RelativeRotation, 0f, 0f);
		transform.Translate(0f, -CurveRadius, 0f);
		transform.SetParent(pipe.transform.parent);
		transform.localScale = Vector3.one;
	}

	public void SetMaterialColor(Color color)
    {
		var material = GetComponent<Renderer>().material;
		material.color = color;
	}

    #endregion

    #region Non-Public Methods

    private int CalculateCurveSegmentCount(int white, bool startOfGame)
    {
		int curveSegmentCount;
		if (startOfGame)
		{
			curveSegmentCount = maxCurveSegmentCount_;
		}
		else
		{
			if (white % 2 == 0)
			{
				curveSegmentCount = 1;
			}
			else
			{
				curveSegmentCount = Random.Range(minCurveSegmentCount_, maxCurveSegmentCount_ + 1);
			}
		}

		return curveSegmentCount;
	}

	private float CalculateCurveRadius(int white, bool startOfGame)
	{
		float curveRadius;

		if (startOfGame)
		{
			curveRadius = Random.Range(minCurveRadius_, maxCurveRadius_);
		}
		else
		{
			if (white % 2 == 0)
			{
				curveRadius = Random.Range(minCurveRadius_, maxCurveRadius_);
			}
			else
			{
				curveRadius = Random.Range(minCurveRadius_, maxCurveRadius_);
			}
		}

		return curveRadius;
	}

	private Mesh CreateMesh()
    {
		var mesh = new Mesh();
		mesh.name = "Pipe";


		mesh.Clear();
		mesh.vertices = CalculateVertices();
		mesh.triangles = CalculateTriangles();
		mesh.RecalculateNormals();

		return mesh;
	}


    #region Calculate Vertices

    private Vector3[] CalculateVertices () 
	{
		var vertices = new Vector3[PipeSegmentCount * curveSegmentCount_ * 4];

		float uStep = GetUStep();
		CurveAngle = uStep * curveSegmentCount_ * (360f / (2f * Mathf.PI));

		CreateFirstQuadRing(vertices, uStep);
		int iDelta = PipeSegmentCount * 4;
		for (int u = 2, i = iDelta; u <= curveSegmentCount_; u++, i += iDelta) {
			CreateQuadRing(vertices, u * uStep, i);
		}

		return vertices;
	}

	private float GetUStep()
    {
		return RingDistance / CurveRadius;
	}

	private void CreateFirstQuadRing (Vector3[] vertices, float u) {
		float vStep = (2f * Mathf.PI) / PipeSegmentCount;

		Vector3 vertexA = GetPointOnTorus(0f, 0f);
		Vector3 vertexB = GetPointOnTorus(u, 0f);
		for (int v = 1, i = 0; v <= PipeSegmentCount; v++, i += 4) {
			vertices[i] = vertexA;
			vertices[i + 1] = vertexA = GetPointOnTorus(0f, v * vStep);
			vertices[i + 2] = vertexB;
			vertices[i + 3] = vertexB = GetPointOnTorus(u, v * vStep);
		}
	}

	private void CreateQuadRing (Vector3[] vertices, float u, int i) {
		float vStep = (2f * Mathf.PI) / PipeSegmentCount;
		int ringOffset = PipeSegmentCount * 4;

		Vector3 vertex = GetPointOnTorus(u, 0f);
		for (int v = 1; v <= PipeSegmentCount; v++, i += 4) {
			vertices[i] = vertices[i - ringOffset + 2];
			vertices[i + 1] = vertices[i - ringOffset + 3];
			vertices[i + 2] = vertex;
			vertices[i + 3] = vertex = GetPointOnTorus(u, v * vStep);
		}
	}

    #endregion


    private int[] CalculateTriangles ()
	{
		var triangles = new int[PipeSegmentCount * curveSegmentCount_ * 6];

		for (int t = 0, i = 0; t < triangles.Length; t += 6, i += 4) {
			triangles[t] = i;
			triangles[t + 1] = triangles[t + 4] = i + 2;
			triangles[t + 2] = triangles[t + 3] = i + 1;
			triangles[t + 5] = i + 3;
		}

		return triangles;
	}

	private Vector3 GetPointOnTorus (float u, float v) {
		Vector3 p;
		float r = (CurveRadius + PipeRadius * Mathf.Cos(v));
		p.x = r * Mathf.Sin(u);
		p.y = r * Mathf.Cos(u);
		p.z = PipeRadius * Mathf.Sin(v);
		return p;
	}

	#endregion
}