namespace SIMDExtensions_Generator.Generator.Types.Data.Method;

internal readonly struct MethodData
{
    public MethodData(MethodMeta _meta, IGeneratorProvider _code)
    {
        Meta = _meta;
        Code = _code;
    }

    public MethodMeta Meta { get; }
    public IGeneratorProvider Code { get; }
}
