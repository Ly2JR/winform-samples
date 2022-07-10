using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControlSamples.Models
{
    public class ProjectSetKey
    {
        public string Type { get; set; }

        public int Sn { get; set; }

        public ProjectSetKey() { }
        public ProjectSetKey(string type, int sn)
        {
            this.Type = type;
            this.Sn = sn;
        }

        public override string ToString()
        {
            return $"{Type}#{Sn}";
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode() + Sn * 102;
        }

        public override bool Equals(object obj)
        {
            var key = obj as ProjectSetKey;
            if (key == null) return false;
            return key.ToString() == this.ToString();
        }
    }
}
