using SIMDExtensions.Core.Intrinsics;
using System.Runtime.Intrinsics;
using System.Numerics;

namespace SIMDExtensions_ExampleUsage;

/// <summary>
/// Below are members that are wrapped by <see cref="BaseVector{T}"/>
/// <list type="bullet">
///		<item>
///			<term><see cref="Vector{T}"/></term>
///		</item>
///		<item>
///			<term><see cref="Vector64{T}"/></term>
///		</item>
///		<item>
/// 		<term><see cref="Vector128{T}"/></term>
/// 	</item>
/// 	<item>
/// 		<term><see cref="Vector256{T}"/></term>
/// 	</item>
/// 	<item>
/// 		<term><see cref="Vector512{T}"/></term>
/// 	</item>
/// </list>
/// <para></para>
/// <b>
/// Do not recommend to use as is. Instead use: <see cref="VectorArray{T}"/> for handling unkown length elements,
/// and or for easily handling data that may exceed a wrapped vectors <see cref="BaseVector{T}.Count"/>
/// </b>
/// </summary>
internal class XMLDocumentationTesting
{
}
