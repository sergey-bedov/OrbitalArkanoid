using UnityEngine;
using System.Collections;

public class ArrayMaker : MonoBehaviour
{
	enum ArrayType {Orthogonal, Spherical};

	public GameObject SubjectObject;
	[SerializeField]
	ArrayType arrayType;
	public int QuantityOfSubjects;
	public float XorRadius;
	public float YorAngle;

	private Transform[] SubjectsTransforms;

	void Start()
	{
		MakeAnArray();
	}

	public void MakeAnArray()
	{
		CreateSubjects();
		UpdateSubjectsTransforms();
	}
	public void CreateSubjects()
	{
		SubjectsTransforms = new Transform[0];
		for (int i = 0; i < QuantityOfSubjects; i++)
		{
			GameObject newGO = Instantiate(SubjectObject) as GameObject;
			newGO.transform.parent = transform;
			CalculateNewTransform(newGO.transform, i);
			ArrayTools.PushLast(SubjectsTransforms, newGO.transform);
		}
	}
	public void CalculateNewTransform(Transform subjectTransform, int subjectNumber)
	{
		if (arrayType == ArrayType.Orthogonal)
		{
			float startX = QuantityOfSubjects * -XorRadius/2;
			subjectTransform.position = new Vector3 (startX + XorRadius * subjectNumber, 0, 0);
		}
		if (arrayType == ArrayType.Spherical)
		{
			subjectTransform.position = new Vector3 (0, -XorRadius, 0);
			subjectTransform.RotateAround(transform.position, Vector3.back, YorAngle * subjectNumber);
		}
	}
	public void UpdateSubjectsTransforms()
	{
		print ("UpdateSubjectsTransforms() | SubjectsTransforms.Length = " + SubjectsTransforms.Length);
		for (int i = 0; i < SubjectsTransforms.Length; i++)
		{
			CalculateNewTransform(SubjectsTransforms[i], i);
		}
	}
}
