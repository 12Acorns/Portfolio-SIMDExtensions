using System.Collections.Generic;
using System;

namespace SIMDExtensions_Generator.Generator.Types.Data;

internal static class NumericTypesHolder
{
	public static readonly IEnumerable<(NumericTypes _type, string _name)> TypeAndKeywordName =
	[
		// Floating types
		(NumericTypes.Single, nameof(Single)),
		(NumericTypes.Double, nameof(Double)),

		// Signed Ints
		(NumericTypes.Int8, nameof(SByte)),
		(NumericTypes.Int16, nameof(Int16)),
		(NumericTypes.Int32, nameof(Int32)),
		(NumericTypes.Int64, nameof(Int64)),

		// Unsigned Ints
		(NumericTypes.UInt8, nameof(Byte)),
		(NumericTypes.UInt16, nameof(UInt16)),
		(NumericTypes.UInt32, nameof(UInt32)),
		(NumericTypes.UInt64, nameof(UInt64))
	];
}