Imports System.IO
Imports System.Net
Imports Newtonsoft
Imports Newtonsoft.Json


Class MainWindow

    'ReadOnly api As String = "https://aws.engraph.com/REST/SFCS/"
    ReadOnly api As String = "https://aws.engraph.com/REST/SFCSTEST/"
    Public token As String
    Public kimPW As String = "6637AA99E18177E7663960496752E46A3CCE8012FB8E4D599DF0890EE3C1A299C768CE89FB2C300CB680A1B22FA98E0AB126049EE703AB92B0D8EBEBB99F3CFF"
    Public kimEmail As String = "kim.marsh@st-francis.org"

    Function SearchClientByBrokerID(ByVal searchText As String) As List(Of Client)
        ValidateToken()

        Dim request As WebRequest = WebRequest.Create(api + $"Client/Search/Broker={searchText}&Token={token}&Device=APIDEMO")
        request.ContentType = "application/json; charset=utf-8"
        listResults.Items.Add(request.RequestUri.ToString())

        Try
            Using response As HttpWebResponse = request.GetResponse()
                Dim reader As StreamReader = New StreamReader(response.GetResponseStream)
                Dim s As String = reader.ReadToEnd()
                listResults.Items.Add(s)
                Dim clients As SimpleList(Of Client) = JsonConvert.DeserializeObject(Of SimpleList(Of Client))(s)

                For Each c In clients.list
                    listResults.Items.Add($"{c.clientID} {c.firstName} {c.lastName}")

                Next




            End Using

        Catch ex As Exception
            listResults.Items.Add(ex.ToString())
        End Try

    End Function

    Sub ValidateToken()
        If String.IsNullOrWhiteSpace(token) Then
            token = GetToken()
        End If
    End Sub

    Function GetTrips() As List(Of Trip)

        ValidateToken()

        Dim request As WebRequest = WebRequest.Create(api + $"TripService/Trips?Token={token}&Device=APIDEMO&Driver=31&Date=2016-08-15")
        request.ContentType = "application/json; charset=utf-8"
        listResults.Items.Add(request.RequestUri.ToString())

        Try
            Using response As HttpWebResponse = request.GetResponse()
                Dim reader As StreamReader = New StreamReader(response.GetResponseStream)
                Dim s As String = reader.ReadToEnd()
                listResults.Items.Add(s)
                Dim trips As List(Of Trip) = JsonConvert.DeserializeObject(Of List(Of Trip))(s)

                For Each t In trips
                    listResults.Items.Add($"{t.tripID} {t.clientFirstName} {t.clientLastName}")

                Next




            End Using

        Catch ex As Exception
            listResults.Items.Add(ex.ToString())
        End Try


        Return New List(Of Trip)

    End Function

    Function GetToken() As String
        'https://aws.engraph.com/ParaPlanREST/UserService/Login?UserName=kim.marsh@st-francis.org&Password=6637AA99E18177E7663960496752E46A3CCE8012FB8E4D599DF0890EE3C1A299C768CE89FB2C300CB680A1B22FA98E0AB126049EE703AB92B0D8EBEBB99F3CFF&Device=APIDEMO&Version=0.1&DeviceToken=
        Dim request As WebRequest = WebRequest.Create($"https://aws.engraph.com/ParaPlanREST/UserService/Login?UserName={kimEmail}&Password={kimPW}&Device=APIDEMO&Version=0.1&DeviceToken=")
        request.ContentType = "application/json; charset=utf-8"
        Dim rv As String
        Try
            Using response As HttpWebResponse = request.GetResponse()
                Dim reader As StreamReader = New StreamReader(response.GetResponseStream)
                Dim s As String = reader.ReadToEnd()
                listResults.Items.Add(s)
                Dim user As User = JsonConvert.DeserializeObject(Of User)(s)

                listResults.Items.Add(user.name)

                rv = user.key


            End Using

        Catch ex As Exception
            listResults.Items.Add(ex.ToString())
        End Try

        Return rv

    End Function

    Private Sub GetTripsButton_Click(sender As Object, e As RoutedEventArgs)
        GetTrips()
    End Sub

    Private Sub SearchByBroker_Click(sender As Object, e As RoutedEventArgs)
        SearchClientByBrokerID("53378")
    End Sub

    Private Sub listResults_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)
        Clipboard.SetText(listResults.SelectedItem.ToString())
    End Sub
End Class

Public Class RESTBase
    Public tokenIsValid As Boolean
    Public tokenExists As Boolean
    Public errorMessage As String
    Public success As Boolean
End Class
Public Class Trip
    Inherits RESTBase

    Public tripID As Int32
    Public clientLastName As String
    Public clientFirstName As String

End Class

Public Class Client
    Inherits RESTBase
    Public clientID As Int32
    Public firstName As String
    Public lastName As String

End Class

Public Class SimpleList(Of T)
    Inherits RESTBase
    Public list As List(Of T)

End Class

Public Class User
    Public name As String
    Public userId As String
    Public key As String

End Class

