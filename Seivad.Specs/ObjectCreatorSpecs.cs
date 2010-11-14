using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Rhino.Mocks;

namespace Seivad.Specs
{
    public class When_passed_a_type_with_no_public_constructor_and_no_arguments
    {
        Establish context = () => objectCreator.ReturnType = typeof(NoPublicConstructor);
        Because of = () => ex = Catch.Exception(() => objectCreator.GetInstance(new ArgumentsDefinition()));

        It should_throw_a_constuctor_exception = () =>
        {
            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<ConstructorException>();
        };

        static ObjectCreator objectCreator = new ObjectCreator();
        static Exception ex;
    }

    public class When_passed_a_type_with_no_public_constructor_and_arguments
    {
        Establish context = () => objectCreator.ReturnType = typeof(NoPublicConstructor);
        Because of = () => ex = Catch.Exception(() => objectCreator.GetInstance(new ArgumentsDefinition()));

        It should_throw_a_constructor_exception = () =>
        {
            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<ConstructorException>();
        };

        static ObjectCreator objectCreator = new ObjectCreator();
        static Exception ex;
    }

    public class When_passed_a_type_with_only_a_default_constructor_and_no_arguments
    {
        Establish context = () => objectCreator.ReturnType = typeof(DefaultConstructor);
        Because of = () => obj = objectCreator.GetInstance(new ArgumentsDefinition());

        It should_return_an_instance = () =>
        {
            obj.ShouldNotBeNull();
            obj.ShouldBeOfType<DefaultConstructor>();
        };

        static ObjectCreator objectCreator = new ObjectCreator();
        static Object obj;
    }

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

        It should_throw_a_constructor_exception = () =>
        {
            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<ConstructorException>();
        };

        static IArguments args;
        static ObjectCreator objectCreator = new ObjectCreator();
        static Exception ex;
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
}
