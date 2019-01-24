using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MovementTest
{

    public class RotationGene : Gene<int>
    {
        public RotationGene() : base()
        {
        }

        public RotationGene(string name) : base(name)
        {
        }

        public RotationGene(int value) : base(value)
        {
        }

        public override Gene GetRandom()
        {
            return new RotationGene(Name);
        }

        public override void Randomize()
        {
            _value = Random.Range(0, 360);
        }

    }

}
