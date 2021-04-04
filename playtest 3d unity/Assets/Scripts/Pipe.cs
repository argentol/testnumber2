using UnityEngine;

public class Pipe : MonoBehaviour {

    #region Fields

    private const float pipeRadius = 1.0f; // радиус внутри трубы
	private const int pipeSegmentCount = 20; // кол-во сегментов, образующих тор
	private const float ringDistance = 1.0f; // хз

    /// <summary>
	/// 
	/// </summary>
	public float minCurveRadius, maxCurveRadius; // радиус от центра тора до трубы


	public int minCurveSegmentCount, maxCurveSegmentCount; // кол-во сегментов, которые возьмутся из тора

	private int curveSegmentCount;

	private Mesh mesh;
	private int[] triangles;

    #endregion

    #region Properties

    public float CurveAngle { get; private set; }

	public float CurveRadius { get; private set; }

	public float RelativeRotation { get; private set; }

	#endregion

	#region Methods

	#region Overrides

	private void Awake()
	{
		var meshFilter = GetComponent<MeshFilter>();
		mesh = new Mesh();
		mesh.name = "Pipe";

		meshFilter.mesh = mesh;
	}

	#endregion

	#region  Custom Methods

	public void Generate (int white, bool NewLevel, bool StartOfGame) {
		if (StartOfGame && !NewLevel)
		{
			curveSegmentCount = maxCurveSegmentCount;
			CurveRadius = Random.Range(minCurveRadius, maxCurveRadius);
			NewLevel = false;
		}
		else
		{
			if (NewLevel)
			{
				curveSegmentCount = maxCurveSegmentCount;
				CurveRadius = Random.Range(minCurveRadius, maxCurveRadius);
				NewLevel = false;
			}

			else
			{
				if (white % 2 == 0)
				{
					curveSegmentCount = 1;
					CurveRadius = Random.Range(minCurveRadius, maxCurveRadius);
				}
				else
				{
					CurveRadius = Random.Range(minCurveRadius, maxCurveRadius);
					curveSegmentCount =
						Random.Range(minCurveSegmentCount, maxCurveSegmentCount + 1);
				}
			}
		}
			mesh.Clear();
			mesh.vertices = GetVertices();
			SetTriangles();
			mesh.RecalculateNormals();
	}

	public void AlignWith(Pipe pipe)
	{
		RelativeRotation =
			Random.Range(0, curveSegmentCount) * 360f / pipeSegmentCount;

		transform.SetParent(pipe.transform, false);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.Euler(0f, 0f, -pipe.CurveAngle);
		transform.Translate(0f, pipe.CurveRadius, 0f);
		transform.Rotate(RelativeRotation, 0f, 0f);
		transform.Translate(0f, -CurveRadius, 0f);
		transform.SetParent(pipe.transform.parent);
		transform.localScale = Vector3.one;
	}

	private Vector3[] GetVertices () {
		var vertices = new Vector3[pipeSegmentCount * curveSegmentCount * 4];

		float uStep = ringDistance / CurveRadius;
		CurveAngle = uStep * curveSegmentCount * (360f / (2f * Mathf.PI));

		CreateFirstQuadRing(vertices, uStep);
		int iDelta = pipeSegmentCount * 4;
		for (int u = 2, i = iDelta; u <= curveSegmentCount; u++, i += iDelta) {
			CreateQuadRing(vertices, u * uStep, i);
		}

		return vertices;
	}

	private void CreateFirstQuadRing (Vector3[] vertices, float u) {
		float vStep = (2f * Mathf.PI) / pipeSegmentCount;

		Vector3 vertexA = GetPointOnTorus(0f, 0f);
		Vector3 vertexB = GetPointOnTorus(u, 0f);
		for (int v = 1, i = 0; v <= pipeSegmentCount; v++, i += 4) {
			vertices[i] = vertexA;
			vertices[i + 1] = vertexA = GetPointOnTorus(0f, v * vStep);
			vertices[i + 2] = vertexB;
			vertices[i + 3] = vertexB = GetPointOnTorus(u, v * vStep);
		}
	}

	private void CreateQuadRing (Vector3[] vertices, float u, int i) {
		float vStep = (2f * Mathf.PI) / pipeSegmentCount;
		int ringOffset = pipeSegmentCount * 4;

		Vector3 vertex = GetPointOnTorus(u, 0f);
		for (int v = 1; v <= pipeSegmentCount; v++, i += 4) {
			vertices[i] = vertices[i - ringOffset + 2];
			vertices[i + 1] = vertices[i - ringOffset + 3];
			vertices[i + 2] = vertex;
			vertices[i + 3] = vertex = GetPointOnTorus(u, v * vStep);
		}
	}

	private void SetTriangles () {
		triangles = new int[pipeSegmentCount * curveSegmentCount * 6];
		for (int t = 0, i = 0; t < triangles.Length; t += 6, i += 4) {
			triangles[t] = i;
			triangles[t + 1] = triangles[t + 4] = i + 2;
			triangles[t + 2] = triangles[t + 3] = i + 1;
			triangles[t + 5] = i + 3;
		}
		mesh.triangles = triangles;
	}

	private Vector3 GetPointOnTorus (float u, float v) {
		Vector3 p;
		float r = (CurveRadius + pipeRadius * Mathf.Cos(v));
		p.x = r * Mathf.Sin(u);
		p.y = r * Mathf.Cos(u);
		p.z = pipeRadius * Mathf.Sin(v);
		return p;
	}

    #endregion



	#endregion
}