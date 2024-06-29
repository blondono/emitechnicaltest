using System.Reflection;

namespace Emi.Employees.Infrastructure;

internal static class AssemblyReference
{
    /// <summary>
    /// The assembly.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}


