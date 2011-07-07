using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Machine.Specifications;
using Rhino.Mocks;
using Seivad.Args;
using Seivad.ConstructorSelector;

namespace Seivad.Specs.ConstructorSelectorSpecs
{
    public class NoRepositoryBase : SpecBase
    {
        static internal ConstructorData constructorData;
        static protected List<ConstructorInfo> constructors;
        static protected Arguments args;
        static internal Selector selector;

        static NoRepositoryBase()
        {
            selector = new Selector(MockRepository.GenerateMock<IRegistry>());
        }

        Because of = () =>
        {
            ex = null;
            constructorData = null;

            ex = Catch.Exception(() => constructorData = selector.GetConstructorData(constructors, args)); // objectCreator.GetInstance(args));

        };

        protected static void AddConstructorsFor<T>()
        {
            if (constructors == null) constructors = new List<ConstructorInfo>();

            constructors.AddRange(typeof(T).GetConstructors());
        }

        protected static void SetupDefaults()
        {
            constructors = null;
            args = MockRepository.GenerateStub<Arguments>();
        }


    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_no_public_constructor_and_no_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            SetupDefaults();
            AddConstructorsFor<NoPublicConstructor>();
        };

        It should_throw_a_constuctor_exception = () => ex.ShouldBeOfType<ConstructorException>();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_no_public_constructor_and_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            SetupDefaults();
            AddConstructorsFor<NoPublicConstructor>();
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
    }



    [Subject("With no Repository")]
    public class When_passed_a_type_with_only_a_default_constructor_and_no_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            SetupDefaults();
            AddConstructorsFor<DefaultConstructor>();
        };

        It should_return_the_default_constructor = () => constructorData.Constructor.GetParameters().Count().ShouldEqual(0);
        It should_not_return_any_arguments = () => constructorData.Arguments.ShouldBeEmpty();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_only_a_default_constructor_and_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            SetupDefaults();
            AddConstructorsFor<DefaultConstructor>();
            args.Add("test", "value");
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
    }



    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_no_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            SetupDefaults();
            AddConstructorsFor<ParameterisedOneArgument>();
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_one_nonmatching_by_name_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            SetupDefaults();
            AddConstructorsFor<ParameterisedOneArgument>();
            args.Add("test", "value");
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_one_nonmatching_by_type_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            SetupDefaults();
            AddConstructorsFor<ParameterisedOneArgument>();
            args.Add("argument", 10);
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_one_matching_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            SetupDefaults();
            AddConstructorsFor<ParameterisedOneArgument>();
            args.Add("argument", "test");
        };

        It should_return_the_constructor = () => constructorData.Constructor.GetParameters().Count().ShouldEqual(1);
        It should_return_the_argument = () => constructorData.Arguments.ShouldContainOnly(args.First());
    }

    [Subject("With no repository")]
    public class When_passed_a_type_with_only_parameterised_constructors_with_two_parameters_and_only_one_supplied : NoRepositoryBase
    {
        private Establish context = () =>
        {
            SetupDefaults();
            AddConstructorsFor<ParameterisedTwoArguments>();
            args.Add("argument", "string");
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
    }



    [Subject("With no Repository")]
    public class When_passed_a_type_with_default_and_parameterised_constructors_and_no_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            SetupDefaults();
            AddConstructorsFor<DefaultAndParameterisedOneArgument>();
        };

        It should_return_the_default_constructor = () => constructorData.Constructor.GetParameters().Count().ShouldEqual(0);
        It should_return_no_arguments = () => constructorData.Arguments.ShouldBeEmpty();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_default_and_parameterised_constructors_and_one_matching_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            SetupDefaults();
            AddConstructorsFor<DefaultAndParameterisedOneArgument>();
            args.Add("argument", "test");
        };

        It should_return_the_parameterised_constructor = () => constructorData.Constructor.GetParameters().Count().ShouldEqual(1);
        It should_return_the_argument = () => constructorData.Arguments.Select(a => a.Name).ShouldContainOnly("argument");

    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_default_and_parameterised_constructors_and_one_non_matching_by_name_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            SetupDefaults();
            AddConstructorsFor<DefaultAndParameterisedOneArgument>();
            args.Add("TEST", "test");
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_default_and_parameterised_constructors_and_one_non_matching_by_type_argument : NoRepositoryBase
    {

        Establish context = () =>
        {
            SetupDefaults();
            AddConstructorsFor<DefaultAndParameterisedOneArgument>();
            args.Add("argument", 8);
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_default_and_parameterised_constructor_with_two_arguments_and_one_matching_argument : NoRepositoryBase
    {
        private Establish context = () =>
        {
            SetupDefaults();
            AddConstructorsFor<DefaultAndParameterisedTwoArguments>();
            args.Add("argument", "string");
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
    }



    internal class NoPublicConstructor
    {
        private NoPublicConstructor()
        {
        }
    }

    internal class DefaultConstructor
    {
        public DefaultConstructor()
        {
        }
    }

    internal class ParameterisedOneArgument
    {
        public ParameterisedOneArgument(string argument) { }
    }

    internal class ParameterisedTwoArguments
    {
        public ParameterisedTwoArguments(string argument, int number) {}
    }

    internal class DefaultAndParameterisedOneArgument
    {
        public string ConstructorCalled { get; private set; }

        public DefaultAndParameterisedOneArgument()
        {
            ConstructorCalled = "Default";
        }

        public DefaultAndParameterisedOneArgument(string argument)
        {
            ConstructorCalled = "Parameterised";
        }
    }

    internal class DefaultAndParameterisedTwoArguments
    {
        public  DefaultAndParameterisedTwoArguments() { }
        public DefaultAndParameterisedTwoArguments(string argument, int number) { }
    }
}
