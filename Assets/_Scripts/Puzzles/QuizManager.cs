using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public List<Q&A> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public Text QuestionTxt;
    // Start is called before the first frame update
    private void Start()
    {
        generateQuestion();
    }
    void SetAnswer(){
        for(int i=0; i < options.Lenght; i++)
        {
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answer[i];
        }

    }
    // Update is called once per frame
    void generateQuestion()
    {
        currentQuestion = Random.Range(0, QnA.Count);

        QuestionTxt.text = QnA[currentQuestion].Question;
    }
    
}
