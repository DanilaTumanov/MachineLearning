using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MovementTest
{

    public class RotationGene : Gene<int>
    {

        public override Gene GetRandom()
        {
            return new RotationGene();
        }

        public override void Randomize()
        {
            _value = Random.Range(0, 360);
        }

    }

}
