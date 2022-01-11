using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
   [SerializeField] private TMP_Text messageBoxTextField;
    [SerializeField] TMP_InputField answerInputField;
    [SerializeField] private int answer;
    

    // Start is called before the first frame update
    void Start()
    {
        GenerateQuestions();
    }

    void GenerateQuestions()
    {
       AddSubtractQuestion(100, out string question, out answer);

        messageBoxTextField.text = question;
        ClearInputField();
    }

    void AddSubtractQuestion(int maxRange, out string question, out int qA)
    {
        //Find Random numbers for operand
        int operand1 = Random.Range(1, maxRange);
        int operand2 = Random.Range(1, maxRange);

        if (Random.value < 0.5f) // Returns a random float within [0.0 to 1.0] range is inclusive.
        {
            question = $"{operand1} + {operand2} = ? ";
            qA = operand1 + operand2;
        }
        else
        {
            question = $"{operand1} - {operand2} = ?";
            qA = operand1 - operand2;
        }

    }

    public void ValidateAnswer()
    {
        if (answerInputField.text == answer.ToString())
        {
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
