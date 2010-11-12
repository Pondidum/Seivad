using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace Seivad.Specs
{
    class When_passed_a_type_with_no_public_constructor_and_no_arguments
    {
        Establish context = () => objectCreator.ReturnType = typeof(NoPublicConstructor);

        Because of = () => ex = Catch.Exception(() => objectCreator.GetInstance());

        It should_throw_a_constuctor_not_found_exception = () => ex.ShouldBeOfType<ConstructorNotFoundException>();

        static ObjectCreator objectCreator = new ObjectCreator();
        static Exception ex;
    }







    class NoPublicConstructor
    {
        private NoPublicConstructor()
        {
        }
    }
}
