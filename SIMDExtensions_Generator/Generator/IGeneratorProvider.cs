namespace SIMDExtensions_Generator.Generator;

internal interface IGeneratorProvider
{
	/// <summary>
	/// Where you can safely write data to without breaking code flow/cause errors.
	/// </summary>
	/// <value>Returns -1 if you should not write any data, 
	/// else the index you can write to</value>
	public int WriteIndex { get; }

	public string Generate();
}
