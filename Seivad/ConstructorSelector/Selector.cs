using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Seivad.Args;

namespace Seivad.ConstructorSelector
{
    internal class Selector
    {
       internal ConstructorData GetConstructorData(IList<ConstructorInfo> constructors, Arguments args)
        {
            /*
                0:  No Constructors : Throw Ex
                1:  Only Default Constructor, No Arguments : Invoke()
                2:  Only Default Constructor, Arguments supplied : Invoke()
                3:  Onlt Parameterised, No Arguments : Throw Ex()
                4:  Only Parameterised, Constructors with all args = 0 : Throw Ex
                5:  Only Parameterised, Constructors with exact args : Invoke()
                6:  Only Parameterised, Constructors with all args, registry cannot fill remainder : Throw Ex
                7:  Only Parameterised, Constructors with all args, registry can fill remainder : Invoke()
                8:  Mixed Constructors, No Arguments : Invoke()
                9:  Mixed Constructors, Constructors with all args = 0 : Throw Ex
                10: Mixed Constructors, Constructors with exact args : Invoke()
                11: Mixed Constructors, Constructors with all args, registry cannot fill remainder : Throw Ex
                12: Mixed Constructors, Constructors with all args, registry can fill remainder : Invoke()
            */

            //none
            if (constructors.Count == 0)
            {
                throw new ConstructorException();
            }

            //only Default
            if (constructors.All(c => c.GetParameters().Count() == 0))
            {

                //1
                if (args.Count != 0)
                {
                    throw new ConstructorException();
                }

                //2
                return new ConstructorData(constructors.First(), null);
            }


            //Only Parameterised
            if (constructors.All(c => c.GetParameters().Count() > 0))
            {

                //3
                if (args.Count == 0)
                {
                    throw new ConstructorException();
                }

                //4
                if (!constructors.Any(c => args.All(a => c.GetParameters().Contains(a))))
                {
                    throw new ConstructorException();
                }

                var withAll = constructors.Where(c => args.All(a => c.GetParameters().Contains(a)));
                var exact = withAll.Where(c => c.GetParameters().Count() == args.Count);

                //5
                if (exact.Count() > 0)
                {
                    var best = GetBestMatch(exact, args, null);
                    
                    return new ConstructorData(best.Constructor, best.Arguments );
                }

                //6
                var unmatchedArgs = GetUnmatchedArgs(withAll, args);
                if (!unmatchedArgs.Any(u => registry.ContainsAll(u.Value)))
                {
                    throw new ConstructorException();
                }

                //7
                return GetBestMatch(exact, args, registry.Get(unmatchedArgs)).Invoke(args);
            }

            //mixed
            if (constructors.Any(c => c.GetParameters().Count() > 0) && constructors.Any(c => c.GetParameters().Count() == 0))
            {

                //8
                if (args.Count == 0)
                {
                    var ctor = constructors.First(c => c.GetParameters().Count() == 0);
                    return new ConstructorData(ctor,null);
                }

                //9
                if (!constructors.Any(c => args.All(a => c.GetParameters().Contains(a))))
                {
                    throw new ConstructorException();
                }

                var withAll = constructors.Where(c => args.All(a => c.GetParameters().Contains(a)));
                var exact = withAll.Where(c => c.GetParameters().Count() == args.Count);

                //10
                if (exact.Count() > 0)
                {
                    var best = GetBestMatch(exact, args, new List<Argument>());
                    return new ConstructorData(best.Constructor, best.Arguments );
                }

                //11
                var unmatchedArgs = GetUnmatchedArgs(withAll, args);
                if (!unmatchedArgs.Any(u => registry.ContainsAll(u.Value)))
                {
                    throw new ConstructorException();
                }

                //12
                return GetBestMatch(exact, args, registry.Get(unmatchedArgs)).Invoke(args);
            }

            throw new ConstructorException();

        }




        Dictionary<ConstructorInfo, IList<ParameterInfo>> GetUnmatchedArgs(IEnumerable<ConstructorInfo> constructors, Arguments args)
        {

            var result = new Dictionary<ConstructorInfo, IList<ParameterInfo>>();

            foreach (var ctor in constructors)
            {
                var parameters = ctor.GetParameters().Where(p => !args.Contains(p)).ToList();
                if (parameters.Count > 0)
                {
                    result.Add(ctor, parameters);
                }
            }

            return result;
        }


        ConstructorData GetBestMatch(IEnumerable<ConstructorInfo> constructors, Arguments args, IList<Argument> registry)
        {

            var ctors = constructors.Select(c => new ConstructorMatchData
            {
                Constructor = c,
                Parameters = c.GetParameters().ToList(),
                ParameterDelta = c.GetParameters().Count() - args.Count(), /* negative number here is most likely (and wanted!) */
                MatchLength = 0,
                Score = 0
            });

            foreach (var ctor in ctors)
            {

                for (var i = 0; i < Math.Min(ctor.Parameters.Count(), args.Count()); i++)
                {

                    if (ctor.Parameters[i].Name != args[i].Name || ctor.Parameters[i].ParameterType != args[i].Value.GetType())
                    {
                        break;
                    }

                    ctor.MatchLength = ctor.MatchLength + 1;
                }
            }

            foreach (var ctor in ctors)
            {
                ctor.Score = ctor.MatchLength + ctor.ParameterDelta;
            }

            //check if the top item has a unique score
            var best = ctors.Where(c => c.Score == ctors.OrderBy(t => t.Score).First().Score);

            if (best.Count() == 1)
            {
                return new ConstructorData(best.First().Constructor, args.FilteredFor(best.First().Constructor ));
            }

            //apply the delta again to try and distinct the matches
            foreach (var ctor in ctors)
            {
                ctor.Score += ctor.ParameterDelta;
            }

            //check if the top item has a unique score
            best = ctors.Where(c => c.Score == ctors.OrderBy(t => t.Score).First().Score);

            if (best.Count() == 1)
            {
                return new ConstructorData(best.First().Constructor , args.FilteredFor(best.First().Constructor )); 
            }

            throw new ConstructorException();
        }

        private class ConstructorMatchData
        {
            public ConstructorInfo Constructor { get; set; }
            public List<ParameterInfo> Parameters { get; set; }
            public int ParameterDelta { get; set; }
            public int MatchLength { get; set; }
            public int Score { get; set; }
        }

    }

    
}
