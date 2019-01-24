using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace MovementTest
{
    
    public class MazeWalkerBrain : Brain
    {

        public Transform eyes;
        public LayerMask obstacleMask;
        public float speed;


        private Transform _populationManager;
        private bool _seeObstacle;
       
        

        public override void Init()
        {
            Init(
                new DNA(
                    new List<Gene>()
                    {
                        new RotationGene("dontSeeObstacle"),
                        new RotationGene("seeObstacle")
                    }
                )
            );
        }


        private void Start()
        {
            _populationManager = FindObjectOfType<PopulationManager>().transform;
        }


        private void Update()
        {
            if (!_alive)
                return;


            _seeObstacle = false;

            if(Physics.Raycast(eyes.position, eyes.forward, out RaycastHit hitInfo, 0.5f))
            {
                _seeObstacle = 1 << hitInfo.collider.gameObject.layer == obstacleMask.value;
            }


            var geneValue = _seeObstacle ? dna.GetGene<RotationGene>("seeObstacle").Value : dna.GetGene<RotationGene>("dontSeeObstacle").Value;
            

            transform.Translate(0, 0, Time.deltaTime * speed);
            //transform.Rotate(0, geneValue, 0);
            transform.Rotate(0, _seeObstacle ? geneValue : 0, 0);


            var currDist = Vector3.Distance(_populationManager.position, transform.position);
            distance = currDist > distance ? currDist : distance;
            timeAlive += Time.deltaTime;
        }

    }

}


