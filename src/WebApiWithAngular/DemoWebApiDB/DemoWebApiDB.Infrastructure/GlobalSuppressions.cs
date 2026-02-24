// This file is used by Code Analysis to maintain SuppressMessage attributes applied in the project.
// Project-level suppressions either have no target
// or are given a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;


/*********
[assembly: SuppressMessage(
    category: "Style", 
    checkId: "IDE0300:Simplify collection initialization", 
    Justification = "<Pending>", 
    Scope = "member", 
    Target = "~M:DemoWebApiDB.Infrastructure.Results.Result`1.NotFound(System.String)~DemoWebApiDB.Infrastructure.Results.Result{`0}")]
******/

[assembly: SuppressMessage(
    category: "Style",
    checkId: "IDE0300:Simplify collection initialization",
    Justification = "Personal preference: Do not want to use simplified Initialization",
    Scope = "module")]

[assembly: SuppressMessage(
    category: "Style",
    checkId: "IDE0063:Use simple 'using' statement",
    Justification = "Personal preference: I wish to use 'using' blocks",
    Scope = "module")]

[assembly: SuppressMessage(
    category: "Style",
    checkId: "IDE0290:Use primary constructor",
    Justification = "Personal preference: Do not want to use Primary Constructors",
    Scope = "module")]

[assembly: SuppressMessage(
    category: "Style",
    checkId: "IDE0305:Collection initialization can be simplified",
    Justification = "Personal preference: Do not want to use simplified Initialization",
    Scope = "module")]
