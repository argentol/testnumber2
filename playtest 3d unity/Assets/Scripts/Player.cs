using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading;

public class Player : MonoBehaviour {

	public PipeSystem pipeSystem;
	public GameObject TapToStart;
	public GameObject ScoreCounter;
	public GameObject Sphere;
	public GameObject LevelsMode;

	private float velocity = 0;
	public float PlayerVelocity;
	public float acceleration;

	public float rotationVelocity;

	private Pipe currentPipe;
	public bool RotateOrNot = false;
	private System.DateTime LevelDuration = System.DateTime.Now;

	private float distanceTraveled;
	private float deltaToRotation;
	private float systemRotation;
	private float worldRotation, avatarRotation;

	private Transform world, rotater;
	private bool StartGame = false;

	private void Start () {
		world = pipeSystem.transform.parent;
		rotater = transform.GetChild(0);
		currentPipe = pipeSystem.SetupFirstPipe();
		SetupCurrentPipe();
	}

	private void Update () {
		if (Input.touchCount > 0)
		{
			StartGame = true;
			ScoreCounter.gameObject.SetActive(true);
			TapToStart.gameObject.SetActive(false);
			LevelsMode.gameObject.SetActive(false);
		}
		if (StartGame == true)
		{
			while (PlayerVelocity >= velocity)
			{
				velocity += acceleration * Time.deltaTime;
			}
            float delta = velocity * Time.deltaTime;
			distanceTraveled += delta;
			systemRotation += delta * deltaToRotation;

			if (systemRotation >= currentPipe.CurveAngle) {
				delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;
				currentPipe = pipeSystem.SetupNextPipe();
				SetupCurrentPipe();
				systemRotation = delta * deltaToRotation;
			}

			pipeSystem.transform.localRotation =
				Quaternion.Euler(0f, 0f, systemRotation);
			if (RotateOrNot == true)
				UpdateAvatarRotation();
			ScoreCounter.GetComponent<Text>().text = "Score: " + ((int)distanceTraveled).ToString() ;
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		print(other.gameObject.name);
	}

	private void UpdateAvatarRotation()
	{
		avatarRotation -=
			rotationVelocity * Time.deltaTime /** Input.GetAxis("Horizontal")*/;
		if (avatarRotation < 0f)
		{
			avatarRotation += 360f;
		}
		else if (avatarRotation >= 360f)
		{
			avatarRotation -= 360f;
		}
		rotater.localRotation = Quaternion.Euler(avatarRotation, 0f, 0f);
	}


	private void SetupCurrentPipe () {
		deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
		worldRotation += currentPipe.RelativeRotation;
		if (worldRotation < 0f) {
			worldRotation += 360f;
		}
		else if (worldRotation >= 360f) {
			worldRotation -= 360f;
		}
		world.localRotation = Quaternion.Euler(worldRotation, 0f, 0f);
	}

	public Color GetSphereMaterialColor()
    {
		return Sphere.GetComponent<Renderer>().material.color;
    }

	public void KillPlayer()
	{
		velocity = 0;
		PlayerVelocity = -1;
		LevelsMode.gameObject.SetActive(true);
	}

}