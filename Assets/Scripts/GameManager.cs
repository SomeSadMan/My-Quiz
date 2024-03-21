using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using MaterialUI;
using YG;



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
    [SerializeField] private GameObject rPoints;
    [SerializeField] private Text reachedPoints;
    [SerializeField] private Text playerHealth;
    
    public static int score = 0;
    private static int health = 3;
   

    void Start()
    {
        

        if (unAnsweredQ == null || unAnsweredQ.Count == 0)
        {
            unAnsweredQ = questions.ToList<Question>();
        }
        scoreText.text = score.ToString();
        playerHealth.text = health.ToString();

        AssignRandQUestion();
        WinScreen();
        YouLose();

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
            trueOrFalseAnswer.text = "Правильно";
            score++;
            scoreText.text = "" + score;
        }
        else
        {
            health--;
            trueOrFalseAnswer.color = Color.red;
            trueOrFalseAnswer.text = "Неправильно";
            Debug.Log(health);
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
            trueOrFalseAnswer.text = "Правильно";
            score++;
            scoreText.text = "" + score;
            

        }
        else
        {
            health--;
            trueOrFalseAnswer.color = Color.red;
            trueOrFalseAnswer.text = "Неправильно";
            Debug.Log(health);
        }
        StartCoroutine(ToNextQuestion());
        
    }

    public void TryAgain()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        health = 3;
        score = 0;
        ShowAd();

    }

    IEnumerator ToNextQuestion()
    {
        unAnsweredQ.Remove(currentQ);
        yield return new WaitForSeconds(timeToNextQuestion);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void WinScreen()
    {
        

        if (score == 116 )
        {
            
            animator.SetBool("True", true );
            qText.text = "ПОЗДРАВЛЯЮ!";
            buttons[0].SetActive(false);
            buttons[1].SetActive(false);
            winScreen.SetActive(true);
            reachedPoints.color = Color.green;
            reachedPoints.text = "Вы набрали " + score + " баллов";
            buttons[2].SetActive(true);
            rPoints.SetActive(true);



        }

    }

    void YouLose()
    {
        if (health == 0)
        {
            qText.text = "Ой, ты истратил все жизни";
            buttons[0].SetActive(false);
            buttons[1].SetActive(false);
            buttons[2].SetActive(true);
            reachedPoints.color= Color.red;
            reachedPoints.text = "Вы набрали " + score + " баллов";
            rPoints.SetActive(true);
            


        }
    }
    
    public void ShowAd()
    {
        YandexGame.FullscreenShow();
    }
}



