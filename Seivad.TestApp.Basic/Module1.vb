Module Module1

    Sub Main()

        Dim container = New Seivad.Container

        container.Register(Of ITest).Returns(Of TestClass)()

        Dim out = container.GetInstance(Of ITest)(Seivad.Arguments.Add("value", "testing").
                                                                   Add("another", "string"))

        Console.WriteLine(out.Test)
        Console.ReadKey()

    End Sub

End Module
