using MvcMovie.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Myclass
{
    public class MyDependency: IMyDependency
    {
        public string WriteMessage(string message)
        {
            return $"MyDependency.WriteMessage called. Message: {message}";
        }
    }
}
