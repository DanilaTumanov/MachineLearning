using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace MovementTest
{

    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class StraightWalkBrain : Brain
    {

        private ThirdPersonCharacter _character;
        private Vector3 _move;
        private bool _jump;
       

        private void Awake()
        {
            _character = GetComponent<ThirdPersonCharacter>();
        }


        public override void Init()
        {
            Init(
                new DNA(
                    new List<Gene>()
                    {
                        new MovementGene(),
                        new RotationGene()
                    }
                )
            );
        }


        private void FixedUpdate()
        {
            //float horizontal = 0;
            //float vertical = 0;
            //bool crouch = false;

            //switch (dna.GetGene<MovementGene>().Value)
            //{
            //    case Movement.Forward:
            //        vertical = 1;
            //        break;
            //    case Movement.Back:
            //        vertical = -1;
            //        break;
            //    case Movement.Left:
            //        horizontal = -1;
            //        break;
            //    case Movement.Right:
            //        horizontal = 1;
            //        break;
            //}


            var rotation = dna.GetGene<RotationGene>().Value;
            _move = Vector3.forward * Mathf.Cos(rotation) + Vector3.right * Mathf.Sin(rotation);
            _character.Move(_move, false, _jump);
            _jump = false;

            if (_alive)
                timeAlive += Time.fixedDeltaTime;
        }

    }

}


