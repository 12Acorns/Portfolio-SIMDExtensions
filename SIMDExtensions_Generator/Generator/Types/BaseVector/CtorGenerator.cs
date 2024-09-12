using SIMDExtensions_Generator.Generator.Types.Data;
using System.Collections.Generic;
using System.Text;

namespace SIMDExtensions_Generator.Generator.Types.BaseVector;

internal sealed partial class BaseVectorGenerator
{
	private sealed class CtorGenerator : IGeneratorProvider
	{
		private const string CTORTEMPLATE =
		"""
			public BaseVector(ReadOnlySpan<T> _data)
			{{
				switch(targetBitWidth)
				{{
					{0}
				}}
			}}
		""";
		private static readonly string vectorTypes = GetVectorTypes();

		public int WriteIndex { get; } = -1;

		public string Generate()
		{
			return string.Concat(
				vectorTypes,
				"\n",
				"""
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
					/// 		<term><see cref="Vector256{T}/></term>
					/// 	</item>
					/// 	<item>
					/// 		<term><see cref="Vector512{T}/></term>
					/// 	</item>
					/// </list>
					/// <para></para>
					/// <b>
					/// Do not recommend to use as is. Instead use: <see cref="VectorArray{T}"/> for handling unkown length elements,
					/// and or for easily handling data that may exceed a wrapped vectors <see cref="BaseVector{T}.Count"/>
					/// </b>
					/// </summary>
				
				""",
				string.Format(CTORTEMPLATE, string.Join("\n", GenerateCtor())));
		}

		private static IEnumerable<string> GenerateCtor()
		{
			// 0 -> Width member
			// 1 -> Code
			const string _SWITCHCASETEMPLATE =
				@$"
				case {SIMDDataHolder.SIMDWidthsEnumName}.{{0}}:
					{{1}}
					break;
				";
			const string _SWITCHNONECASETEMPLATE =
				$@"
				case {SIMDDataHolder.SIMDWidthsEnumName}.None:
					throw new PlatformNotSupportedException(""SIMD not supported"");
				";

			foreach(var _width in SIMDDataHolder.SIMDWidths)
			{
				if(_width is "None")
				{
					yield return _SWITCHNONECASETEMPLATE;
					continue;
				}
				else if(_width is "Vector")
				{
					yield return string.Format(_SWITCHCASETEMPLATE,
						_width, "vector = new Vector<T>(_data);");
					continue;
				}
				_ = SIMDDataHolder.TryEnumToMember(_width, out string _name);
				yield return string.Format(_SWITCHCASETEMPLATE,
					_width, string.Format("{0} = {1}.Create(_data);", _name, _width));
			}
			yield return 
				"\t\t\tdefault:\n\t\t\t\t" +
						"throw new NotSupportedException(nameof(targetBitWidth) + \" is not yet supported\");";
		}
		private static string GetVectorTypes()
		{
			var _builder = new StringBuilder();
			foreach(var (_type, _name) in SIMDDataHolder.VectorTypeAndNames)
			{
				_builder.AppendFormat("\tprivate readonly {0} {1};\n", _type, _name);
			}
			return _builder.ToString();
		}
	}
}