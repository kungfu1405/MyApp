using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Interface
{
    public interface IMyDependency
    {
        public string WriteMessage(string message);
    }
}
