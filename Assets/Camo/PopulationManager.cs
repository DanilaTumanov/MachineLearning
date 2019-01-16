using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace CamoTest
{

    public class PopulationManager : MonoBehaviour
    {
        public static float elapsed;

        public GameObject personPrefab;
        public int populationSize = 10;


        private List<DNA> population = new List<DNA>();
        private float trialTime = 10;
        private int generation = 1;



        GUIStyle guiStyle = new GUIStyle();

        private void OnGUI()
        {
            guiStyle.fontSize = 20;
            guiStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
            GUI.Label(new Rect(10, 35, 100, 20), "Trial Time: " + (int)elapsed, guiStyle);
        }



        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < populationSize; i++)
            {
                population.Add(InstantiatePerson(
                    color: new Color(
                        r: UnityEngine.Random.Range(0f, 1f),
                        g: UnityEngine.Random.Range(0f, 1f),
                        b: UnityEngine.Random.Range(0f, 1f)
                    ),
                    size: UnityEngine.Random.Range(0.3f, 1.7f)
                ));
            }
        }

        // Update is called once per frame
        void Update()
        {
            elapsed += Time.deltaTime;
            if (elapsed > trialTime)
            {
                BreedNewPopulation();
                elapsed = 0;
            }
        }



        private void BreedNewPopulation()
        {
            List<DNA> sortedList = population.OrderBy(p => p.timeToDie).ToList();

            population.Clear();

            for (int i = (int)(sortedList.Count / 2) - 1; i < sortedList.Count - 1; i++)
            {
                population.Add(Breed(sortedList[i], sortedList[i + 1]));
                population.Add(Breed(sortedList[i + 1], sortedList[i]));
            }

            sortedList.ForEach(p => Destroy(p.gameObject));
            generation++;
        }


        private DNA Breed(DNA dna1, DNA dna2)
        {
            Color color;
            float size;

            if (UnityEngine.Random.Range(0, 10) < 1)
            {
                color = new Color(
                    r: UnityEngine.Random.Range(0f, 1f),
                    g: UnityEngine.Random.Range(0f, 1f),
                    b: UnityEngine.Random.Range(0f, 1f)
                );
            }
            else
            {
                color = new Color(
                    r: UnityEngine.Random.Range(0, 1) == 0 ? dna1.color.r : dna2.color.r,
                    g: UnityEngine.Random.Range(0, 1) == 0 ? dna1.color.g : dna2.color.g,
                    b: UnityEngine.Random.Range(0, 1) == 0 ? dna1.color.b : dna2.color.b
                );
            }

            if (UnityEngine.Random.Range(0, 10) < 1)
            {
                size = UnityEngine.Random.Range(0.3f, 1.7f);
            }
            else
            {
                size = UnityEngine.Random.Range(0, 1) == 0 ? dna1.size : dna2.size;
            }


            return InstantiatePerson(color, size);
        }


        private DNA InstantiatePerson(Color color, float size)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-4, 4), 0);
            DNA dna = Instantiate(personPrefab, pos, Quaternion.identity).GetComponent<DNA>();

            dna.color = color;
            dna.size = size;

            return dna;
        }
    }

}


