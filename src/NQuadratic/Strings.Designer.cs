namespace NQuadratic
{
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode]
    [CompilerGenerated]
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    internal sealed class Strings
    {
        private static ResourceManager resourceMan;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager =>
            resourceMan ?? (resourceMan = new ResourceManager(typeof(Strings).FullName, typeof(Strings).Assembly));

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture { get; set; }

        internal static string Arg_FactorZero => ResourceManager.GetString(nameof(Arg_FactorZero), Culture);

        internal static string Arg_ZeroNonQuadraticFormat => ResourceManager.GetString(nameof(Arg_ZeroNonQuadraticFormat), Culture);
    }
}
