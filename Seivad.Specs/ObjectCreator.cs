using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace Seivad.Specs
{
    public class When_passed_a_type_with_no_public_constructor_and_no_arguments
    {
        Establish context = () => objectCreator.ReturnType = typeof(NoPublicConstructor);
        Because of = () => ex = Catch.Exception(() => objectCreator.GetInstance());

        It should_throw_a_constuctor_exception = () =>
        {
            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<ConstructorNotFoundException>();
        };

        static ObjectCreator objectCreator = new ObjectCreator();
        static Exception ex;
    }

    public class When_passed_a_type_with_no_public_constructor_and_arguments
    {
        Establish context = () => objectCreator.ReturnType = typeof(NoPublicConstructor);
        Because of = () => ex = Catch.Exception(() => objectCreator.GetInstance());

        It should_throw_a_constructor_exception = () =>
        {
            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<ConstructorNotFoundException>();
        };

        static ObjectCreator objectCreator = new ObjectCreator();
        static Exception ex;
    }

    public class When_passed_a_type_with_only_a_default_constructor_and_no_arguments
    {
        Establish context = () => objectCreator.ReturnType = typeof(DefaultConstructor);
        Because of = () => obj = objectCreator.GetInstance();

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
        Establish context = () => objectCreator.ReturnType = typeof(DefaultConstructor);
        Because of = () => ex = Catch.Exception(() => objectCreator.GetInstance());

        It should_throw_a_constructor_exception = () =>
        {
            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<ConstructorNotFoundException>();
        };



        static ObjectCreator objectCreator = new ObjectCreator();
        static Exception  ex;
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
