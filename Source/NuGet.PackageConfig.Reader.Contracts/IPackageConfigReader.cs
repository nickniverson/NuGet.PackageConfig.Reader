using System.Collections.Generic;

namespace NuGet.PackageConfig.Reader.Contracts
{
	/// <summary>
	/// A simple contract for reading NuGet packages.config files.
	/// </summary>
	public interface IPackageConfigReader
	{
		/// <summary>
		/// Reads the contents of the given packages.config file path.
		/// 
		/// Assumes that the given file path is in fact a valid packages.config file.  
		/// </summary>
		/// <returns>
		/// Returns an IEnumerable of <see cref="Package" />.
		/// </returns>
		/// <exception cref="FileNotFoundException" /> 
		/// <exception cref="InvalidBooleanAttributeException" />
		IEnumerable<Package> Read(string packageConfigFilePath);
	}
}
