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

    [SerializeField]
    private float timeBetweenQuestions = 1f;

    [SerializeField]
    private Animator animator;

    // Use this for initialization
    void Start() {
		loadQuiz ();
		SetCurrentQuestion (0);
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
			Debug.Log (questions.Length);
			foreach (string s in eachLine) {
				Debug.Log (s);
			}
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

	/*public void chooseQuestionSet(int startIndex, int endIndex){
		for (int i = startIndex; i < endIndex; i++) {
			
		}
	}*/

	void SetCurrentQuestion(int questionIndex){
		Question q = questions [questionIndex];
		questionText.text = q.question;
		for (int i = 0; i < q.optionNumber; i++) {
			GameObject option = Instantiate (questionOption);
			option.transform.Find ("Button Layer").transform.Find ("Button Text").transform.GetComponent<Text> ().text = q.options [i];
			option.transform.SetParent (GameObject.Find ("Option Panel").transform);
		}
    }

    IEnumerator TransitionToNextQuestion() {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue() {
        animator.SetTrigger("True");
        StartCoroutine(TransitionToNextQuestion());
 
    }

    public void UserSelectFalse(){
        animator.SetTrigger("False");
        StartCoroutine(TransitionToNextQuestion());
    }
}
