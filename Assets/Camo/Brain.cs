using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace MovementTest
{

    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class Brain : MonoBehaviour
    {

        public int DNALength = 1;
        public float timeAlive;
        public DNA dna;

        private ThirdPersonCharacter _character;
        private Vector3 _move;
        private bool _jump;
        private bool _alive = true;


        private void OnCollisionEnter(Collision obj)
        {
            if(obj.gameObject.tag == "dead")
            {
                _alive = false;
            }
        }


        public void Init()
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

        public void Init(DNA dna)
        {
            this.dna = dna;

            _character = GetComponent<ThirdPersonCharacter>();
            timeAlive = 0;
            _alive = true;
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
