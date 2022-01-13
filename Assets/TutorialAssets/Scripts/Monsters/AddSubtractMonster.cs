using System.Collections;
using System.Collections.Generic;
using TutorialAssets.Scripts;
using UnityEngine;

namespace Monsters
{
    public class AddSubtractMonster : MonsterController, IQuestion
    {
        public int maxOperand1 = 100;
        public int maxOperand2 = 100;

        public QuestionAnswer GenerateQuestion()
        {
            //Find Random numbers for operand
            int operand1 = Random.Range(1, maxOperand1);
            int operand2 = Random.Range(1, maxOperand2);

            QuestionAnswer qA;

            if (Random.value < 0.5f) // Returns a random float within [0.0 to 1.0] range is inclusive.
            {
                qA = new QuestionAnswer($"{operand1} + {operand2} =?", (operand1 + operand2).ToString());
            }
            else
            {
                qA = new QuestionAnswer($"{operand1} - {operand2} = ?", (operand1 - operand2).ToString());
            }

            return qA;
        }
    }
}