using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Perceptron
{

    private PerceptronInput[] _inputs;
    
    private float _bias;


    public Perceptron(byte inputsNum)
    {
        _inputs = new PerceptronInput[inputsNum];

        for(var i = 0; i < inputsNum; i++)
        {
            _inputs[i] = new PerceptronInput();
        }

        _bias = Random.Range(-0.99f, 0.99f);
    }



    public float Process(IEnumerable<float> inputs)
    {
        SetInputs(inputs);

        float res = 0;

        for(var i = 0; i < _inputs.Length; i++)
        {
            res += _inputs[i].input * _inputs[i].weight;
        }

        return res += _bias;
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
            _inputs[i].AdjustError(error);
        }

        _bias += error;
    }

}



public class PerceptronInput
{
    public float input;
    public float weight;

    public PerceptronInput()
    {
        weight = Random.Range(-0.99f, 0.99f);
    }

    public void AdjustError(float error)
    {
        weight = input * error + weight;
    }
}
