namespace Gr.Pavlo.Focus
{
    interface IContext
    {
        long PackageId { get; }

        long ClassId { get; }

        long StaticClassId { get; }

        long SubClassId { get; }

        long MethodId { get; }

        long BlockId { get; }

        IContext Extend(StructuralType type, long id);
    }
}
