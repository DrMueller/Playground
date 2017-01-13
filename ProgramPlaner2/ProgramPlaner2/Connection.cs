using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramPlaner2
{
    [Serializable]
    public class Connection
    {
        public SectionClass ConnectionTarget { get; private set; }
        public ConnectionType ConnectionType { get; private set; }

        public Connection(SectionClass connectionTaget, ConnectionType connectionType)
        {
            this.ConnectionType = connectionType;
            this.ConnectionTarget = connectionTaget;
        }
    }
}
