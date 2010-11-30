using Machine.Specifications;
using Rhino.Mocks;
using Seivad.Args;

namespace Seivad.Specs
{
    public class NoRepositoryBase : SpecBase
    {
        static NoRepositoryBase()
        {
            registry = MockRepository.GenerateMock<IRegistry>();
            objectCreator = new ObjectCreator(registry);
        }

        Because of = () =>  
        {
            ex = null;
            obj = null;

            ex = Catch.Exception(() => obj = objectCreator.GetInstance(args));

        };

        static protected object obj;
        static protected Arguments args;
        static internal ObjectCreator objectCreator;
        internal static IRegistry registry;
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_no_public_constructor_and_no_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            objectCreator.ReturnType = typeof(NoPublicConstructor);
           args = MockRepository.GenerateStub<Arguments>();
        };

        It should_throw_a_constuctor_exception = () => ex.ShouldBeOfType<ConstructorException>();
        It should_not_create_an_instance = () => obj.ShouldBeNull();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_no_public_constructor_and_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            objectCreator.ReturnType = typeof(NoPublicConstructor);
            args = MockRepository.GenerateStub<Arguments>();
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
        It should_not_create_an_instance = () => obj.ShouldBeNull();
    }



    [Subject("With no Repository")]
    public class When_passed_a_type_with_only_a_default_constructor_and_no_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            objectCreator.ReturnType = typeof(DefaultConstructor);
            args = MockRepository.GenerateStub<Arguments>();
        };

        It should_return_an_instance = () => obj.ShouldBeOfType<DefaultConstructor>();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_only_a_default_constructor_and_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            objectCreator.ReturnType = typeof(DefaultConstructor);
            args = MockRepository.GenerateStub<Arguments>();
            args.Add("test", "value");
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
        It should_not_create_an_instance = () => obj.ShouldBeNull();
    }



    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_no_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            objectCreator.ReturnType = typeof(ParameterisedOneArgument);
            args = MockRepository.GenerateStub<Arguments>();
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
        It should_not_create_an_instance = () => obj.ShouldBeNull();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_one_nonmatching_by_name_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            objectCreator.ReturnType = typeof(ParameterisedOneArgument);
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
            objectCreator.ReturnType = typeof(ParameterisedOneArgument);
            args = MockRepository.GenerateStub<Arguments>();
            args.Add("argument", 10);
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
        It should_not_create_an_instance = () => obj.ShouldBeNull();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_one_matching_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            objectCreator.ReturnType = typeof(ParameterisedOneArgument);
            args = MockRepository.GenerateStub<Arguments>();
            args.Add("argument", "test");
        };

        It should_return_an_instance = () => obj.ShouldBeOfType<ParameterisedOneArgument>();
    }



    [Subject("With no Repository")]
    public class When_passed_a_type_with_default_and_parameterised_constructors_and_no_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            objectCreator.ReturnType = typeof(DefaultAndParameterisedOneArgument);
            args = MockRepository.GenerateStub<Arguments>();
        };

        It should_return_an_instance = () => obj.ShouldNotBeNull();
        It should_call_the_default_constructor = () => ((DefaultAndParameterisedOneArgument)obj).ConstructorCalled.ShouldEqual("Default");
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_default_and_parameterised_constructors_and_one_matching_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            objectCreator.ReturnType = typeof(DefaultAndParameterisedOneArgument);
            args = MockRepository.GenerateStub<Arguments>();
            args.Add("argument", "test");
        };

        It should_return_an_instance = () => obj.ShouldNotBeNull();
        It should_call_the_parameterised_constructor = () => ((DefaultAndParameterisedOneArgument)obj).ConstructorCalled.ShouldEqual("Parameterised");
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_default_and_parameterised_constructors_and_one_non_matching_by_name_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            objectCreator.ReturnType = typeof(DefaultAndParameterisedOneArgument);
            args = MockRepository.GenerateStub<Arguments>();
            args.Add("TEST", "test");
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
        It should_not_create_an_instance = () => obj.ShouldBeNull();

    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_default_and_parameterised_constructors_and_one_non_matching_by_type_argument : NoRepositoryBase
    {

        Establish context = () =>
        {
            objectCreator.ReturnType = typeof(DefaultAndParameterisedOneArgument);
            args = MockRepository.GenerateStub<Arguments>();
            args.Add("argument", 8);
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
        It should_not_create_an_instance = () => obj.ShouldBeNull();
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
