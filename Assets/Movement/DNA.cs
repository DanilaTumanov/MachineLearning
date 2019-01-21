using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MovementTest
{

    public class DNA
    {

        private Dictionary<string, Gene> _genes = new Dictionary<string, Gene>();


        public Dictionary<string, Gene> Genes => _genes;



        public DNA(List<Gene> genes)
        {
            SetGenes(genes);
        }


        private void SetGenes(List<Gene> genes)
        {
            genes.ForEach(gene => _genes.Add(gene.Name != null ? gene.Name : gene.GetType().ToString(), gene));
        }


        public DNA Combine(DNA dna2)
        {
            var newGenes = new List<Gene>();

            foreach(var gene in _genes)
            {
                if (dna2.Genes.ContainsKey(gene.Key))
                {
                    if (gene.Value.Name == "MovementTest.MovementGene" ||
                        dna2.Genes[gene.Key].Name == "MovementTest.MovementGene")
                    {
                        Debug.Log("Here");
                    }

                    newGenes.Add(UnityEngine.Random.Range(0, 2) == 0 ? gene.Value : dna2.Genes[gene.Key]);
                }
            }

            return new DNA(newGenes);
        }


        public DNA Mutate()
        {
            var genes = _genes.Values.ToList();
            int randIndex = UnityEngine.Random.Range(0, genes.Count());

            genes[randIndex] = genes[randIndex].GetRandom();
            
            return new DNA(genes);
        }


        public T GetGene<T>() where T: Gene
        {
            return (T)_genes[typeof(T).ToString()];
        }

        public T GetGene<T>(string name) where T : Gene
        {
            if (!_genes.ContainsKey(name))
            {
                Debug.Log(_genes.Count());
                _genes.Keys.ToList().ForEach(g => Debug.Log(g));
            }

            return (T)_genes[name];
        }
    }


    

}
