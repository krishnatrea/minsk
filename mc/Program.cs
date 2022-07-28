using System.Drawing;
using System.Linq;
using System;
using System.Collections.Generic;
using Minsk.Analysis;
using Minsk.Analysis.Syntax;

namespace Minsk
{
     public class Program
     {
          public static void Main(string[] args)
          {
               bool showtree = false;
               while (true)
               {
                    Console.Write("> ");
                    var line = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                         return;

                    if (line == "#showTree")
                    {
                         showtree = !showtree;
                         Console.WriteLine(showtree ? "showing diagnostics tree " : "not showing tree");
                         continue;
                    }
                    else if (line == "#clear")
                    {
                         Console.Clear();
                         continue;
                    }

                    var syntaxTree = SyntaxTree.Parse(line);
                    
                    var color = Console.ForegroundColor;
               
                    if (showtree)
                    {
                         Console.ForegroundColor = ConsoleColor.DarkGray;
                         PrettyPrint(syntaxTree.Root);
                         Console.ForegroundColor = color;
                    }


                    if (!syntaxTree.Diagnostics.Any())
                    {
                         var e = new Evaluater(syntaxTree.Root);
                         var result = e.Evaluator();

                         Console.WriteLine(result);
                    }
                    else
                    {
                         Console.ForegroundColor = ConsoleColor.DarkRed;
                         foreach (var diagonstic in syntaxTree.Diagnostics)
                         {
                              Console.WriteLine(diagonstic);
                         }
                         Console.ForegroundColor = color;
                    }
               }
          }
          static void PrettyPrint(SyntaxNode node, string intent = "", bool isLast = true)
          {
               // └  │ ─ ├
               var marker = isLast ? "└───" : "├───";
               Console.Write(intent);
               Console.Write(marker);
               Console.Write(node.Kind);
               if (node is SyntaxToken t && t.Value != null)
               {
                    Console.Write(" ");
                    Console.Write(t.Value);
               }
               Console.WriteLine();

               intent += isLast ? "     " : "│    ";

               var lastChild = node.GetChildren().LastOrDefault();

               foreach (var child in node.GetChildren())
               {
                    PrettyPrint(child, intent, lastChild == child);
               }
          }
     }
}