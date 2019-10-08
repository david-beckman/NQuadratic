namespace NQuadratic.Console
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

        internal static string FactoredFormName => ResourceManager.GetString(nameof(FactoredFormName), Culture);

        internal static string FormNameAndValueSeparator => ResourceManager.GetString(nameof(FormNameAndValueSeparator), Culture);

        internal static string StandardFormName => ResourceManager.GetString(nameof(StandardFormName), Culture);

        internal static string Usage => ResourceManager.GetString(nameof(Usage), Culture);

        internal static string VertexFormName => ResourceManager.GetString(nameof(VertexFormName), Culture);
    }
}
