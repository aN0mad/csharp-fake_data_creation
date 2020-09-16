# csharp-fake_data_creation
csharp-fake_data_creation is a C# program to generate fake data. Useful for creating non-sensitive data to exfiltrate during engagements. This project is targeted against .NET 4.0.

## Installation

Clone and load the project into visual studio then restore the NUGET packages.


## Usage

```
.\fakeData.exe
Options:
-n, --num     Number of entries to generate
-d, --domain     Domain to append to generated emails

Usage:
createData.exe -n 10000 -d test.com
```

## NuGet Libraries
* Bogus - For data creation
* CommandLineParser - For parsing command line arguments
* Costura.Fody - Fody plugin for embedding assemblies into the binary
* Fody - For manipulating the IL of the assembly

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.
