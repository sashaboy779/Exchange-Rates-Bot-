using System.Reflection;
using System.Runtime.InteropServices;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config")]
[assembly: AssemblyTitle("ExchangeRateApi")]
[assembly: AssemblyDescription("A Telegram bot that provide an exchange rate information for selected date")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Oleksand Boichuk")]
[assembly: AssemblyProduct("ExchangeRateBot")]
[assembly: AssemblyCopyright("© 2020 Oleksandr Boichuk")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]
[assembly: Guid("47FD995A-B5A6-452A-8982-D253406695B8")]

// Version information for an assembly consists of the following four values:

// MAJOR is a major release (usually many new features or changes to the UI or underlying OS)
// MINOR is a minor release (perhaps some new features) on a previous major release
// REVISION is usually a fix for a previous minor release (no new functionality)
// BUILD NUMBER is incremented for each latest build of a revision.

[assembly: AssemblyVersion("1.15.0.0")]
