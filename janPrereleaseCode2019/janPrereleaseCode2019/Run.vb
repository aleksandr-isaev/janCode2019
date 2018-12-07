Public Class Run

    Private distance As Integer
    Private seconds As Integer
    Public Sub SetDistance(ByVal newValue As Integer)
        distance = newValue
    End Sub
    Public Sub SetSeconds(ByVal newValue As Integer)
        seconds = newValue
    End Sub
    Public Function GetDistance() As Integer
        Return distance
    End Function
    Public Function GetSeconds() As Integer
        Return seconds
    End Function
    Public Function GetPace() As Double 'changed to double from integer
        Return (seconds / 60) / (distance / 1000)
    End Function
    Public Function GetSpeed() As Double 'changed to double from decimal
        Return Convert.mps2kmph(distance / seconds)
    End Function
    Public Sub OutputRun()
        Console.WriteLine("You ran " & distance / 1000 & " km in " & seconds / 60 & " Minutes")
        Console.WriteLine("     You average speed was " & GetSpeed())
        Console.WriteLine("     You average pace was " & GetPace())
    End Sub

End Class
