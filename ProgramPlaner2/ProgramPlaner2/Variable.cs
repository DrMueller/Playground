using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramPlaner2
{
    [Serializable]
    public class Variable
    {
        public String VariableType { get; private set; }
        public string VariableName { get; private set; }

        public Variable(String variableName, string variableType)
        {
            this.VariableName = variableName;
            this.VariableType = variableType;
        }
    }
}
