using System.Collections;
using System.Collections.Generic;
using TutorialAssets.Scripts;
using UnityEngine;

namespace Monsters
{
    public class MulMonster : MonsterController, IQuestion
    {
        public int maxOperand1 = 50;
        public int maxMultiply = 12;

        public QuestionAnswer GenerateQuestion()
        {
            //Find Random numbers for operand
            int operand1 = Random.Range(1, maxOperand1);
            int operand2 = Random.Range(1, maxMultiply);

            return new QuestionAnswer($"{operand1} * {operand2} = ?", (operand1 * operand2).ToString());
        }

        }
    }