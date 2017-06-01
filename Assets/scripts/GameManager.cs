using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[System.Serializable]
	public class Question
	{
		public int optionNumber;
		public string question;
		public string[] options;
		public int correctAnswerIndex;

	}

    public Question[] questions;
    private static List<Question> unansweredQuestions;
    private Question currentQuestion;
	public Text questionText;
	public GameObject questionOption;
	public int startIndex, endIndex, currentIndex;

    [SerializeField]
    private float timeBetweenQuestions = 1f;

    [SerializeField]
    private Animator animator;

    // Use this for initialization
    void Start() {
		loadQuiz ();
		this.startIndex = GameObject.Find ("ResourceManager").GetComponent<ResourceManage> ().startIndex;
		this.endIndex = GameObject.Find ("ResourceManager").GetComponent<ResourceManage> ().endIndex;
		this.currentIndex = startIndex;
		setCurrentQuestion (currentIndex);
        /*if (unansweredQuestions == null || unansweredQuestions.Count == 0) {
            unansweredQuestions = questions.ToList<Question>();
        }*/
        //SetCurrentQuestion();
    }

	void loadQuiz(){
		TextAsset[] quizs = GameObject.Find ("ResourceManager").GetComponent<ResourceManage> ().getQuiz ();
		questions = new Question[quizs.Length];
		for (int i = 0; i < quizs.Length; i++) {
			string wholeFile = quizs[i].text;
			List<string> eachLine = new List<string>();
			eachLine.AddRange (wholeFile.Split ("\n" [0]));

			Question q = new Question ();

			q.optionNumber = int.Parse (eachLine [0].Trim());
			q.question = eachLine [1];
			q.options = new string[int.Parse (eachLine [0])];
			q.correctAnswerIndex = int.Parse(eachLine [int.Parse (eachLine [0]) + 2]);
			for (int j = 0; j < q.optionNumber; j++) {
				q.options [j] = eachLine [j + 2];
			}
			questions [i] = q;
		}
	}

	public void chooseQuestionSet(int startIndex, int endIndex){
		
	}

	void setCurrentQuestion(int questionIndex){
		currentQuestion = questions [questionIndex];
		questionText.text = currentQuestion.question;
		for (int i = 0; i < currentQuestion.optionNumber; i++) {
			GameObject option = Instantiate (questionOption);
			option.transform.Find ("Button Layer").transform.Find ("Button Text").transform.GetComponent<Text> ().text = currentQuestion.options [i];
			option.transform.SetParent (GameObject.Find ("Option Panel").transform);
		}
    }

    IEnumerator TransitionToNextQuestion() {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	public void checkAnswer(){
		foreach (Transform child in GameObject.Find("Option Panel").transform) {
			//Check the correctness of chosen option
			if (child.transform.GetComponent<Option> ().chose) {
				if (child.GetSiblingIndex() == currentQuestion.correctAnswerIndex) {
					child.Find ("Button Layer").Find ("Button Text").transform.GetComponent<Text> ().color = new Color (0, 255, 0);
				} else {
					child.Find ("Button Layer").Find ("Button Text").transform.GetComponent<Text> ().color = new Color (255, 0, 0);
				}
				break;
			}
		}

		//Set text color of correct option
		GameObject.Find ("Option Panel").transform.GetChild (currentQuestion.correctAnswerIndex).Find ("Button Layer").Find ("Button Text").transform.GetComponent<Text> ().color = new Color (0, 255, 0);

	}

	public void nextQuestion(){
		if (currentIndex < endIndex) {
			currentIndex++;
			foreach (Transform child in GameObject.Find("Option Panel").transform) {
				GameObject.Destroy (child.gameObject);
			}
			setCurrentQuestion (currentIndex);
		}
	}
		
}
