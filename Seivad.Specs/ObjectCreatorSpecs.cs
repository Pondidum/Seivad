using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Rhino.Mocks;

namespace Seivad.Specs
{
    [Subject("With no Repository")]
    public class When_passed_a_type_with_no_public_constructor_and_no_arguments
    {
        Establish context = () => objectCreator.ReturnType = typeof(NoPublicConstructor);
        Because of = () => ex = Catch.Exception(() => objectCreator.GetInstance(new ArgumentsDefinition()));

        It should_throw_a_constuctor_exception = () => ex.ShouldBeOfType<ConstructorException>();

        static ObjectCreator objectCreator = new ObjectCreator();
        static Exception ex;
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_no_public_constructor_and_arguments
    {
        Establish context = () => objectCreator.ReturnType = typeof(NoPublicConstructor);
        Because of = () => ex = Catch.Exception(() => objectCreator.GetInstance(new ArgumentsDefinition()));

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();

        static ObjectCreator objectCreator = new ObjectCreator();
        static Exception ex;
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_only_a_default_constructor_and_no_arguments
    {
        Establish context = () => objectCreator.ReturnType = typeof(DefaultConstructor);
        Because of = () => obj = objectCreator.GetInstance(new ArgumentsDefinition());

        It should_return_an_instance = () => obj.ShouldBeOfType<DefaultConstructor>();

        static ObjectCreator objectCreator = new ObjectCreator();
        static Object obj;
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_only_a_default_constructor_and_arguments
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

        static IArguments args;
        static ObjectCreator objectCreator = new ObjectCreator();
        static Exception ex;
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_no_arguments
    {
        Establish context = () => objectCreator.ReturnType = typeof(ParameterisedOneArgument);

        Because of = () => ex = Catch.Exception(() => objectCreator.GetInstance(new ArgumentsDefinition()));

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();

        static ObjectCreator objectCreator = new ObjectCreator();
        static Exception ex;
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_one_nonmatching_by_name_argument
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

        static IArguments args;
        static ObjectCreator objectCreator = new ObjectCreator();
        static Exception ex;
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_one_nonmatching_by_type_argument
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

        static IArguments args;
        static ObjectCreator objectCreator = new ObjectCreator();
        static Exception ex;
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_one_matching_argument
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

        static IArguments args;
        static ObjectCreator objectCreator = new ObjectCreator();
        static Object obj;
    }




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
