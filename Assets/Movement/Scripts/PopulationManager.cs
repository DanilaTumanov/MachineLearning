using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MovementTest
{

    public class PopulationManager : MonoBehaviour
    {

        public static float elapsed;

        public GameObject botPrefab;
        public int populationSize = 50;

        [Range(0, 100)]
        public int mutationChance = 1;
        public float trialTime = 5;
        public float trialTimeIcrease = 0;

        public Vector2Int xBounds = new Vector2Int(-2, 2);
        public Vector2Int zBounds = new Vector2Int(-2, 2);

        private List<Brain> population = new List<Brain>();
        private int generation = 1;
        private int mutationsCount = 0;



        GUIStyle guiStyle = new GUIStyle();

        private void OnGUI()
        {
            guiStyle.fontSize = 20;
            guiStyle.normal.textColor = Color.white;
            GUI.BeginGroup(new Rect(10, 10, 250, 150));
            GUI.Box(new Rect(0, 0, 140, 140), "Stats", guiStyle);
            GUI.Label(new Rect(10, 25, 200, 30), "Generation: " + generation, guiStyle);
            GUI.Label(new Rect(10, 50, 200, 30), "Trial Time: " + (int) (trialTime - elapsed), guiStyle);
            GUI.Label(new Rect(10, 75, 200, 30), "Population: " + population.Count, guiStyle);
            GUI.Label(new Rect(10, 100, 200, 30), "Mutations: " + mutationsCount, guiStyle);
            GUI.EndGroup();
        }



        // Start is called before the first frame update
        void Start()
        {
            Brain brain;

            for (int i = 0; i < populationSize; i++)
            {
                brain = InstantiatePerson();
                brain.Init();
                population.Add(brain);
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
            List<Brain> sortedList = population.OrderBy(
                p =>
                    p.timeAlive + 
                    (p.distance/* + Vector3.Distance(transform.position, p.transform.position)*/)
                ).ToList();
            
            population.Clear();
            mutationsCount = 0;

            for (int i = (int)(sortedList.Count / 2) - 1; i < sortedList.Count - 1; i++)
            {
                population.Add(Breed(sortedList[i], sortedList[i + 1]));
                population.Add(Breed(sortedList[i + 1], sortedList[i]));
            }

            sortedList.ForEach(p => Destroy(p.gameObject));
            generation++;
            trialTime += trialTimeIcrease;
        }


        private Brain Breed(Brain brain1, Brain brain2)
        {
            DNA dna;
            Brain brain;
            

            if (Random.Range(0, 100) < mutationChance)
            {
                dna = brain1.dna.Mutate();
                mutationsCount++;
            }
            else
            {
                dna = brain1.dna.Combine(brain2.dna);
            }

            brain = InstantiatePerson();
            brain.Init(dna);

            return brain;
        }



        private Brain InstantiatePerson()
        {
            Vector3 pos = new Vector3(
                transform.position.x + Random.Range(xBounds.x, xBounds.y + 1),
                transform.position.y, 
                transform.position.z + Random.Range(zBounds.x, zBounds.y + 1));

            Brain brain = Instantiate(botPrefab, pos, Quaternion.Euler(0, Random.Range(0, 4) * 90, 0)).GetComponent<Brain>();

            return brain;
        }
    }

}


