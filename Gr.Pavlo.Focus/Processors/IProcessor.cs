namespace Gr.Pavlo.Focus.Processors
{
    internal interface IProcessor
    {
        (StructuralType Type, long Id) Process();
    }
}
