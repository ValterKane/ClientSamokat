﻿using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSamokat.Components
{
    public interface IHashProvider
    {
        public string GetHash(string password);
    }
}
