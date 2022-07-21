using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControlSamples.Models
{
    public class RowButonInfo : ICloneable
    {
        public string Text { get; set; }

        public string Key { get; set; }

        public int RowIndex { get; set; }

        public int ColumnIndex { get; set; }

        public string ImageKey { get; set; }

        public object Clone()
        {
            return new RowButonInfo()
            {
                Text = this.Text,
                Key = this.Key,
                RowIndex = this.RowIndex,
                ColumnIndex = this.ColumnIndex,
            };
        }

        public override int GetHashCode()
        {
            return this.Key.GetHashCode() + RowIndex * 102 + ColumnIndex * 121;
        }

        public override string ToString()
        {
            return $"{Key}_{RowIndex}_{ColumnIndex}";
        }

        public override bool Equals(object obj)
        {
            var key = obj as RowButonInfo;
            if (key == null) return false;
            return key.ToString() == this.ToString();
        }
    }
}
