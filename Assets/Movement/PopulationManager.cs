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


        private List<Brain> population = new List<Brain>();
        private float trialTime = 5;
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
            GUI.Label(new Rect(10, 50, 200, 30), "Trial Time: " + (int)elapsed, guiStyle);
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
            List<Brain> sortedList = population.OrderBy(p => p.timeAlive).ToList();

            print("------->>>>>>>>>> GEN " + generation);
            sortedList.GroupBy(b => (b.dna.GetGene<MovementGene>().Value)).ToList().ForEach(g => print("MovementGene: " + g.Count() + " " + g.Key));

            population.Clear();
            mutationsCount = 0;

            for (int i = (int)(sortedList.Count / 2) - 1; i < sortedList.Count - 1; i++)
            {
                population.Add(Breed(sortedList[i], sortedList[i + 1]));
                population.Add(Breed(sortedList[i + 1], sortedList[i]));
            }

            sortedList.ForEach(p => Destroy(p.gameObject));
            generation++;
        }


        private Brain Breed(Brain brain1, Brain brain2)
        {
            DNA dna;
            Brain brain;

            // TODO: Мутация вызывает проблемы через несколько поколений - почти все становятся мутантами)
            if (Random.Range(0, 100) == 1)
            {
                dna = brain1.dna.Mutate();
                mutationsCount++;

                print("MUTATION: " + brain1.dna.GetGene<MovementGene>().Value + " -> " + dna.GetGene<MovementGene>().Value);
            }
            else
            {
                dna = brain1.dna.Combine(brain2.dna);

                //print(brain1.dna.GetGene<MovementGene>().Value + " + " + brain2.dna.GetGene<MovementGene>().Value + " = " + dna.GetGene<MovementGene>().Value);
            }

            //if(dna.GetGene<MovementGene>() != brain1.dna.GetGene<MovementGene>() 
            //    && dna.GetGene<MovementGene>() != brain2.dna.GetGene<MovementGene>())
            //{
            //    print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //}

            brain = InstantiatePerson();
            brain.Init(dna);

            return brain;
        }



        private Brain InstantiatePerson()
        {
            Vector3 pos = new Vector3(
                transform.position.x + Random.Range(-2, 3),
                transform.position.y, 
                transform.position.z + Random.Range(-2, 3));

            Brain brain = Instantiate(botPrefab, pos, Quaternion.identity).GetComponent<Brain>();

            return brain;
        }
    }

}


