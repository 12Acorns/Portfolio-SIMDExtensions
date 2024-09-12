using System;

namespace SIMDExtensions_Generator.Generator.Types.Data;

[Flags]
internal enum MethodType : byte
{
	// Access Modifiers
	Public = 1,
	Internal = 2,
	Private = 4,
	Protected = 8,

	// Other
	Static = 16,
	//ReadOnly
}
