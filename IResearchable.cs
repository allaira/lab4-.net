using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_3_students
{
    public interface IResearchable
    {
        string ThesisTopic { get; }
        int PublicationsCount { get; }
    }
}
