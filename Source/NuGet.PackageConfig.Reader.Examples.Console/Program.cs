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
