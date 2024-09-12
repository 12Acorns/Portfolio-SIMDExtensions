using SIMDExtensions_Generator.Generator.Types.Data;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;

namespace SIMDExtensions_Generator.Generator.Types.BaseVector;

internal sealed partial class BaseVectorGenerator
{
	private sealed class MethodGenerator : IGeneratorProvider
	{
		private static readonly Dictionary<MethodType, string> MethodTypeMapper = new()
		{
			// Acces
			{ MethodType.Protected, "protected" },
			{ MethodType.Internal, "internal" },
			{ MethodType.Private, "private" },
			{ MethodType.Public, "public" },

			// Other
			//{ MethodType.ReadOnly, "readonly" },
			{ MethodType.Static, "static" },
		};

		public MethodGenerator(MethodData _method)
		{
			method = _method;
		}

		private readonly MethodData method;

		public int WriteIndex { get; }

		public string Generate()
		{
			return GenerateMethod();
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private string GenerateMethod()
		{
			// 0 -> Access Modifer
			// 1 -> Static Modifer
			// 2 -> Return Type
			// 3 -> Name
			// 4 -> MethodArgs
			// 5 -> Code
			const string _METHODTEMPLATE =
			"""
			
				{0} {1} {2} {3}({4})
				{{
					{5}
				}}
			""";

			var _type = method.Meta.Type;
			ThrowIfInvalidType(_type);
			var _accessType = GetAccessType(_type);
			var _accessTypeName = GetAccessTypeName(_accessType);

			_ = IsMethodStatic(_type, out string _static);

			return string.Format(_METHODTEMPLATE,
				_accessTypeName, _static, method.Meta.ReturnType,
				method.Meta.Name, MethodMeta.ArgsToMethodParams(method.Meta.Args),
				method.Code.Generate());
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsMethodStatic(MethodType _type, out string _static)
		{
			_static = string.Empty;
			if(_type.HasFlag(MethodType.Static))
			{
				_static = "static";
				return true;
			}
			return false;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static string GetAccessTypeName(MethodType _type)
		{
			if(!MethodTypeMapper.TryGetValue(_type, out var _typeName))
			{
				throw new NotSupportedException($"{nameof(_type)} is not a supported {nameof(MethodType)} member");
			}
			return _typeName;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static MethodType GetAccessType(MethodType _type)
		{
			return _type.HasFlag(MethodType.Public)
				? MethodType.Public
				: _type.HasFlag(MethodType.Private)
				? MethodType.Private
				: _type.HasFlag(MethodType.Protected)
				? MethodType.Protected
				: MethodType.Internal;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void ThrowIfInvalidType(MethodType _type)
		{
			ThrowIfNoAccesabilityModifer(_type);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void ThrowIfNoAccesabilityModifer(MethodType _type)
		{
			bool _public = _type.HasFlag(MethodType.Public);
			bool _private = _type.HasFlag(MethodType.Private);
			bool _internal = _type.HasFlag(MethodType.Internal);
			bool _protected = _type.HasFlag(MethodType.Protected);

			if(!_public
			&& !_private
			&& !_internal
			&& !_protected)
			{
				throw new Exception(nameof(_type) + " must have a accesability modifer");
			}
		}
	}
}

