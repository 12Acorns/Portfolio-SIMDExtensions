using SIMDExtensions.Core.Intrinsics;
using System.Numerics;
using System.Runtime.CompilerServices;

Console.WriteLine("Hello, World!");

var _testVec =  new BaseVector<double>();
var _testVec1 = new BaseVector<float>();

var _vec2 = new BaseVector<int>();
var _vec3 = new BaseVector<int>();

Console.WriteLine(GetVectorData<Half>());
Console.WriteLine(GetVectorData<double>());
Console.WriteLine(GetVectorData<float>());

//var _vec = _vec2 + _vec3;

Console.ReadLine();

static string GetVectorData<T>() where T : INumber<T>
{
	var _count = BaseVector<T>.Count;
	var _supported = BaseVector<T>.IsSupported;
	var _type = ToUpperFirstChar(typeof(T).Name);

	return
$@"
{_type}:
    Count: {_count}
    Supported: {_supported}";
}
static string ToUpperFirstChar(string _input)
{
	if(string.IsNullOrWhiteSpace(_input))
	{
		return string.Empty;
	}

	var _firstChar = char.ToUpper(_input[0]);
	if(_input.Length == 1)
	{
		return new string(_firstChar, 1);
	}

	var _buffer = new char[_input.Length];
	_buffer[0] = _firstChar;
	Array.ConstrainedCopy(_input.ToCharArray(), 1, _buffer, 1, _buffer.Length - 1);
	return new string(_buffer);
}