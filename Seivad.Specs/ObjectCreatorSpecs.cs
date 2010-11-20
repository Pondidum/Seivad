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

        Because of = () => {
            ex = null;
            obj = null;

            ex = Catch.Exception(() => obj = objectCreator.GetInstance(args));
        };

        static protected object obj;
        static protected IArguments args;
        static internal ObjectCreator objectCreator = new ObjectCreator();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_no_public_constructor_and_no_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            objectCreator.ReturnType = typeof(NoPublicConstructor);
            args = MockRepository.GenerateMock<IArguments>();
            args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>());
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
            args = MockRepository.GenerateMock<IArguments>();
            args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>());
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
            args = MockRepository.GenerateMock<IArguments>();
            args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>());
        };

        It should_return_an_instance = () => obj.ShouldBeOfType<DefaultConstructor>();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_only_a_default_constructor_and_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            objectCreator.ReturnType = typeof(DefaultConstructor);
            args = MockRepository.GenerateMock<IArguments>();
            args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>() { { "test", "value" } });
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
            args = MockRepository.GenerateMock<IArguments>();
            args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>());
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
            args = MockRepository.GenerateMock<IArguments>();
            args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>() { { "test", "value" } });
        };

        It should_throw_a_constructor_exception = () => ex.ShouldBeOfType<ConstructorException>();
    }

    [Subject("With no Repository")]
    public class When_passed_a_type_with_one_parameterised_constructor_and_one_nonmatching_by_type_argument : NoRepositoryBase
    {
        Establish context = () =>
        {
            objectCreator.ReturnType = typeof(ParameterisedOneArgument);
            args = MockRepository.GenerateMock<IArguments>();
            args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>() { { "argument", 10 } });
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
            args = MockRepository.GenerateMock<IArguments>();
            args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>() { { "argument", "test" } });
        };

        It should_return_an_instance = () => obj.ShouldBeOfType<ParameterisedOneArgument>();
    }



    [Subject("With no Repository")]
    public class When_passed_a_type_with_default_and_parameterised_constructors_and_no_arguments : NoRepositoryBase
    {
        Establish context = () =>
        {
            objectCreator.ReturnType = typeof(DefaultAndParameterisedOneArgument);
            args = MockRepository.GenerateMock<IArguments>();
            args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>());
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
            args = MockRepository.GenerateMock<IArguments>();
            args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>() { { "argument", "test" } });
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
           args = MockRepository.GenerateMock<IArguments>();
           args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>() { { "TEST", "test" } });
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
            args = MockRepository.GenerateMock<IArguments>();
            args.Expect(a => a.ToDictionary()).Return(new Dictionary<string, object>() { { "argument", 8 } });
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
