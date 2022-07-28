using System.Collections.Generic;
using Minsk.Analysis.Syntax;


namespace Minsk.Analysis
{
     abstract class SyntaxNode
     {
          public abstract SyntaxKind Kind { get; }

          public abstract IEnumerable<SyntaxNode> GetChildren();
     }
}