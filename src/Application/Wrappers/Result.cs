using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappers
{
    public class Result<T>
    {
        public T Value { get; set; }
    }
    public class Result : Result<int> { }
}
