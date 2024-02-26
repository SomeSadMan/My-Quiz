using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour
{

    public Question[] questions;
    public static List<Question> unAnsweredQ;
    private Question currentQ;

    [SerializeField] private Text qText;
    [SerializeField] private float timeToNextQuestion;
    [SerializeField] private Animator animator;
    [SerializeField] private Text trueOrFalseAnswer;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject face;
    
    public static int score = 0;
   

    void Start()
    {
        

        if (unAnsweredQ == null || unAnsweredQ.Count == 0)
        {
            unAnsweredQ = questions.ToList<Question>();
        }
        scoreText.text = score.ToString();
        AssignRandQUestion();
        WinScreen();
    }

    void AssignRandQUestion()
    {
        int randQ = Random.Range(0, unAnsweredQ.Count);
        currentQ = unAnsweredQ[randQ];
        qText.text = currentQ.randomFact;

        
    }

    public void SelectTrue()
    {
        buttons[0].SetActive(false);
        buttons[1].SetActive(false);

        
        if (currentQ.isTrue )
        {
            trueOrFalseAnswer.color = Color.green;
            trueOrFalseAnswer.text = "Correct";
            score++;
            scoreText.text = "" + score;
        }
        else
        {
            trueOrFalseAnswer.color = Color.red;
            trueOrFalseAnswer.text = "Wrong";
        }
        StartCoroutine(ToNextQuestion());
        
    }
    public void SelectFalse()
    {
        buttons[0].SetActive(false);
        buttons[1].SetActive(false);

        if (!currentQ.isTrue)
        {
            trueOrFalseAnswer.color = Color.green;
            trueOrFalseAnswer.text = "Correct";
            score++;
            scoreText.text = "" + score;
            

        }
        else
        {
            trueOrFalseAnswer.color = Color.red;
            trueOrFalseAnswer.text = "Wrong";
        }
        StartCoroutine(ToNextQuestion());
        
    }

    IEnumerator ToNextQuestion()
    {
        unAnsweredQ.Remove(currentQ);
        yield return new WaitForSeconds(timeToNextQuestion);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void WinScreen()
    {
        

        if (score == 15 )
        {
            face.SetActive(true);
            animator.SetBool("True", true );
            qText.text = "CONGRATULATIONS";
            buttons[0].SetActive(false);
            buttons[1].SetActive(false);
            winScreen.SetActive(true);
            
            
        }

    }


}
