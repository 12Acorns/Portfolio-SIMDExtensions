using System;

namespace SIMDExtensions_Generator.Generator.Types.Data.Method;

public sealed class Arg
{
	public Arg(Type _type, bool _autoName = true) : this(_type.Name, _autoName) { }
	public Arg(Type _type, string _name) : this(_type.Name, _name) { }
	public Arg(string _type, bool _autoName = true)
	{
		TypeName = _type;
		if(!_autoName)
		{
			ArgName = null;
			return;
		}

		var _span = _type.AsSpan();
		var _sliceStart = _span.IndexOf('<');
		var _typeSlice = _span.Slice(0, _sliceStart);
		ArgName = _typeSlice.ToString().ToLowerInvariant();
	}
	public Arg(string _type, string _name)
	{
		TypeName = _type;
		ArgName = _name;
	}

	/// <summary>
	/// The name of the type passed in
	/// </summary>
	public string TypeName { get; }
	/// <summary>
	/// The param name
	/// </summary>
	public string? ArgName { get; }
}