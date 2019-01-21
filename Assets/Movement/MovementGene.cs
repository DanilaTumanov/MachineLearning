using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MovementTest
{

    public class MovementGene : Gene<Movement>
    {
        public MovementGene() : base() { }

        public MovementGene(Movement value) : base(value) { }

        public MovementGene(string name) : base(name)
        {
        }

        public override Gene GetRandom()
        {
            return new MovementGene(Name);
        }

        public override void Randomize()
        {
            var GeneValues = Enum.GetValues(typeof(Movement));

            _value = (Movement) GeneValues.GetValue(
                UnityEngine.Random.Range(0, GeneValues.Length)
            );
        }


    }


    public enum Movement
    {
        Forward,
        Left,
        Right,
        Turn180
    }

}
