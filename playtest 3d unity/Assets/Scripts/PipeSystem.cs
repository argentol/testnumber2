using UnityEngine;
using UnityEngine.UI;

public class PipeSystem : MonoBehaviour {

	public Pipe pipePrefab_;

	public ColorObjectManager colorObjectManager_;
	public int pipeCount_;

	private Pipe[] pipes_;

    private int whitePipe_ = 2;

    public Button button1_;
	public Button button2_;
	public Button button3_;

	public Material color1_;
	public Material color2_;
	public Material color3_;

	public Material color1Button_;
	public Material color2Button_;
	public Material color3Button_;

	public GameObject player_;
	public GameObject textLose_;


	public bool StartOfGame = false;

	private const int StartLevel = 10;
	private const int EndLevel = 0;

	private int newLevel_ = StartLevel;

	private int newLevel1_ = EndLevel;

	bool whiteRepeat_ = false;

	private int RandomMaterial = 1;

	private void Awake ()
	{
		RandomizeOfColors();

		pipes_ = CreateDefaultPipes();

		AlignNextPipeWithOrigin();
		SetMaterialColorForLastPipe(whitePipe_, whitePipe_);

		ColoringOfButtons();
		StartOfGame = false;
	}

	private Pipe[] CreateDefaultPipes()
    {
		var pipes = new Pipe[pipeCount_];

		for (int i = 0; i < pipes.Length; i++)
		{
			pipes[i] = Instantiate<Pipe>(pipePrefab_);
			pipes[i].transform.SetParent(transform, false);

			pipes[i].InitializePipe(whitePipe_, StartOfGame);

			whitePipe_++;

			if (i > 0)
			{
				pipes[i].AlignWith(pipes[i - 1]);
			}
		}

		return pipes;
	}
	
    void FixedUpdate()
    {
		CheckLose();

        var currentPipeColor = pipes_[1].GetComponent<Renderer>().material.color;

		if (currentPipeColor == Color.white && newLevel_ == StartLevel)
		{
			RandomizeOfColors();
			ColoringOfPipes();
			ColoringOfButtons();
		}

		if (currentPipeColor == Color.white && whiteRepeat_ == false)
		{
			newLevel_--;
			whiteRepeat_ = true;
		}
		if (currentPipeColor == Color.white && newLevel1_ == EndLevel)
		{
			newLevel_++;
			newLevel1_ = -StartLevel;
		}
		if (!(currentPipeColor == Color.white))
		{
			whiteRepeat_ = false;
		}
    }


    void RandomizeOfColors()
    {
        color1_.color = new Color(Random.value, Random.value, Random.value, 1);
        color2_.color = new Color(Random.value, Random.value, Random.value, 1);
        color3_.color = new Color(Random.value, Random.value, Random.value, 1);

        color1Button_.color = color1_.color;
        color2Button_.color = color2_.color;
		color3Button_.color = color3_.color;
	}

    public Pipe SetupFirstPipe () 
	{
		transform.localPosition = new Vector3(0f, -pipes_[1].CurveRadius);
		return pipes_[1];
	}

	public Pipe SetupNextPipe () 
	{
		ShiftPipes();
		AlignNextPipeWithOrigin();
		SetMaterialColorForLastPipe(whitePipe_, whitePipe_);

		whitePipe_++;
        pipes_[pipes_.Length - 1].InitializePipe(whitePipe_, StartOfGame);
		pipes_[pipes_.Length - 1].AlignWith(pipes_[pipes_.Length - 2]);
		transform.localPosition = new Vector3(0f, -pipes_[1].CurveRadius);
		return pipes_[1];
	}

	private void ShiftPipes () 
	{
		Pipe temp = pipes_[0];
		for (int i = 1; i < pipes_.Length; i++) {
			pipes_[i - 1] = pipes_[i];
		}
		pipes_[pipes_.Length - 1] = temp;
	}


	private void ColoringOfPipes()
    {
		for (int i = 1; i < pipes_.Length; i++)
			if (i % 2 == 0)
			{
				RandomMaterial = Random.Range(1, 4);
				if (RandomMaterial  == 1)
					pipes_[i].GetComponent<Renderer>().material.color = color1Button_.color;
				if (RandomMaterial == 2)
					pipes_[i].GetComponent<Renderer>().material.color = color2Button_.color;
				if (RandomMaterial == 3)
					pipes_[i].GetComponent<Renderer>().material.color = color3Button_.color;
			}
			else
				pipes_[i].GetComponent<Renderer>().material.color = Color.white;

	}

	private void ColoringOfButtons()
    {
        button1_.image.color = color1Button_.color;
        button2_.image.color = color2Button_.color;
        button3_.image.color = color3Button_.color;
    }

	private void CheckLose()
    {
		var pipeMaterialColor = pipes_[1].GetComponent<Renderer>().material.color;

		if (pipeMaterialColor != Color.black &&
            pipeMaterialColor != Color.white)
		{
			var playerColor = player_.GetComponent<Renderer>().material.color;
			if (pipeMaterialColor != playerColor)
			{
				textLose_.gameObject.SetActive(true);
			}
        }
	}

	private void AlignNextPipeWithOrigin() 
	{
		Transform transformToAlign = pipes_[1].transform;

		for (int i = 0; i < pipes_.Length; i++)
		{
			if (i != 1)
			{
				pipes_[i].transform.SetParent(transformToAlign);
			}
		}

		transformToAlign.localPosition = Vector3.zero;
		transformToAlign.localRotation = Quaternion.identity;

		for (int i = 0; i < pipes_.Length; i++)
		{
			if (i != 1)
			{
				pipes_[i].transform.SetParent(transform);
			}
		}
	}

	private void SetMaterialColorForLastPipe(int newLevel, int whitePipe)
	{
		var pipeMaterialColor = GetPipeMaterialColor(newLevel, whitePipe);
		pipes_[pipes_.Length - 1].SetMaterialColor(pipeMaterialColor);
	}

	private Color GetPipeMaterialColor(int whitePipe, int newLevel)
	{
		Color pipeMaterialColor;
		if (newLevel == 0)
		{
			pipeMaterialColor = Color.white;
		}
		else
		{
			if (whitePipe % 2 == 0)
			{
				RandomMaterial = Random.Range(1, 4);
				if (RandomMaterial == 1)
					pipeMaterialColor = color1_.color;
				else if (RandomMaterial == 2)
					pipeMaterialColor = color2_.color;
				else if (RandomMaterial == 3)
					pipeMaterialColor = color3_.color;
				else
					pipeMaterialColor = Color.white;

			}
			else
			{
				pipeMaterialColor = Color.white;
			}
		}

		return pipeMaterialColor;
	}

}