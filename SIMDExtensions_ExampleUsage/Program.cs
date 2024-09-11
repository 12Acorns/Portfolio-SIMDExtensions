using SIMDExtensions.Core.Intrinsics;
using System.Numerics;

Console.WriteLine("Hello, World!");

var _testSequenceInt = Enumerable.Range(0, 1000).ToArray();

var _vecc = new Vector<int>(_testSequenceInt);

var _vec1 = new BaseVector<int>(_testSequenceInt);
var _vec2 = new BaseVector<int>(_testSequenceInt);

Console.WriteLine(GetVectorData<Half>());
Console.WriteLine(GetVectorData<double>());
Console.WriteLine(GetVectorData<float>());
Console.WriteLine(GetVectorData<int>());

//var _vec = _vec2 + _vec3;

Console.ReadLine();

static string GetVectorData<T>() where T : struct, INumber<T>
{
	var _supported = BaseVector<T>.IsSupported;

	int? _count = _supported ? BaseVector<T>.Count : null;
	var _type = ToUpperFirstChar(typeof(T).Name);

	return
$@"
{_type}:
    Count: {_count?.ToString() ?? "Not supported"}
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