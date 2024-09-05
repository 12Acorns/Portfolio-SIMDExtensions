namespace SIMDExtensions_Generator;

internal readonly record struct ReturnInfo
{
	public ReturnInfo(string _input, string _returnType)
	{
		Input = _input;
		ReturnType = _returnType;
	}

	public string Input { get; }
	public string ReturnType { get; }
}
