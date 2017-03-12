namespace Gr.Pavlo.Focus
{
    interface IContext
    {
        long? ProjectId { get; }

        long? PackageId { get; }

        long? ScopeId { get; }

        long? TypeId { get; }

        long? SubTypeId { get; }

        long? ModuleId { get; }

        long? BlockId { get; }

        IContext Extend(StructuralType type, long id);
    }
}
