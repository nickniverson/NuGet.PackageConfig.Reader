# NuGet.PackageConfig.Reader
A simple implementation for reading the NuGet packages.config file.


## API

### Interface

```CSharp

namespace NuGet.PackageConfig.Reader.Contracts
{
	public interface IPackageConfigReader
	{
		IEnumerable<Package> Read(string packageConfigFilePath);
	}
}

```


### Model

```CSharp

namespace NuGet.PackageConfig.Reader.Contracts
{
	public class Package
	{
		public Package(
			string id, 
			string version, 
			string targetFramework = null, 
			string allowedVersions = null, 
			bool? developmentDependency = null)
		{
			// required properties
			Id = id ?? throw new ArgumentNullException(nameof(id));
			Version = version ?? throw new ArgumentNullException(nameof(version));

			// optional properties
			TargetFramework = targetFramework;
			AllowedVersions = allowedVersions;
			DevelopmentDependency = developmentDependency;
		}

		public string Id { get; }

		public string Version { get; }

		public string TargetFramework { get; }

		public string AllowedVersions { get; }

		public bool? DevelopmentDependency { get; }
	}
}


```

## Example

To see additional examples of how to use this library, check out the [unit tests](https://github.com/nickniverson/NuGet.PackageConfig.Reader/blob/master/Source/NuGet.PackageConfig.Reader.Tests/PackageConfigReaderTests.cs).


The example console application below can be found [here](https://github.com/nickniverson/NuGet.PackageConfig.Reader/tree/master/Source/NuGet.PackageConfig.Reader.Examples.Console).  


``` CSharp
using NuGet.PackageConfig.Reader;
using NuGet.PackageConfig.Reader.Contracts;
using System;
using System.Collections.Generic;
using System.IO;

namespace NuGet.PackageConfig.Reader.Examples.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			new Example(new PackageConfigReader()).DisplayPackages();

			System.Console.WriteLine("Press enter to exit...");
			System.Console.ReadLine();
		}
	}


	public class Example
	{
		private readonly IPackageConfigReader _packageConfigReader;


		public Example(IPackageConfigReader packageConfigReader)
		{
			_packageConfigReader = packageConfigReader ?? 
				throw new ArgumentNullException(nameof(packageConfigReader));
		}


		public void DisplayPackages()
		{
			string packageConfigFile = Path.Combine(
				Environment.CurrentDirectory, 
				@"..\..\fake-packages.config");

			IEnumerable<Package> packages = _packageConfigReader.Read(packageConfigFile);

			WriteHeader($"File:  {packageConfigFile}");

			foreach (Package package in packages)
			{
				Display(package, packageConfigFile);
			}
		}


		private void Display(Package package, string packageConfigFile)
		{
			WriteLine($"package.Id: {package.Id}");
			WriteLine($"package.Version: {package.Version}");
			WriteLine($"package.TargetFramework: {package.TargetFramework}");
			WriteLine($"package.AllowedVersions: {package.AllowedVersions}");
			WriteLine($"package.DevelopmentDependency: {package.DevelopmentDependency}");

			WriteEmptyLines(2);
		}


		private void WriteHeader(string message)
		{
			WriteLine(message);
			WriteLine("-".PadRight(message.Length, '-'));
			WriteEmptyLines(2);

		}


		private void WriteEmptyLines(int count)
		{
			for (int i = 0; i < count; i++)
			{
				WriteLine(string.Empty);
			}
		}


		private void WriteLine(string line)
		{
			System.Console.WriteLine(line);
		}
	}
}

```

### Example Output

```Cmd

File:  C:\Users\nlniv\Documents\code\NuGet.PackageConfig.Reader\Source\NuGet.PackageConfig.Reader.Examples.Console\bin\Debug\..\..\fake-packages.config
-------------------------------------------------------------------------------------------------------------------------------------------------------


package.Id: Newtonsoft.Json
package.Version: 12.0.1
package.TargetFramework: net461
package.AllowedVersions: [8,13)
package.DevelopmentDependency: False


package.Id: Fake.Package.With.Only.Required.Attributes
package.Version: 12.0.1
package.TargetFramework:
package.AllowedVersions:
package.DevelopmentDependency:


Press enter to exit...

```


