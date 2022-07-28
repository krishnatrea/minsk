using System;
using System.Collections.Generic;

namespace Minsk.Analysis.Syntax
{
     class ExpressionSyntax : SyntaxNode
     {
          public override SyntaxKind Kind { get; }

          public override IEnumerable<SyntaxNode> GetChildren()
          {
               throw new NotImplementedException();
          }
     }
}