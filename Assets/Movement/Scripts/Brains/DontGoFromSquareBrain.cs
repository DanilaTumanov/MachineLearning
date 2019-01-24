using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace MovementTest
{
    
    public class DontGoFromSquareBrain : Brain
    {

        public Transform eyes;
        public LayerMask deadLayerMask;
        public float speed;

        private bool _seeGround;
       
        

        public override void Init()
        {
            Init(
                new DNA(
                    new List<Gene>()
                    {
                        new MovementGene("SeeGroundMove"),
                        new MovementGene("DontSeeGroundMove"),
                    }
                )
            );
        }


        private void Update()
        {
            if (!_alive)
                return;


            _seeGround = false;

            if(Physics.Raycast(eyes.position, eyes.forward, out RaycastHit hitInfo, 10))
            {
                _seeGround = 1 << hitInfo.collider.gameObject.layer == deadLayerMask.value;
            }


            var geneValue = _seeGround ? dna.GetGene<MovementGene>("SeeGroundMove").Value : dna.GetGene<MovementGene>("DontSeeGroundMove").Value;
            float turn = 0;
            float move = 0;

           
            switch (geneValue)
            {
                case Movement.Forward:
                    move = 1;
                    break;
                case Movement.Left:
                    turn = -90;
                    break;
                case Movement.Right:
                    turn = 90;
                    break;
                case Movement.Turn180:
                    turn = 180;
                    break;
            }

            transform.Translate(0, 0, move * Time.deltaTime * speed);
            transform.Rotate(0, turn, 0);


            distance += move;
            timeAlive += Time.deltaTime;
        }

    }

}


