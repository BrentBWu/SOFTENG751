using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {

    public Question[] questions;
    private static List<Question> unansweredQuestions;
    private Question currentQuestion;

    [SerializeField]

    // Use this for initialization
    void Start() {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0) {
            unansweredQuestions = questions.ToList<Question>();
        }
        SetCurrentQuestion();
        Debug.Log(currentQuestion.fact + " is " + currentQuestion.isTrue);
    }

    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];
        unansweredQuestions.RemoveAt(randomQuestionIndex);
    }
}
