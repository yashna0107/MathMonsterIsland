using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
   [SerializeField] private TMP_Text messageBoxTextField;
    [SerializeField] TMP_InputField answerInputField;

    private string question;
    [SerializeField] private int answer;

    // Start is called before the first frame update
    void Start()
    {
        //Find Random numbers for operand
        int operand1 = Random.Range(1, 100);
        int operand2 = Random.Range(1, 100);
       
        if(Random.value < 0.5f) // Returns a random float within [0.0 to 1.0] range is inclusive.
        {
            question = $"{operand1} + {operand2} = ? ";
            answer = operand1 + operand2;
        }
        else
        {
             question= $"{operand1} - {operand2} = ?";
             answer = operand1 - operand2;
        }

        messageBoxTextField.text = question;
        answerInputField.Select();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
