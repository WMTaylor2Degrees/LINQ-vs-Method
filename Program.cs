using System;
using System.Collections.Generic;
using System.Linq;

namespace SandboxConsoleApp;

public class Program
{
    public static void Main()
    {
        IList<Source> nullSourceObjs = null;

        IList<Source> emptySourceObjs = new List<Source>();

        IList<Source> populatedSourceObjs = new List<Source>
        {
            new Source {
                foo = "Foo",
                bar = 1
            },
            new Source {
                foo = "Bar",
                bar = 2
            }
        };

        // Explicit
        Console.WriteLine("--Mapping using explicit method to deal with list--");

        Console.WriteLine("  Null source");
        var explicitConversionNull = nullSourceObjs.ConvertToTargetList();
        PrintObject(explicitConversionNull);

        Console.WriteLine("  Empty source");
        var explicitConversionEmpty = emptySourceObjs.ConvertToTargetList();
        PrintObject(explicitConversionEmpty);

        Console.WriteLine("  Populated source");
        var explicitConversionPopulated = populatedSourceObjs.ConvertToTargetList();
        PrintObject(explicitConversionPopulated);


        // LINQ
        Console.WriteLine("--Mapping using LINQ statement to deal with list--");

        Console.WriteLine("  Null source");
        IEnumerable<Target> linqConversionNull = nullSourceObjs?.Select(x => x.ConvertToTarget());
        PrintObject(linqConversionNull);

        Console.WriteLine("  Empty source");
        IEnumerable<Target> linqConversionEmpty = emptySourceObjs?.Select(x => x.ConvertToTarget());
        PrintObject(linqConversionEmpty);

        Console.WriteLine("  Populated source");
        IEnumerable<Target> linqConversionPopulated = populatedSourceObjs?.Select(x => x.ConvertToTarget());
        PrintObject(linqConversionPopulated);
    }

    private static void PrintObject(IEnumerable<Target> targets)
    {
        if (targets == null)
        {
            Console.WriteLine("Null List");
            return;
        }

        if (!targets.Any())
        {
            Console.WriteLine("Empty List");
            return;
        }

        Console.Write("List length:");
        Console.WriteLine(targets.Count());
        foreach (Target tgt in targets)
        {
            Console.WriteLine(tgt.foo);
            Console.WriteLine(tgt.bar);
        }
    }
}

public class Source
{
    public string foo { get; set; }
    public int bar { get; set; }
}

public class Target
{
    public string foo { get; set; }
    public int bar { get; set; }
}

public static class TargetConverter
{
    public static List<Target> ConvertToTargetList(this IList<Source>? objList) => 
        objList == null || !objList.Any()
            ? new List<Target>(0)
            : objList.Select(ConvertToTarget).ToList();

    public static Target ConvertToTarget(this Source obj) => 
        obj == null
            ? null
            : new Target
            {
                foo = obj.foo,
                bar = obj.bar
            };
    
}