using System;

namespace Magisterka.Infrastructure.Shared.Generic
{
    public class GenericPair<T> 
        where  T : class
    {
        public T First { get; set; }

        public T Second { get; set; }

        public GenericPair()
        {
        }

        public GenericPair(T first, T second)
        {
            First = first;
            Second = second;
        }


        public override bool Equals(object obj)
        {
            var cast = obj as GenericPair<T>;

            if (cast == null)
            {
                return false;
            }
            if (cast.First == this.First && cast.Second == this.Second)
            {
                return true;
            }

            return false;
        }

     
    }
}