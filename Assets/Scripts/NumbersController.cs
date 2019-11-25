using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumbersController : MonoBehaviour
{
    public static NumbersController _instance;

    public int result;
    public Text resultText;

    [Header("Reference to sprite with ball numebers")]
    public Sprite[] blueNumbers = new Sprite[10];
    public Sprite[] redNumbers = new Sprite[10];
    public Sprite[] greenNumbers = new Sprite[10];
    public Sprite[] yellowNumbers = new Sprite[10];


    public void Awake() => _instance = this;

    // Start is called before the first frame update
    void Start()
    {
        result = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClearResult()
    {
        result = -1;
        resultText.text = "";
    }

    public void NumberPressed(int value)
    {
        if (result == -1)
        {
            result = value;
            resultText.text = value.ToString();
        } else if (result != -1 && resultText.text.Length == 1)
        {
            string doubleDigitResult = string.Concat(resultText.text, value.ToString());
            resultText.text = doubleDigitResult;
            result = int.Parse(doubleDigitResult);
        }
    }
}
