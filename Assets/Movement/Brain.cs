using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace MovementTest
{
    
    public abstract class Brain : MonoBehaviour
    {
        
        public float timeAlive;
        public DNA dna;
        public float distance;

        protected bool _alive = true;


        private void OnCollisionEnter(Collision obj)
        {
            if(obj.gameObject.tag == "dead")
            {
                _alive = false;
            }
        }


        public abstract void Init();
        

        public void Init(DNA dna)
        {
            this.dna = dna;
            
            timeAlive = 0;
            distance = 0;
            _alive = true;
        }
        

    }

}
