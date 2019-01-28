using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptronTrainer
{

    private Perceptron _perceptron;
    private TrainSet _trainSet;
    private int _trainingIterations;
    private float _allowableError = 0;


    public PerceptronTrainer(Perceptron perceptron, TrainSet trainSet, int trainingIterations)
    {
        _trainSet = trainSet;
        _perceptron = perceptron;
        _trainingIterations = trainingIterations;
    }


    public PerceptronTrainer(Perceptron perceptron, TrainSet trainSet, int trainingIterations, float allowableError)
        : this(perceptron, trainSet, trainingIterations)
    {
        _allowableError = allowableError;
    }


    public void Train()
    {
        for(var i = 0; i < _trainingIterations; i++)
        {
            Debug.Log("TRAIN " + i + " -----------");
            ProcessEpoch();
        }
    }

    private void ProcessEpoch()
    {
        for(var i = 0; i < _trainSet.rows.Length; i++)
        {
            var res = _perceptron.Process(_trainSet.rows[i].inputs);
            var error = _trainSet.rows[i].expectation - res;

            Debug.Log(
                "Epoch " + i + ": " +
                _trainSet.rows[i].inputs[0] + " || " +
                _trainSet.rows[i].inputs[1] + " => " +
                res + " error: " +
                error
            );

            if(Mathf.Abs(error) > _allowableError)
            {
                _perceptron.AdjustError(error);
            }
        }
    }

}



public class TrainSet
{
    public TrainRow[] rows;
}


public class TrainRow
{
    public float[] inputs;
    public float expectation;
}
