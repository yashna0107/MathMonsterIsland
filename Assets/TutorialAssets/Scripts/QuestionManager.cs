using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] MonsterManager monsterManager;
   [SerializeField] private TMP_Text messageBoxTextField;
    [SerializeField] TMP_InputField answerInputField;

    [SerializeField] private string answer;

    //create Event
    public event Action OnGameWin;

    // Start is called before the first frame update
    void Start()
    {

        GenerateQuestions();

        OnGameWin += () =>
        {
            answerInputField.text = "You've cleared the wave.";
            messageBoxTextField.text = "Well done! Wave Cleared.";
        };
    }

    void GenerateQuestions()
    {
        if(monsterManager.monsters.Count == 0)
        {
            OnGameWin?.Invoke(); //inline nullcheck
            return;
        }

        QuestionAnswer qA = monsterManager.monsters[0].GetComponent<IQuestion>().GenerateQuestion();
        messageBoxTextField.text = qA.question;
        answer = qA.answer;

        ClearInputField("Enter your answer: ");
    }

    public void ValidateAnswer()
    {
        if (answerInputField.text == answer)
        {
            monsterManager.KillMonster(0);
            monsterManager.MonsterAttack(0);
            monsterManager.MoveNextMonsterToQueue();
            GenerateQuestions();
        }
        else
        {
            ClearInputField(" Please Try Again!");
        }
    }

    void ClearInputField(string inputFieldPlaceHolder = "")
    {
        answerInputField.text = inputFieldPlaceHolder;
        answerInputField.ActivateInputField();
    }

}
