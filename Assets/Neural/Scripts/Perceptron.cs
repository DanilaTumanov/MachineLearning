using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Perceptron
{

    private PerceptronInput[] _inputs;
    
    private float _bias;

    private Func<float, float> _adapter;


    public Perceptron(byte inputsNum)
    {
        _inputs = new PerceptronInput[inputsNum];

        for(var i = 0; i < inputsNum; i++)
        {
            _inputs[i] = new PerceptronInput();
        }

        _bias = UnityEngine.Random.Range(-0.99f, 0.99f);
    }


    public Perceptron(byte inputsNum, Func<float, float> adapter): this(inputsNum)
    {
        _adapter = adapter;
    }


    public float Process(IEnumerable<float> inputs)
    {
        SetInputs(inputs);

        float res = 0;
        
        for(var i = 0; i < _inputs.Length; i++)
        {
            res += _inputs[i].input * _inputs[i].weight;
            //Debug.Log("weight " + (i + 1) + ": " + _inputs[i].weight);
        }

        res += _bias;

        //Debug.Log("bias: " + _bias);

        return _adapter != null ? _adapter(res) : res;
    }
    


    private void SetInputs(IEnumerable<float> inputs)
    {
        if(inputs.Count() != _inputs.Length)
        {
            Debug.LogError("Количество входных данных не соответствует требуемому (" + _inputs.Length + ")");
            return;
        }

        var inpArr = inputs.ToArray();

        for(var i = 0; i < inpArr.Length; i++)
        {
            _inputs[i].input = inpArr[i];
        }
    }
    

    public void AdjustError(float error)
    {


        for (var i = 0; i < _inputs.Length; i++)
        {
            //Debug.Log("Before adj: " + _inputs[i].weight);
            _inputs[i].AdjustError(error);
            //Debug.Log("Error: " + error + " ----> " + _inputs[i].weight);
        }

        _bias += error;

        //Debug.Log("BIAS: " + _bias);
    }

}



public class PerceptronInput
{
    public float input;
    public float weight;

    public PerceptronInput()
    {
        weight = UnityEngine.Random.Range(-0.99f, 0.99f);
    }

    public void AdjustError(float error)
    {
        weight = input * error + weight;
    }
}
