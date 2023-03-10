﻿using Ninject.Modules;
using System.Collections.Immutable;

namespace Nadeko.Medusa;

public sealed record ResolvedMedusa(
    WeakReference<MedusaAssemblyLoadContext> LoadContext,
    IImmutableList<ModuleInfo> ModuleInfos,
    IImmutableList<SnekInfo> SnekInfos,
    IMedusaStrings Strings,
    Dictionary<Type, TypeReader> TypeReaders,
    IReadOnlyCollection<ICustomBehavior> Execs
)
{
    public INinjectModule KernelModule { get; set; }
}