using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MathGameScript : MonoBehaviour
{
    public TMP_InputField playerAnswer;
    public TextMeshProUGUI questionText;

    public int solution;
    public int problem;
    public bool impossible;

    string CreateNewQuestion()
    {
        int num1 = UnityEngine.Random.Range(0, 9);
        int num2 = UnityEngine.Random.Range(0, 9);
        int sign = UnityEngine.Random.Range(0, 1);
        if (sign == 0)
            solution = num1 + num2;
        else solution = num1 - num2;

        return string.Concat(new object[]
        {
            "SOLVE MATH Q",
            problem,
            $": \n \n",
            num1,
            sign == 0 ? "+" : "-",
            num2,
            "="
        });
    }

    string CreateImpossibleQuestion()
    {
        int num1 = UnityEngine.Random.Range(1, 9999);
        int num2 = UnityEngine.Random.Range(1, 9999);
        int num3 = UnityEngine.Random.Range(1, 9999);
        int sign = UnityEngine.Random.Range(0, 1);
        if (sign == 0)
        {
            return string.Concat(new object[]
                {
                    "SOLVE MATH Q",
                    problem,
                    ": \n",
                    num1,
                    "+(",
                    num2,
                    "X",
                    num3,
                    "="
                });
        }
        else
        {
            return string.Concat(new object[]
                {
                    "SOLVE MATH Q",
                    problem,
                    ": \n (",
                    num1,
                    "/",
                    num2,
                    ")+",
                    num3,
                    "="
                });
        }
    }

    void NewProblem()
    {
        problem++;
        impossible = problem >= 3 && GameManager.current.notebooks >= 2;

        playerAnswer.text = string.Empty;
        questionText.text = impossible ? CreateImpossibleQuestion() : CreateNewQuestion();
        playerAnswer.ActivateInputField();
    }

    public void CheckAnswer()
    {
        if (impossible)
        {
            if (!GameManager.current.spoopMode)
                GameManager.current.ActivateSpoopMode();
        }

        if (playerAnswer.text != solution.ToString())
        {
            return;
        }

        NewProblem();
    }

    private void Start()
    {
        NewProblem();
    }
}
