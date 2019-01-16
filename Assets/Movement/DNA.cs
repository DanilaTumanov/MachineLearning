using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MovementTest
{

    public class DNA
    {

        private Dictionary<Type, Gene> _genes = new Dictionary<Type, Gene>();


        public Dictionary<Type, Gene> Genes { get => _genes; }



        public DNA(List<Gene> genes)
        {
            SetGenes(genes);
        }


        private void SetGenes(List<Gene> genes)
        {
            genes.ForEach(gene => _genes.Add(gene.GetType(), gene));
        }


        public DNA Combine(DNA dna2)
        {
            var newGenes = new List<Gene>();

            foreach(var gene in _genes)
            {
                if (dna2.Genes.ContainsKey(gene.Key))
                {
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

            DNA mutatedDNA = new DNA(genes);

            return mutatedDNA;
        }


        public T GetGene<T>() where T: Gene
        {
            return (T)_genes[typeof(T)];
        }
    }


    

}
