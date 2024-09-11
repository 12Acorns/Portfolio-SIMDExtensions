namespace SIMDExtensions_Generator.Generator.Types.Data;

internal enum NumericTypes : byte
{
	// Single types
	/// <summary>
	/// <see cref="float"/>
	/// </summary>
	Single,
	/// <summary>
	/// <see cref="double"/>
	/// </summary>
	Double,
	// Integer types - Signed
	/// <summary>
	/// <see cref="sbyte"/>
	/// </summary>
	Int8,
	/// <summary>
	/// <see cref="short"/>
	/// </summary>
	Int16,
	/// <summary>
	/// <see cref="int"/>
	/// </summary>
	Int32,
	/// <summary>
	/// <see cref="long"/>
	/// </summary>
	Int64,
	// Interger types - unsigned
	/// <summary>
	/// <see cref="byte"/>
	/// </summary>
	UInt8,
	/// <summary>
	/// <see cref="ushort"/>
	/// </summary>
	UInt16,
	/// <summary>
	/// <see cref="uint"/>
	/// </summary>
	UInt32,
	/// <summary>
	/// <see cref="ulong"/>
	/// </summary>
	UInt64,
}
