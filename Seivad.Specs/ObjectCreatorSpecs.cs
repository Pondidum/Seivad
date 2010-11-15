using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Rhino.Mocks;

namespace Seivad.Specs
{
    public class NoRepositoryBase : SpecBase
    {
        static protected IArguments args;
        static internal ObjectCreator objectCreator = new ObjectCreator();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_no_public_constructor_and_no_arguments : NoRepositoryBase
    {
        Establish context = () => objectCreator.ReturnType = typeof(NoPublicConstructor);
        Because of = () => ex = Catch.Exception(() => objectCreator.GetInstance(new ArgumentsDefinition()));

        It should_throw_a_constuctor_exception = () => ex.ShouldBeOfType<ConstructorException>();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_no_public_constructor_and_arguments : NoRepositoryBase
    {
        Establish context = () => objectCreator.ReturnType = typeof(NoPublicConstructor);
        Because of = () => ex = Catch.Exception(() => objectCreator.GetInstance(new ArgumentsDefinition()));

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();

    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_only_a_default_constructor_and_no_arguments : NoRepositoryBase
    {
        Establish context = () => objectCreator.ReturnType = typeof(DefaultConstructor);
        Because of = () => obj = objectCreator.GetInstance(new ArgumentsDefinition());

        It should_return_an_instance = () => obj.ShouldBeOfType<DefaultConstructor>();

        static object obj;
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_only_a_default_constructor_and_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            args = MockRepository.GenerateMock<IArguments>();
            args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>() { { "test", "value" } });
            args.Expect(a => a.Names()).Return(new List<string>() { "test" });

            objectCreator.ReturnType = typeof(DefaultConstructor);
        };

        Because of = () => ex = Catch.Exception(() => objectCreator.GetInstance(args));

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();

    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_no_arguments : NoRepositoryBase
    {
        Establish context = () => objectCreator.ReturnType = typeof(ParameterisedOneArgument);

        Because of = () => ex = Catch.Exception(() => objectCreator.GetInstance(new ArgumentsDefinition()));

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();

    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_one_nonmatching_by_name_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            args = MockRepository.GenerateMock<IArguments>();
            args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>() { { "test", "value" } });
            args.Expect(a => a.Names()).Return(new List<string>() { "test" });

            objectCreator.ReturnType = typeof(ParameterisedOneArgument);
        };

        Because of = () => ex = Catch.Exception(() => objectCreator.GetInstance(args));

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();

    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_one_nonmatching_by_type_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            args = MockRepository.GenerateMock<IArguments>();
            args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>() { { "argument", 10 } });
            args.Expect(a => a.Names()).Return(new List<string>() { "argument" });

            objectCreator.ReturnType = typeof(ParameterisedOneArgument);
        };

        Because of = () => ex = Catch.Exception(() => objectCreator.GetInstance(args));

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();

    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_one_matching_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            args = MockRepository.GenerateMock<IArguments>();
            args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>() { { "argument", "test" } });
            args.Expect(a => a.Names()).Return(new List<string>() { "argument" });

            objectCreator.ReturnType = typeof(ParameterisedOneArgument);
        };

        Because of = () => obj = objectCreator.GetInstance(args);

        It should_return_an_instance = () => obj.ShouldBeOfType<ParameterisedOneArgument>();

        static object obj;
    }



    [Subject("With no Repository")]
    public class With_default_and_parameterised_constructors_and_no_arguments : NoRepositoryBase
    { }


    [Subject("With no Repository")]
    public class With_default_and_parameterised_constructors_and_one_matching_argument : NoRepositoryBase
    {

    }


    [Subject("With no Repository")]
    public class With_default_and_parameterised_constructors_and_one_non_matching_by_name_argument : NoRepositoryBase
    { }


    [Subject("With no Repository")]
    public class With_default_and_parameterised_constructors_and_one_non_matching_by_type_argument : NoRepositoryBase
    { }

    class NoPublicConstructor
    {
        private NoPublicConstructor()
        {
        }
    }

    class DefaultConstructor
    {
        public DefaultConstructor()
        {
        }
    }

    class ParameterisedOneArgument
    {
        public ParameterisedOneArgument(string argument)
        {
        }
    }
}
