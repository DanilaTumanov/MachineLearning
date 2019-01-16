using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementTest
{

    public abstract class Gene
    {

        protected object _value;

        public Gene()
        {
            Randomize();
        }

        public Gene(object value)
        {
            _value = value;
        }


        public object GetValue()
        {
            return _value;
        }

        public T GetValue<T>()
        {
            return (T)_value;
        }

        public abstract void Randomize();

        public abstract Gene GetRandom();

    }



    public abstract class Gene<T>: Gene
    {

        public T Value {
            get {
                return (T)_value;
            }
        }



        public Gene(T value) : base(value) { }

        public Gene() : base() { }



        public new T GetValue()
        {
            return (T)_value;
        }


        public static bool operator == (Gene<T> gene1, Gene<T> gene2)
        {
            return gene1.Value.Equals(gene2.Value);
        }

        public static bool operator != (Gene<T> gene1, Gene<T> gene2)
        {
            return !gene1.Value.Equals(gene2.Value);
        }


        public override bool Equals(object gene2)
        {
            return Value.Equals((gene2 as Gene<T>).Value);
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<T>.Default.GetHashCode(Value);
        }
    }

}
