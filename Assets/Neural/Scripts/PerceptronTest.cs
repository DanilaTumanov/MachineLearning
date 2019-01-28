using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptronTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var OrPerceptron = new Perceptron(2, res => res >= 0 ? 1 : 0);

        var OrTrainer = new PerceptronTrainer(
            OrPerceptron,
            new TrainSet()
            {
                rows = new TrainRow[4] {
                    new TrainRow()
                    {
                        inputs = new float[2] {0, 0},
                        expectation = 0
                    },
                    new TrainRow()
                    {
                        inputs = new float[2] {0, 1},
                        expectation = 1
                    },
                    new TrainRow()
                    {
                        inputs = new float[2] {1, 0},
                        expectation = 1
                    },
                    new TrainRow()
                    {
                        inputs = new float[2] {1, 1},
                        expectation = 1
                    }
                }
            },
            4
        );


        OrTrainer.Train();

    }
    
}
