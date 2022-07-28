using System.Linq;
using System;
using System.Collections.Generic;
using Minsk.Analysis.Syntax;


namespace Minsk.Analysis
{
     class SyntaxToken : SyntaxNode
     {

          public SyntaxToken(SyntaxKind kind, int position, string text, Object value)
          {
               Kind = kind;
               Position = position;
               Text = text;
               Value = value;

          }

          public override SyntaxKind Kind { get; }

          public int Position { get; }
          public string Text { get; }

          public Object Value { get; }
          public override IEnumerable<SyntaxNode> GetChildren()
          {
               return Enumerable.Empty<SyntaxNode>();
          }

     }
}