using System.Collections;
using System.Collections.Generic;
using TutorialAssets.Scripts;
using UnityEngine;

namespace Monsters
{
    public class DivMonster : MonsterController, IQuestion
    {
        public int maxOperand1 = 50;
        public int multiplyMax = 10;

        public QuestionAnswer GenerateQuestion()
        {
            //Find Random numbers for operand
            int operand1 = Random.Range(1, maxOperand1);
            int operand2 = Random.Range(1, multiplyMax);

            string question = $"{operand1} / {operand2} = ?";
            float answer = (float)operand1 / operand2;

            /*this block of code can be easily written using ternary operator as below.
             * if (answer % 1 == 0)
            {
                stringAnswer = answer.ToString(format: "0");

          //  bool exBool =2>1? "true": "false";
            }*/
      
            string stringAnswer = answer.ToString(answer % 1 == 0? "0" : "0.0");

            return new QuestionAnswer(question,stringAnswer);
        }

        }
}