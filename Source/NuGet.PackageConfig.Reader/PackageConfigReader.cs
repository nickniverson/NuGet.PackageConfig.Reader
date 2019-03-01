using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NuGet.PackageConfig.Reader.Contracts;

namespace NuGet.PackageConfig.Reader
{
	/// <summary>
	/// A simple class for reading NuGet packages.config files.
	/// </summary>
	public class PackageConfigReader : IPackageConfigReader
	{
		/// <inherit /> 
		IEnumerable<Package> IPackageConfigReader.Read(string packageConfigFilePath)
		{
			if (!File.Exists(packageConfigFilePath))
			{
				throw new FileNotFoundException($"unable to find package.config file with path:  {packageConfigFilePath}");
			}

			var packages = XElement.Load(packageConfigFilePath);

			return packages
				.Descendants("package")
				.Select(x => new Package(
					id: x.Attribute("id").Value,
					version: x.Attribute("version").Value,
					targetFramework: x.Attribute("targetFramework")?.Value,
					allowedVersions: x.Attribute("allowedVersions")?.Value,
					developmentDependency: x.GetBoolAttributeNullOrThrow("developmentDependency")
				));
		}
	}
}
