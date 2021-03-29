using UnityEngine;
using UnityEngine.UI;
public class PipeSystem : MonoBehaviour {

	public Pipe pipePrefab;

	public int pipeCount;

	private Pipe[] pipes;

    private int WhitePipe = 2;


    public Button Button1;
	public Button Button2;
	public Button Button3;

	public GameObject cheating;

	public Material Color1;
	public Material Color2;
	public Material Color3;

	public Material Color1Button;
	public Material Color2Button;
	public Material Color3Button;

	public GameObject Player;
	public GameObject TextLose;

	int renewColors;

	public bool StartOfGame = false;
	private int newLevel = 10;
	private int newLevel1 = 0;

	bool WhiteRepeat = false;
    private bool NewLevel = false;
	private void Awake () {
		randomizeOfColors();
		pipes = new Pipe[pipeCount];
		for (int i = 0; i < pipes.Length; i++) {
			Pipe pipe = pipes[i] = Instantiate<Pipe>(pipePrefab);
			pipe.transform.SetParent(transform, false);
			pipe.Generate(WhitePipe,NewLevel, StartOfGame);
            WhitePipe++;
            if (i > 0) {
				pipe.AlignWith(pipes[i - 1]);
			}
		}
  //      Color1.color = new Color(Random.value, Random.value, Random.value, 1);
		//Color2.color = new Color(Random.value, Random.value, Random.value, 1);
		//Color3.color = new Color(Random.value, Random.value, Random.value, 1);
		AlignNextPipeWithOrigin(WhitePipe, newLevel);
        //randomizeOfColors();
		ColoringOfButtons();
		pipes[pipeCount-1].GetComponent<Renderer>().material.color = Color.red;
		StartOfGame = false;
	}

	
    void FixedUpdate()
    {
		IsLose();
		if ((pipes[0].GetComponent<Renderer>().material.color == Color.white) && (StartOfGame = false))
			StartOfGame = true;
		if ((pipes[1].GetComponent<Renderer>().material.color == Color.white) && (newLevel == 9) /*&& (pipes[1].GetComponent<Renderer>().material.color == Color.white)*/)
		{
			ColoringOfButtons();
			//ColoringOfPipes();
		}

		if ((pipes[1].GetComponent<Renderer>().material.color == Color.white) && (newLevel == 0)  /*&& (pipes[1].GetComponent<Renderer>().material.color == Color.white)*/)
		{
			randomizeOfColors();
			newLevel = 10;
			NewLevel = true;
			ColoringOfPipes();
			ColoringOfButtons();
		}
		//if (newLevel == 0)
		//{
		//	//if(pipes[1].GetComponent<Renderer>().material.name == cheating.GetComponent<Renderer>().material.name)
  // //         {
  // //             Button1.image.color = Color1Button.color;
  // //             Button2.image.color = Color2Button.color;
  // //             Button3.image.color = Color3Button.color;
  // //         }
		//	randomizeOfColors();
		//	newLevel = 10;
		//	NewLevel = true;
		//}
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
		//print(newLevel);
        print("Start of game " + StartOfGame);
        print("NewLevel " + NewLevel);
		//print(newLevel);
    }

	//void ColoringOfPipes()
 //   {
	//	for (int i = 0; i < pipes.Length; i++)
 //       {
	//		if (i % 2 != 0)
	//			pipes[i].GetComponent<Renderer>().material.color = Color.red;
	//		else
	//			pipes[i].GetComponent<Renderer>().material.color = Color.white;
	//	}
	//}

    void randomizeOfColors()
    {
        Color1.color = new Color(Random.value, Random.value, Random.value, 1);
        Color2.color = new Color(Random.value, Random.value, Random.value, 1);
        Color3.color = new Color(Random.value, Random.value, Random.value, 1);
        //Color1Button.color = new Color(1, 1, 1, 1);
        //Color2Button.color = new Color(1, 1, 0, 1);
        //Color3Button.color = new Color(0, 1, 1, 1);
        Color1Button.color = Color1.color;
        Color2Button.color = Color2.color;
		Color3Button.color = Color3.color;
	}

    public Pipe SetupFirstPipe () {
		transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
		return pipes[1];
	}

	public Pipe SetupNextPipe () {
		ShiftPipes();
		AlignNextPipeWithOrigin(WhitePipe, newLevel);
        WhitePipe++;
        pipes[pipes.Length - 1].Generate(WhitePipe, NewLevel, StartOfGame);
		pipes[pipes.Length - 1].AlignWith(pipes[pipes.Length - 2]);
		transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
		return pipes[1];
	}

	private void ShiftPipes () {
		Pipe temp = pipes[0];
		for (int i = 1; i < pipes.Length; i++) {
			pipes[i - 1] = pipes[i];
		}
		pipes[pipes.Length - 1] = temp;
	}

	int RandomMaterial = 1;

	private void ColoringOfPipes()
    {
		print("Hello");
		for (int i = 1; i < pipes.Length; i++)
			if (i % 2 == 0)
			{
				RandomMaterial = Random.Range(1, 4);
				if (RandomMaterial  == 1)
					pipes[i].GetComponent<Renderer>().material.color = Color1Button.color;
				if (RandomMaterial == 2)
					pipes[i].GetComponent<Renderer>().material.color = Color2Button.color;
				if (RandomMaterial == 3)
					pipes[i].GetComponent<Renderer>().material.color = Color3Button.color;
			}
			else
				pipes[i].GetComponent<Renderer>().material.color = Color.white;

	}
	private void ColoringOfButtons()
    {
        Button1.image.color = Color1Button.color;
        Button2.image.color = Color2Button.color;
        Button3.image.color = Color3Button.color;
    }

	private void IsLose()
    {
		if ((pipes[1].GetComponent<Renderer>().material.color != Color.red) && (pipes[1].GetComponent<Renderer>().material.color != Color.black) && (pipes[1].GetComponent<Renderer>().material.color != Color.white))
		{
				if (pipes[1].GetComponent<Renderer>().material.color != Player.GetComponent<Renderer>().material.color)
					TextLose.gameObject.SetActive(true);	
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
			//WhitePipe++;
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