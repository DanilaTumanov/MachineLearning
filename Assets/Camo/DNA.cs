using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CamoTest
{

    public class DNA : MonoBehaviour
    {

        public Color color;
        public float size;

        public float timeToDie = 0;

        private SpriteRenderer SR;
        private Collider2D Coll2D;


        // Start is called before the first frame update
        void Start()
        {
            SR = GetComponent<SpriteRenderer>();
            Coll2D = GetComponent<Collider2D>();

            SR.color = color;
            transform.localScale = transform.localScale * size;
        }


        private void OnMouseDown()
        {
            timeToDie = PopulationManager.elapsed;
            //Debug.Log("Dead At: " + timeToDie);
            SR.enabled = false;
            Coll2D.enabled = false;
        }
    }

}

