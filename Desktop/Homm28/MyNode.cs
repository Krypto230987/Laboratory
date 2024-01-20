using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homm28;
namespace Homm28
{
    public class MyNode<T>
    {
        public T Value { get; set; }
        public MyNode<T> Next { get; set; }
        public MyNode<T> Previous { get; set; }

        public MyNode(T value)
        {
            Value = value;
        }
    }
}
