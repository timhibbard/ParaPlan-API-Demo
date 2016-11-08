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
    Public apiFailuresCounter As Int32 = 0

    Function SearchClientByBrokerID(ByVal searchText As String) As SimpleList(Of Client)
        ValidateToken()

        Dim request As WebRequest = WebRequest.Create(api + $"ClientService/Client/Search/Broker={searchText}?Token={token}&Device=APIDEMO")
        request.ContentType = "application/json; charset=utf-8"
        listResults.Items.Add(request.RequestUri.ToString())
        Dim clients As SimpleList(Of Client) = New SimpleList(Of Client)
        Try
            Using response As HttpWebResponse = request.GetResponse()
                Dim reader As StreamReader = New StreamReader(response.GetResponseStream)
                Dim s As String = reader.ReadToEnd()
                listResults.Items.Add(s)
                clients = JsonConvert.DeserializeObject(Of SimpleList(Of Client))(s)
                If clients.tokenExists = False Or clients.tokenIsValid = False Then
                    apiFailuresCounter = apiFailuresCounter + 1
                    If apiFailuresCounter > 5 Then
                        Throw New Exception("Too many API failures")
                    End If
                    token = GetToken()
                    Return SearchClientByBrokerID(searchText)
                End If
                For Each c In clients.list
                    listResults.Items.Add($"{c.id} {c.name} {c.notes}")

                Next

            End Using

        Catch ex As Exception
            listResults.Items.Add(ex.ToString())
        End Try

        Return clients

    End Function

    Function SearchClientByCustomID(ByVal searchText As String) As SimpleList(Of Client)
        ValidateToken()

        Dim request As WebRequest = WebRequest.Create(api + $"ClientService/Client/Search/CustomID={searchText}?Token={token}&Device=APIDEMO")
        request.ContentType = "application/json; charset=utf-8"
        listResults.Items.Add(request.RequestUri.ToString())
        Dim clients As SimpleList(Of Client) = New SimpleList(Of Client)
        Try
            Using response As HttpWebResponse = request.GetResponse()
                Dim reader As StreamReader = New StreamReader(response.GetResponseStream)
                Dim s As String = reader.ReadToEnd()
                listResults.Items.Add(s)
                clients = JsonConvert.DeserializeObject(Of SimpleList(Of Client))(s)
                If clients.tokenExists = False Or clients.tokenIsValid = False Then
                    apiFailuresCounter = apiFailuresCounter + 1
                    If apiFailuresCounter > 5 Then
                        Throw New Exception("Too many API failures")
                    End If
                    token = GetToken()
                    Return SearchClientByCustomID(searchText)
                End If
                For Each c In clients.list
                    listResults.Items.Add($"{c.id} {c.name} {c.notes}")

                Next

            End Using

        Catch ex As Exception
            listResults.Items.Add(ex.ToString())
        End Try

        Return clients

    End Function

    Function GetClientByID(ByVal Id As String) As Client
        ValidateToken()

        Dim request As WebRequest = WebRequest.Create(api + $"ClientService/Client/{Id}?Token={token}&Device=APIDEMO")
        request.ContentType = "application/json; charset=utf-8"
        listResults.Items.Add(request.RequestUri.ToString())
        Dim c As Client = New Client
        Try
            Using response As HttpWebResponse = request.GetResponse()
                Dim reader As StreamReader = New StreamReader(response.GetResponseStream)
                Dim s As String = reader.ReadToEnd()
                listResults.Items.Add(s)
                c = JsonConvert.DeserializeObject(Of Client)(s)
                If c.tokenExists = False Or c.tokenIsValid = False Then
                    apiFailuresCounter = apiFailuresCounter + 1
                    If apiFailuresCounter > 5 Then
                        Throw New Exception("Too many API failures")
                    End If
                    token = GetToken()
                    Return GetClientByID(Id)
                End If
                listResults.Items.Add($"{c.id} {c.name} {c.notes}")

            End Using

        Catch ex As Exception
            listResults.Items.Add(ex.ToString())
        End Try

        Return c
    End Function


    Sub ValidateToken()
        If String.IsNullOrWhiteSpace(token) Then
            token = GetToken()
        End If
    End Sub

    Function GetTrips() As List(Of TripWrapper)

        ValidateToken()

        '/EkidzTrips?Token={token}&Device={device}&DateStart={dateStart}&DateEnd={dateEnd}&HideCancelled={hideCancelled}&hideNoShow={hideNoShow}&Programs={programs}//
        'Dim request As WebRequest = WebRequest.Create(api + $"TripService/Trips?Token={token}&Device=APIDEMO&Driver=31&Date=2016-08-15")
        Dim request As WebRequest = WebRequest.Create(api + $"TripService/EkidzTrips?Token={token}&Device=APIDEMO&DateStart=2016-08-15&DateEnd=2016-08-18&HideCancelled=0&HideNoShow=0&Programs=14&ClientType=All")
        request.ContentType = "application/json; charset=utf-8"
        listResults.Items.Add(request.RequestUri.ToString())
        Dim trips As List(Of TripWrapper) = New List(Of TripWrapper)
        Try
            Using response As HttpWebResponse = request.GetResponse()
                Dim reader As StreamReader = New StreamReader(response.GetResponseStream)
                Dim s As String = reader.ReadToEnd()
                listResults.Items.Add(s)
                trips = JsonConvert.DeserializeObject(Of List(Of TripWrapper))(s)
                If trips.First.tokenExists = False Or trips.First.tokenIsValid = False Then
                    apiFailuresCounter = apiFailuresCounter + 1
                    If apiFailuresCounter > 5 Then
                        Throw New Exception("Too many API failures")
                    End If
                    token = GetToken()
                    Return GetTrips()
                End If
                For Each t In trips
                    listResults.Items.Add($"{t.Trip.Client.Contact.bestPhoneNumber} - {t.Trip.tripDate}")
                Next


            End Using

        Catch ex As Exception
            listResults.Items.Add(ex.ToString())
        End Try

        Return trips
    End Function

    Function GetToken() As String
        'https://aws.engraph.com/ParaPlanREST/UserService/Login?UserName=kim.marsh@st-francis.org&Password=6637AA99E18177E7663960496752E46A3CCE8012FB8E4D599DF0890EE3C1A299C768CE89FB2C300CB680A1B22FA98E0AB126049EE703AB92B0D8EBEBB99F3CFF&Device=APIDEMO&Version=0.1&DeviceToken=
        Dim urlString = $"https://aws.engraph.com/ParaPlanREST/UserService/Login?UserName={kimEmail}&Password={kimPW}&Device=APIDEMO&Version=0.1&DeviceToken="
        listResults.Items.Add(urlString)
        Dim request As WebRequest = WebRequest.Create(urlString)
        request.ContentType = "application/json; charset=utf-8"
        Dim rv As String = ""
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
        SearchClientByBrokerID(brokerSearchText.Text)
    End Sub

    Private Sub listResults_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)
        Dim text As String = listResults.SelectedItem.ToString()
        Dim fileLocation As String = System.Environment.CurrentDirectory + "//" + Guid.NewGuid().ToString() + ".txt"
        File.WriteAllText(fileLocation, text)
        Process.Start(fileLocation)
    End Sub

    Private Sub SearchByCustom_Click(sender As Object, e As RoutedEventArgs)
        SearchClientByCustomID(customSearchText.Text)
    End Sub


    Private Sub GetByID_Click(sender As Object, e As RoutedEventArgs)
        GetClientByID(getByIDText.Text)
    End Sub
End Class

Public Class RESTBase
    Public tokenIsValid As Boolean
    Public tokenExists As Boolean
    Public errorMessage As String
    Public success As Boolean
End Class
Public Class [Stop]
    Inherits RESTBase

    Public tripID As Int32
    Public clientLastName As String
    Public clientFirstName As String

End Class

Public Class Client
    Inherits RESTBase
    Public id As Int32
    Public name As String
    Public notes As String

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

Public Class TripWrapper
    Inherits RESTBase
    Public Trip As Trip

End Class

Public Class Trip
    Public appointmentType As String
    Public Client As TripClient
    Public tripId As String
    Public tripDate As DateTime


End Class

Public Class TripClient
    Public birthDate As Date
    Public Contact As ContactInformation

End Class

Public Class ContactInformation
    Public bestPhoneNumber As String
    Public cellPhone As String
    Public homePhone As String
    Public otherPhone As String

End Class


