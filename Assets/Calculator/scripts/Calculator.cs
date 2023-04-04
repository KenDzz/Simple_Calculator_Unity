using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    public TMP_InputField InputCalculator;
    public TMP_InputField InputOperation;
    public AudioSource audioClick;
    public AudioSource audioResult;


    private float result = 0;
    private bool onCal = false;
    private string m_valueOperation = "";
    private string m_value = "";
    private string m_value2 = "";
    private int numClickNumber = 0;
    private string fullText = "Developer By KenDzz";
    private string currentText = "";
    private float typeSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void clickNumber(int value)
    {
        Debug.Log(string.Format("Number: {0}", value));
        if (this.onCal)
        {
            audioClick.Play();
            if (value > 9 || value < 0)
            {
                InputCalculator.text = "Erorr!";
                Invoke("setValueOnCalculator", 1);
            }
            if (this.m_valueOperation != "")
            {
                this.numClickNumber++;
            }

            if (m_value == "" && this.numClickNumber == 0)
            {
                m_value = value.ToString();
                InputCalculator.text = m_value.ToString();
            }
            else if (m_value != "" && this.numClickNumber == 0)
            {
                m_value = m_value.ToString() + value;
                InputCalculator.text = m_value.ToString();

            }
            else
            {
                if (m_value2 != "" && this.numClickNumber == 1)
                {
                    m_value2 = value.ToString();
                    InputCalculator.text = m_value2.ToString();
                }
                else
                {
                    m_value2 = m_value2.ToString() + value;
                    InputCalculator.text = m_value2.ToString();
                }
            }
        }
    }

    private void resetCal()
    {
        m_valueOperation = "";
        m_value = "";
        m_value2 = "";
        this.numClickNumber = 0;
    }

    public void clickOperation(string value)
    {
        Debug.Log(string.Format("Operation: {0}", value));
        if (this.onCal)
        {
            audioClick.Play();
            if (value.All(c => "+-*/".Contains(c))) {
                InputOperation.text = value;
                this.m_valueOperation = value;
                if (this.result > 0)
                {
                    this.m_value = this.result.ToString();
                    this.numClickNumber++;
                }
            }
        }
    }

    public void clickCalculator()
    {
        Debug.Log("clickCalculator");
        if (this.onCal)
        {
            audioResult.Play();
            InputOperation.text = "=";
            if (this.m_valueOperation != "")
            {
                switch (this.m_valueOperation)
                {
                    case "+":
                        this.result = float.Parse(this.m_value) + float.Parse(this.m_value2);
                        break;
                    case "-":
                        this.result = float.Parse(this.m_value) - float.Parse(this.m_value2);
                        break;
                    case "*":
                        this.result = float.Parse(this.m_value) * float.Parse(this.m_value2);
                        break;
                    case "/":
                        this.result = float.Parse(this.m_value) / float.Parse(this.m_value2);
                        break;
                }
                InputCalculator.text = this.result.ToString();
                this.resetCal();
            }
        }
    }

    public void clickPeriod(string value)
    {
        Debug.Log(string.Format("Period: {0}", value));
        if (this.onCal) {
            if (this.numClickNumber == 0)
            {
                this.m_value = this.m_value.ToString() + value;
            }
            else
            {
                this.m_value2 = this.m_value2.ToString() + value;
            }
        }
    }

    public void clickOn()
    {
        Debug.Log("Click On");
        this.numClickNumber = 0;
        //InputCalculator.text = "Developer By KenDzz";
        StartCoroutine(Type());
        this.onCal = true;
    }

    private IEnumerator Type()
    {
        this.currentText = "";
        foreach (char c in fullText)
        {
            currentText += c;
            InputCalculator.text = currentText;
            yield return new WaitForSeconds(typeSpeed);
        }
        Invoke("setValueOnCalculator", 1);
    }

    public void setValueOnCalculator()
    {
        if (this.onCal)
        {
            InputCalculator.text = "0";
        }
    }

    public void clickOff()
    {
        Debug.Log("Click Off");
        InputCalculator.text = "";
        InputOperation.text = "";
        this.resetCal();
        this.result = 0;
        this.onCal = false;
    }

    public void clickC()
    {
        Debug.Log("Click C");
        if (this.onCal)
        {
            InputCalculator.text = "0";
            InputOperation.text = "";
            this.resetCal();
            this.result = 0;
        }
    }
}
