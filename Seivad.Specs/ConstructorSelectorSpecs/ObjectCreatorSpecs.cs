using System.Linq;
using System.Reflection;
using Machine.Specifications;
using Rhino.Mocks;
using Seivad.Args;
using Seivad.ConstructorSelector;
using System.Collections.Generic;

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

    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_no_public_constructor_and_no_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            AddConstructorsFor<NoPublicConstructor>();
            args = MockRepository.GenerateStub<Arguments>();
        };

        It should_throw_a_constuctor_exception = () => ex.ShouldBeOfType<ConstructorException>();
        It should_not_create_an_instance = () => constructorData.ShouldBeNull();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_no_public_constructor_and_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            AddConstructorsFor<NoPublicConstructor>();
            args = MockRepository.GenerateStub<Arguments>();
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
        It should_not_create_an_instance = () => constructorData.ShouldBeNull();
    }



    [Subject("With no Repository")]
    public class When_passed_a_type_with_only_a_default_constructor_and_no_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            AddConstructorsFor<DefaultConstructor>();
            args = MockRepository.GenerateStub<Arguments>();
        };

        It should_return_an_instance = () => constructorData.ShouldBeOfType<DefaultConstructor>();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_only_a_default_constructor_and_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            AddConstructorsFor<DefaultConstructor>();
            args = MockRepository.GenerateStub<Arguments>();
            args.Add("test", "value");
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
        It should_not_create_an_instance = () => constructorData.ShouldBeNull();
    }



    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_no_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            AddConstructorsFor<ParameterisedOneArgument>();
            args = MockRepository.GenerateStub<Arguments>();
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
        It should_not_create_an_instance = () => constructorData.ShouldBeNull();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_one_nonmatching_by_name_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            AddConstructorsFor<ParameterisedOneArgument>();
            args = MockRepository.GenerateStub<Arguments>();
            args.Add("test", "value");
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_one_nonmatching_by_type_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            AddConstructorsFor<ParameterisedOneArgument>();
            args = MockRepository.GenerateStub<Arguments>();
            args.Add("argument", 10);
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
        It should_not_create_an_instance = () => constructorData.ShouldBeNull();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_one_matching_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            AddConstructorsFor<ParameterisedOneArgument>();
            args = MockRepository.GenerateStub<Arguments>();
            args.Add("argument", "test");
        };

        It should_return_an_instance = () => constructorData.ShouldBeOfType<ParameterisedOneArgument>();
    }



    [Subject("With no Repository")]
    public class When_passed_a_type_with_default_and_parameterised_constructors_and_no_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            AddConstructorsFor<DefaultAndParameterisedOneArgument>();
            args = MockRepository.GenerateStub<Arguments>();
        };

        It should_return_an_instance = () => constructorData.ShouldNotBeNull();
        It should_call_the_default_constructor = () => constructorData.Constructor.GetParameters().Count().ShouldEqual(0);
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_default_and_parameterised_constructors_and_one_matching_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            AddConstructorsFor<DefaultAndParameterisedOneArgument>();
            args = MockRepository.GenerateStub<Arguments>();
            args.Add("argument", "test");
        };

        It should_return_an_instance = () => constructorData.ShouldNotBeNull();
        It should_call_the_parameterised_constructor = () => constructorData.Constructor.GetParameters().Count().ShouldEqual(1);

    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_default_and_parameterised_constructors_and_one_non_matching_by_name_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            AddConstructorsFor<DefaultAndParameterisedOneArgument>();
            args = MockRepository.GenerateStub<Arguments>();
            args.Add("TEST", "test");
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
        It should_not_create_an_instance = () => constructorData.ShouldBeNull();

    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_default_and_parameterised_constructors_and_one_non_matching_by_type_argument : NoRepositoryBase
    {

        Establish context = () =>
        {
            AddConstructorsFor<DefaultAndParameterisedOneArgument>();
            args = MockRepository.GenerateStub<Arguments>();
            args.Add("argument", 8);
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
        It should_not_create_an_instance = () => constructorData.ShouldBeNull();
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

    class DefaultAndParameterisedOneArgument
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
}
