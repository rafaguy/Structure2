﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Interface
{
    public interface IFileOpener
    {
      Task OpenFile(string filePath);
    }
}
