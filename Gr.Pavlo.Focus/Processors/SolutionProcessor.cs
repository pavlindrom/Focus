﻿using System;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.IO;

namespace Gr.Pavlo.Focus.Processors
{
    internal class SolutionProcessor: BaseProcessor<Solution>
    {
        public SolutionProcessor(Solution item)
            :base(item)
        { }

        public override (StructuralType Type, long Id) Insert()
        {
            var id = Database.CreateNode("Solution", new Dictionary<string, object>
            {
                { "name", Path.GetFileNameWithoutExtension(Item.FilePath) }
            });

            return (StructuralType.Solution, id);
        }
    }
}
