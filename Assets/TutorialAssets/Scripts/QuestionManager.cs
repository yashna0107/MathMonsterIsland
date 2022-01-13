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
    

    // Start is called before the first frame update
    void Start()
    {
        GenerateQuestions();
    }

    void GenerateQuestions()
    {
        QuestionAnswer qA = monsterManager.monsters[0].GetComponent<IQuestion>().GenerateQuestion();

        messageBoxTextField.text = qA.question;
        answer = qA.answer;
        ClearInputField();
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
            ClearInputField();
        }
    }

    void ClearInputField()
    {
        answerInputField.text = "";
        answerInputField.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
