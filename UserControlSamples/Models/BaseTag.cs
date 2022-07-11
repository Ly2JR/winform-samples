﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControlSamples.Models
{
    public class BaseTag
    {
        public ProjectSetKey Key { get; set; }

        public string Tag { get; set; }

        /// <summary>
        /// 是否继续
        /// </summary>
        public bool Continute { get; set; }
    }
}
