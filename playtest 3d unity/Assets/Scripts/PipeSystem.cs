using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PipeSystem : MonoBehaviour {

	public Pipe pipePrefab;

	public int pipeCount;

	private Pipe[] pipes;

	private int WhitePipe = 1;


	public Button Button1;
	public Button Button2;
	public Button Button3;

	public Button RestartButton;

	public Button NextLevel;

	public GameObject cheating;

	public Material Color1;
	public Material Color2;
	public Material Color3;

	public Material Color1Button;
	public Material Color2Button;
	public Material Color3Button;

	public Player Player;

	public GameObject TextWin;
	public float LevelDuration;
	private float PrivateLevelDuration;
	public bool endlessLevel;


	private Color[] MassiveOfColors = CreatingMassiveOfColors() /*new Color[] { Color.blue, Color.red, Color.yellow, Color.green, Color.grey, Color.magenta, Color.cyan, Color.black }*/;

	int renewColors;

	public bool StartOfGame = false;
	private int newLevel;

	private int newLevel1 = 0;

	bool WhiteRepeat = false;
	private bool NewLevel = false;
	private void Awake() {
		PrivateLevelDuration = LevelDuration;
		newLevel = (pipeCount - 1) / 2;
		pipes = new Pipe[pipeCount];
		for (int i = 0; i < pipes.Length; i++) {
			if (i == 1)
				NewLevel = true;
			Pipe pipe = pipes[i] = Instantiate<Pipe>(pipePrefab);
			pipe.transform.SetParent(transform, false);
			pipe.Generate(WhitePipe, NewLevel);
			NewLevel = false;
			WhitePipe++;
			if (i > 0)
			{
				pipe.AlignWith(pipes[i - 1]);
			}
		}
		randomizeOfColors();
		AlignNextPipeWithOrigin(WhitePipe, newLevel);
		ColoringOfButtons();
		ColoringOfPipes();
		NewLevel = true;
		WhitePipe = 1;
	}


	void FixedUpdate()
	{
		IsLose();
		if ((pipes[0].GetComponent<Renderer>().material.color == Color.white) && (StartOfGame = false))
			StartOfGame = true;
		if ((pipes[1].GetComponent<Renderer>().material.color == Color.white) && (newLevel == 9))
		{
			ColoringOfButtons();
		}

		if ((pipes[1].GetComponent<Renderer>().material.color == Color.white) && (newLevel == 0))
		{
			randomizeOfColors();
			newLevel = (pipeCount - 1) / 2;
			NewLevel = true;
			ColoringOfPipes();
			ColoringOfButtons();
		}
		if ((pipes[1].GetComponent<Renderer>().material.color == Color.white) && (WhiteRepeat == false))
		{
			newLevel--;
			WhiteRepeat = true;
			NewLevel = false;
		}
		if ((pipes[1].GetComponent<Renderer>().material.color == Color.white) && (newLevel1 == 0))
		{
			newLevel++;
			NewLevel = true;
			newLevel1 = -10;
		}
		if (!(pipes[1].GetComponent<Renderer>().material.color == Color.white))
		{
			WhiteRepeat = false;
			NewLevel = false;
		}
		PrivateLevelDuration -= Time.deltaTime;
		if ((PrivateLevelDuration < 0) && (endlessLevel == false))
			EndOfLevel();
	}

	void randomizeOfColors()
	{
		var (color1, color2, color3) = GetNonRepeatebleColors();
		Color1Button.color = color1;
		Color2Button.color = color2;
		Color3Button.color = color3;
		Color1.color = color1;
		Color2.color = color2;
		Color3.color = color3;
	}

	public Pipe SetupFirstPipe() {
		transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
		return pipes[1];
	}

	public Pipe SetupNextPipe() {
		ShiftPipes();
		AlignNextPipeWithOrigin(WhitePipe, newLevel);
		WhitePipe++;
		pipes[pipes.Length - 1].Generate(WhitePipe, NewLevel);
		pipes[pipes.Length - 1].AlignWith(pipes[pipes.Length - 2]);
		transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
		return pipes[1];
	}

	private (Color, Color, Color) GetNonRepeatebleColors()
    {
		int RandomColorButton1;
		int RandomColorButton2;
		int RandomColorButton3;
		Color Color1;
		Color Color2;
		Color Color3;

		RandomColorButton1 = Random.Range(0, MassiveOfColors.Length);
		Color1 = MassiveOfColors[RandomColorButton1];
		RandomColorButton2 = Random.Range(0, MassiveOfColors.Length);
		while (RandomColorButton2 == RandomColorButton1)
			RandomColorButton2 = Random.Range(0, MassiveOfColors.Length);
		Color2 = MassiveOfColors[RandomColorButton2];
		RandomColorButton3 = Random.Range(0, MassiveOfColors.Length);
		while ((RandomColorButton3 == RandomColorButton1) || (RandomColorButton3 == RandomColorButton2))
			RandomColorButton3 = Random.Range(0, MassiveOfColors.Length);
		Color3 = MassiveOfColors[RandomColorButton3];

		return (Color1, Color2, Color3);
	}

	private void ShiftPipes() {
		Pipe temp = pipes[0];
		for (int i = 1; i < pipes.Length; i++) {
			pipes[i - 1] = pipes[i];
		}
		pipes[pipes.Length - 1] = temp;
	}
	int RandomMaterial = 1;

	int[] mas;
	private void ColoringOfPipes()
	{
		var mas = new int[pipes.Length-1];
		for (int i = 1; i < pipes.Length; i++)
			if (i % 2 == 0)
			{
				RandomMaterial = Random.Range(1, 4);
                mas[i - 1] = RandomMaterial;

                if (i >= 4)
                {
					while (mas[i - 1] == mas[i - 3])
					{
						RandomMaterial = Random.Range(1, 4);
						mas[i - 1] = RandomMaterial;
					}
                }

                if (RandomMaterial == 1)
					pipes[i].GetComponent<Renderer>().material.color = Color1Button.color;
				if (RandomMaterial == 2)
					pipes[i].GetComponent<Renderer>().material.color = Color2Button.color;
				if (RandomMaterial == 3)
					pipes[i].GetComponent<Renderer>().material.color = Color3Button.color;
			}
			else
			{
				pipes[i].GetComponent<Renderer>().material.color = Color.white;
                mas[i - 1] = 0;
            }
	}
    private static Color[] CreatingMassiveOfColors()
    {
        var massiveOfColors = new Color[13];
        massiveOfColors[0] = new Color(0, 0, 1);  //синий
        massiveOfColors[1] = new Color(0, 0.4f, 0.1f); //темно-зеленый
        massiveOfColors[2] = new Color(0, 0.4f, 1);   //голубой
        massiveOfColors[3] = new Color(0, 1, 0); //зеленый
        massiveOfColors[4] = new Color(0.1f, 0.9f, 1); //лазурный
		massiveOfColors[5] = new Color(0.6f, 0.3f, 0); //светло-коричневый
		massiveOfColors[6] = new Color(1, 0, 1); //пурпурный
		massiveOfColors[7] = new Color(1, 0.5f, 0); //светло-оранжевый
		massiveOfColors[8] = new Color(1, 0.5f, 0.9f);  //светло-розовый
		massiveOfColors[9] = new Color(1, 1, 0); //желтый
		massiveOfColors[10] = new Color(1, 0, 0); //красный
		massiveOfColors[11] = new Color(0, 0, 0);  //черный
		massiveOfColors[12] = new Color(0.5f, 0.5f, 0.5f); //grey

		return massiveOfColors;
    }
    private void ColoringOfButtons()
    {
        Button1.image.color = Color1Button.color;
        Button2.image.color = Color2Button.color;
        Button3.image.color = Color3Button.color;
    }

	private void IsLose()
    {
		if ((pipes[1].GetComponent<Renderer>().material.color != Color.white))
		{
			if (pipes[1].GetComponent<Renderer>().material.color != Player.GetSphereMaterialColor())
			{
				Player.KillPlayerLose();
				RemoveButtons();
				RestartButton.gameObject.SetActive(true);
			}
        }
	}

	private void RemoveButtons()
    {
		Button1.gameObject.SetActive(false);
		Button2.gameObject.SetActive(false);
		Button3.gameObject.SetActive(false);
	}
	private void EndOfLevel()
    {
		if ((pipes[1].GetComponent<Renderer>().material.color == Color.white) && (newLevel == 0))
        {
			SaveSystem.UpdateLevel();
			TextWin.gameObject.SetActive(true);
			Player.KillPlayerWin();
			RemoveButtons();
			NextLevel.gameObject.SetActive(true);
        }
	}

	private void AlignNextPipeWithOrigin (int WhitePipe, int newLevel) {
		Transform transformToAlign = pipes[1].transform;
		for (int i = 0; i < pipes.Length; i++)
		{
			if (i != 1)
			{
				pipes[i].transform.SetParent(transformToAlign);
			}
		}
		if (newLevel == 0 )
		{
			Material material = pipes[pipes.Length - 1].GetComponent<Renderer>().material;
			material.color = Color.white;
			NewLevel = false;
		}
		else
		{
			if (WhitePipe % 2 == 0)
			{
				Material material = pipes[pipes.Length - 1].GetComponent<Renderer>().material;
				RandomMaterial = Random.Range(1, 4);
				if (RandomMaterial == 1)
					material.color = Color1.color;
				if (RandomMaterial == 2)
					material.color = Color2.color; 
				if (RandomMaterial == 3)
					material.color = Color3.color; 	
			}
			else
			{
				Material material = pipes[pipes.Length - 1].GetComponent<Renderer>().material;
				material.color = Color.white;
			}
		}

        transformToAlign.localPosition = Vector3.zero;
		transformToAlign.localRotation = Quaternion.identity;

		for (int i = 0; i < pipes.Length; i++)
		{
			if (i != 1)
			{
				pipes[i].transform.SetParent(transform);
			}
		}
	}
}