// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// Suppress the warning pertaining to the structured logging, as:
//      CA1873: "Evaluation of this argument may be expensive and unnecessary if logging is disabled"
[assembly: SuppressMessage(
    category: "Performance",
    checkId: "CA1873:Avoid potentially expensive logging",
    Justification = "<Pending>",
    Scope = "module")]
