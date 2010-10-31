
Class TestClass
    Implements ITest

    Private _test As String

    Public Sub New(ByVal value As String)
        _test = value
    End Sub

    Public Property Test As String Implements ITest.Test
        Get
            Return _test
        End Get
        Set(ByVal value As String)
            _test = value
        End Set
    End Property
End Class
