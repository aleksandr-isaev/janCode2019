Public Class TrackerSystem

    Private Accounts(9) As RunTracker
    Private AccountCount As Integer = 0
    Public Sub New()
        Accounts(0) = New RunTracker("Simon", "5000", "fish", True)
        Accounts(1) = New RunTracker("Annie", "2500", "fish", True)
        AccountCount = 2
    End Sub
    Public Function Menu() As Boolean
        Dim choice As Integer
        MenuOptions()
        choice = Console.ReadLine()
        Select Case choice
            Case 0
                Return False
            Case 1
                AddNewRunner()
            Case 2
                AddNewRun()
            Case 3
                OutputHistory()
            Case 4
                TenKmGoal()
            Case 5
                Update()
            Case Else
                Console.WriteLine("Not an Option")
        End Select
        Return True
    End Function
    Public Sub MenuOptions()
        Console.WriteLine("")
        Console.WriteLine("---------------------------------------")
        Console.WriteLine("Choose 1 add a new runner")
        Console.WriteLine("Choose 2 add a new run")
        Console.WriteLine("Choose 3 for your run history")
        Console.WriteLine("Choose 4 for your 10 km goal analysis")
        Console.WriteLine("Choose 5 to update data")
        Console.WriteLine("Choose 0 to exit")
    End Sub
    Public Sub AddNewRunner()
        Dim newName As String
        Dim newGoal As Integer
        Dim newPw As String
        Dim check As Boolean = False
        Dim currentChar As Integer
        Dim addedNumber As Boolean = True
        Dim count As Integer = 0

        Console.WriteLine("Enter Your Name")
        Do
            Do
                newName = Console.ReadLine()
                addedNumber = True
                For x = 1 To Len(newName)
                    currentChar = (Asc(Mid(newName, x, 1)))
                    If currentChar > 47 And currentChar < 58 And count = 0 Then
                        Console.WriteLine("You have entered an integer as part of your name. Please try again. ")
                        count = count + 1
                        addedNumber = False
                    End If
                Next
            Loop Until addedNumber = True
            Do
                Try
                    Console.WriteLine("Enter Your 10 Km goal time")
                    newGoal = Console.ReadLine
                Catch ex As Exception
                    MsgBox("Not entered an integer")
                End Try
            Loop Until newGoal > 0
            Console.WriteLine("Enter Your Password")
            newPw = Console.ReadLine
            check = PasswordChecker(newPw)
            Do While check = False
                Console.WriteLine("Enter a new password. It is not a strong enough password.")
                newPw = Console.ReadLine
                check = PasswordChecker(newPw)
            Loop
            Accounts(AccountCount) = New RunTracker(newName, newGoal, newPw, False)
        Loop Until Accounts(AccountCount).GetName <> ""
        AccountCount += 1
    End Sub
    Public Function PasswordChecker(ByVal newPw As String) As Boolean
        Dim upC As Integer
        Dim lowC As Integer
        Dim num As Integer
        Dim sym As Integer
        Dim points As Integer
        ' Length
        If Len(newPw) >= 8 Then
            points = points + 1
        End If
        For x = 1 To Len(newPw)
            ' Uppercase
            If Asc(Mid(newPw, x, 1)) > 64 And Asc(Mid(newPw, x, 1)) < 91 Then
                upC = upC + 1
                If upC = 1 Then
                    points = points + 1
                End If
            End If
            ' Lowercase
            If Asc(Mid(newPw, x, 1)) > 96 And Asc(Mid(newPw, x, 1)) < 123 Then
                lowC = lowC + 1
                If lowC = 1 Then
                    points = points + 1
                End If
            End If
            ' Numbers
            If Asc(Mid(newPw, x, 1)) >= 0 And Asc(Mid(newPw, x, 1)) < 48 Then
                num = num + 1
                If num = 1 Then
                    points = points + 1
                End If
            End If
            ' Symbols
            If Asc(Mid(newPw, x, 1)) > 47 And Asc(Mid(newPw, x, 1)) < 58 Then
                sym = sym + 1
                If sym = 1 Then
                    points = points + 1
                End If
            End If
        Next
        If points > 3 Then
            Return True
        End If
        Return False
    End Function
    Public Sub AddNewRun()
        Dim foundLocation As Integer
        foundLocation = Verify()
        If foundLocation > -1 Then
            Accounts(foundLocation).AddRun()
        End If
    End Sub
    Public Sub OutputHistory()
        Dim foundLocation As Integer
        foundLocation = Verify()
        If foundLocation > -1 Then
            Accounts(foundLocation).RunReview()
        End If
    End Sub
    Public Sub TenKmGoal()
        Dim foundLocation As Integer
        foundLocation = Verify()
        If foundLocation > -1 Then
            Accounts(foundLocation).RunAnalysis()
        End If
    End Sub
    Public Function Verify() As Integer
        Dim inputName As String
        Dim inputPw As String
        Console.WriteLine("Enter Your Name")
        inputName = Console.ReadLine()
        Console.WriteLine("Enter Your Password")
        inputPw = Console.ReadLine()
        For x = 0 To AccountCount - 1
            If inputName = Accounts(x).GetName Then
                If inputPw = Accounts(x).GetPw Then
                    Return x
                End If
            End If
        Next
        Console.WriteLine("Incorrect Username or password")
        Return -1
    End Function
    Public Sub Update()
        Dim newName As String
        Dim newGoal As Integer
        Dim choice As String
        Dim nmCheck As Integer
        Dim check As Boolean
        Console.WriteLine("UPDATE DATA")
        nmCheck = Verify()
        If nmCheck > -1 Then
            Console.WriteLine("Choose 1 to update name
Choose 2 to update goal")
            choice = Console.ReadLine()
            Select Case choice
                Case 1
                    Console.WriteLine("Enter new name")
                    check = Accounts(nmCheck).SetName(Console.ReadLine())
                    If check = True Then
                        Console.WriteLine("Name changed")
                    End If
                Case 2
                    Console.WriteLine("Enter new goal")
                    check = Accounts(nmCheck).SetGoalTenK(Console.ReadLine())
                    If check = True Then
                        Console.WriteLine("Goal changed")
                    End If
                Case Else

            End Select
        End If
    End Sub

End Class
