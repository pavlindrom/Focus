using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;

namespace Gr.Pavlo.Focus
{
    internal class Context : IContext
    {
        public long? ProjectId { get; private set; }

        public long? PackageId { get; private set; }

        public long? ScopeId { get; private set; }

        public long? TypeId { get; private set; }

        public long? SubTypeId { get; private set; }

        public long? ModuleId { get; private set; }

        public long? BlockId { get; private set; }

        public Context(IContext context)
        {
            ProjectId = context.ProjectId;
            PackageId = context.PackageId;
            ScopeId = context.ScopeId;
            TypeId = context.TypeId;
            SubTypeId = context.SubTypeId;
            ModuleId = context.ModuleId;
            BlockId = context.BlockId;
        }

        public IContext Extend(StructuralType type, long id)
        {
            var extended = new Context(this);

            switch(type)
            {
                case StructuralType.Project:
                    extended.ProjectId = id;
                    break;
                default:
                    throw new NotImplementedException($"Could not extend the context with type {type}.");
            }

            return extended;
        }
    }
}
