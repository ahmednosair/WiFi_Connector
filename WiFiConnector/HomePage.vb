Imports Windows.Devices.WiFi
Imports Windows.Security.Credentials
Imports Windows.Foundation

Public Class HomePage
    Private adapter As WiFiAdapter
    Private result As IAsyncOperation(Of WiFiConnectionResult)
    Private resultNetworkView As TableLayoutPanel
    Private scanInProg As String = "NO"
    Private lastPushWps As Button
    Private Sub HomePage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ScanBtn.PerformClick() Scan once on program start
    End Sub


    Private Sub populateNetworkView(ByRef view As TableLayoutPanel, ByRef network As WiFiAvailableNetwork, ByVal connectedToThis As Boolean, ByVal hasWps As Boolean)
        view.AutoSize = True
        view.AutoSizeMode = AutoSizeMode.GrowAndShrink
        view.Controls.Clear()

        Dim ssidRow As New FlowLayoutPanel
        ssidRow.AutoSize = True
        Dim ssid As New Label
        Dim ssidVal As New Label
        ssidVal.Name = "ssidVal"
        ssid.Margin = New Padding(0)
        ssidVal.Margin = New Padding(0)
        ssid.AutoSize = True
        ssid.Text = "Ssid:"
        ssidVal.AutoSize = True
        ssidVal.Font = New Font(New Font(New FontFamily("Microsoft Sans Serif"), 14), FontStyle.Bold)
        ssidVal.Text = If(network.Ssid.Length = 0, "Hidden Network", network.Ssid)
        ssidRow.Controls.Add(ssid)
        ssidRow.Controls.Add(ssidVal)

        view.Controls.Add(ssidRow)


        Dim bssid As New Label
        bssid.AutoSize = True
        bssid.Text = "Bssid:  " & network.Bssid
        view.Controls.Add(bssid)
        Dim rssi As New Label
        rssi.AutoSize = True
        rssi.Text = "Rssi:  " & network.NetworkRssiInDecibelMilliwatts & " dBm"
        view.Controls.Add(rssi)
        Dim ch As New Label
        ch.AutoSize = True
        ch.Text = "Channel Frequency:  " & network.ChannelCenterFrequencyInKilohertz & " kHz"
        view.Controls.Add(ch)
        Dim auth As New Label
        auth.AutoSize = True
        auth.Text = "Authentication:  " & network.SecuritySettings.NetworkAuthenticationType.ToString()
        view.Controls.Add(auth)
        Dim encr As New Label
        encr.AutoSize = True
        encr.Text = "Encryption: " & network.SecuritySettings.NetworkEncryptionType.ToString()
        view.Controls.Add(encr)


        Dim reconnectRow As New FlowLayoutPanel
        reconnectRow.AutoSize = True
        Dim reconnectBox As New CheckBox
        reconnectBox.Margin = New Padding(5, 5, 0, 0)
        reconnectBox.AutoSize = True
        Dim reconnect As New Label
        reconnect.Text = "Connect Automatically"
        reconnect.AutoSize = True
        reconnectRow.Controls.Add(reconnectBox)
        reconnectRow.Controls.Add(reconnect)
        reconnectRow.Name = "reconnectRow"
        If Not connectedToThis Then
            view.Controls.Add(reconnectRow)

        End If

        Dim nameVal As New TextBox
        If String.IsNullOrEmpty(network.Ssid) Then
            Dim nameLbl As New Label
            Dim nameRow As New FlowLayoutPanel
            nameRow.AutoSize = True
            nameLbl.Margin = New Padding(0, 0, 0, 0)
            nameVal.Margin = New Padding(0)
            nameLbl.Text = "Name (Ssid):"
            nameLbl.AutoSize = True
            nameVal.MinimumSize = New System.Drawing.Size(200, 25)
            nameRow.Controls.Add(nameLbl)
            nameRow.Controls.Add(nameVal)
            nameRow.Name = "nameRow"
            If Not connectedToThis Then
                view.Controls.Add(nameRow)

            End If
        End If


        Dim secretVal As New TextBox
        If Not network.SecuritySettings.NetworkAuthenticationType = Windows.Networking.Connectivity.NetworkAuthenticationType.None And Not network.SecuritySettings.NetworkAuthenticationType = Windows.Networking.Connectivity.NetworkAuthenticationType.Open80211 Then
            Dim secretRow As New FlowLayoutPanel
            secretRow.Name = "secretRow"
            secretRow.AutoSize = True
            Dim secret As New Label
            secret.Margin = New Padding(0, 0, 11, 0)
            secretVal.Margin = New Padding(0)
            secret.Text = "Secret Key:"
            secret.AutoSize = True
            secretVal.MinimumSize = New System.Drawing.Size(200, 25)
            secretVal.UseSystemPasswordChar = True
            Dim showPass As New Button
            showPass.BackColor = Color.FromArgb(200, 200, 200)
            showPass.MinimumSize = New Drawing.Size(40, 28)
            showPass.MaximumSize = New Drawing.Size(40, 28)
            showPass.Margin = New Padding(5, 0, 0, 0)

            showPass.Image = Image.FromFile("view.png")
            showPass.ImageAlign = ContentAlignment.MiddleCenter

            AddHandler showPass.Click, Sub()
                                           secretVal.UseSystemPasswordChar = Not secretVal.UseSystemPasswordChar
                                       End Sub
            secretRow.Controls.Add(secret)
            secretRow.Controls.Add(secretVal)
            secretRow.Controls.Add(showPass)
            If Not connectedToThis Then
                view.Controls.Add(secretRow)

            End If
        End If




        Dim buttonsRow As New FlowLayoutPanel
        buttonsRow.Name = "buttonsRow"
        buttonsRow.AutoSize = True
        Dim connect As New Button
        connect.Name = "connect"
        connect.AutoSize = True
        connect.AutoSizeMode = AutoSizeMode.GrowAndShrink
        connect.Text = If(connectedToThis, "Disconnect", "Connect")
        connect.MinimumSize = New System.Drawing.Size(100, 35)
        connect.Anchor = AnchorStyles.None
        connect.BackColor = Color.FromArgb(80, 80, 80)

        Dim wps As New Button
        wps.Text = "WPS"
        wps.MinimumSize = New System.Drawing.Size(100, 35)
        wps.Anchor = AnchorStyles.None
        wps.BackColor = Color.FromArgb(80, 80, 80)


        Dim networkState As NetworkState = New NetworkState(network, secretVal, reconnectBox, hasWps, nameVal)

        buttonsRow.Controls.Add(connect)
        If hasWps Then
            wps.Tag = networkState
            wps.Name = "wps"
            If Not connectedToThis Then
                buttonsRow.Controls.Add(wps)
            End If
        End If

        connect.Tag = networkState
        view.Controls.Add(buttonsRow)

        If (String.IsNullOrEmpty(network.Ssid)) Then
            connect.Enabled = False
            AddHandler nameVal.TextChanged, Sub()
                                                If (String.IsNullOrEmpty(nameVal.Text)) Then
                                                    connect.Enabled = False
                                                Else
                                                    connect.Enabled = True
                                                End If
                                            End Sub
        End If

        AddHandler connect.Click, AddressOf connect_click
        AddHandler wps.Click, AddressOf wps_click

        view.MinimumSize = New System.Drawing.Size(399, 100)
        view.BackColor = Color.FromArgb(45, 45, 45)
        view.ForeColor = Color.White

    End Sub

    Private Async Sub ScanBtn_Click(sender As Object, e As EventArgs) Handles ScanBtn.Click
        SyncLock scanInProg
            If scanInProg = "YES" Then
                Return
            End If
            scanInProg = "YES"
        End SyncLock

        result = Nothing
        resultNetworkView = Nothing
        Dim enumResult = Await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector())
            adapter = Await WiFiAdapter.FromIdAsync(enumResult.ElementAt(0).Id)
            Await adapter.ScanAsync()
            Dim connected As Windows.Networking.Connectivity.ConnectionProfile = Await adapter.NetworkAdapter.GetConnectedProfileAsync()
            Dim connectedSsid As String = Nothing

            If (Not IsNothing(connected)) AndAlso connected.IsWlanConnectionProfile() Then
                connectedSsid = connected.WlanConnectionProfileDetails.GetConnectedSsid()
            End If
            If Not String.IsNullOrEmpty(connectedSsid) Then
                statusLbl.Text = "Successfully Connected."
            Else
                statusLbl.Text = "Not Connected."
            End If
            Dim report = adapter.NetworkReport
            NetworksList.Controls.Clear()
            NetworksList.AutoScroll = False
            NetworksList.AutoScroll = True
            For Each network In report.AvailableNetworks
                Dim connectedToThis As Boolean = (Not String.IsNullOrEmpty(connectedSsid)) And network.Ssid = connectedSsid
            Dim networkView As TableLayoutPanel = New TableLayoutPanel
            If connectedToThis Then
                resultNetworkView = networkView
            End If

            Dim wpsConf As WiFiWpsConfigurationResult = Await adapter.GetWpsConfigurationAsync(network)
            Dim hasWps As Boolean = wpsConf.SupportedWpsKinds.Contains(WiFiWpsKind.PushButton)
            populateNetworkView(networkView, network, connectedToThis, hasWps)
                NetworksList.Controls.Add(networkView)

            Next
        SyncLock scanInProg
            scanInProg = "NO"
        End SyncLock


    End Sub

    Private Sub connect_click(sender As Object, e As EventArgs)
        Dim connectBtn As Button = CType(sender, Button)

        If connectBtn.Text = "Connect" Then
            If Not IsNothing(resultNetworkView) Then
                disconnect(resultNetworkView.Controls().Find("connect", True)(0))
            End If
            connect(connectBtn)
        Else
            disconnect(connectBtn)
        End If


    End Sub
    Private Sub wps_click(sender As Object, e As EventArgs)
        If Not IsNothing(resultNetworkView) Then
            disconnect(resultNetworkView.Controls().Find("connect", True)(0))
        End If
        Dim connectBtn As Button = CType(sender, Button)
        Dim networkState As NetworkState = CType(connectBtn.Tag, NetworkState)
        Dim reconnect As WiFiReconnectionKind = If(networkState.autoConnect.Checked, WiFiReconnectionKind.Automatic, WiFiReconnectionKind.Manual)
        resultNetworkView = CType(connectBtn.Parent.Parent, TableLayoutPanel)
        result = adapter.ConnectAsync(networkState.network, reconnect, Nothing, "", WiFiConnectionMethod.WpsPushButton)

        result.Completed = New AsyncOperationCompletedHandler(Of WiFiConnectionResult)(AddressOf update_status_after_wps)
        connectBtn.Enabled = False
        lastPushWps = connectBtn
    End Sub

    Private Sub connect(ByRef connectBtn As Button)
        Dim networkState As NetworkState = CType(connectBtn.Tag, NetworkState)

        resultNetworkView = CType(connectBtn.Parent.Parent, TableLayoutPanel)

        Dim credential As PasswordCredential = New PasswordCredential()
        Dim reconnect As WiFiReconnectionKind = If(networkState.autoConnect.Checked, WiFiReconnectionKind.Automatic, WiFiReconnectionKind.Manual)

        If Not String.IsNullOrEmpty(networkState.secret.Text) Then
            credential.Password = networkState.secret.Text
        End If

        If String.IsNullOrEmpty(networkState.network.Ssid) Then
            result = adapter.ConnectAsync(networkState.network, reconnect, credential, networkState.name.Text)
        Else
            result = adapter.ConnectAsync(networkState.network, reconnect, credential)

        End If
        result.Completed = New AsyncOperationCompletedHandler(Of WiFiConnectionResult)(AddressOf update_status)

    End Sub

    Private Sub disconnect(ByRef connectBtn As Button)
        Dim networkState As NetworkState = CType(connectBtn.Tag, NetworkState)
        adapter.Disconnect()
        Dim networkView As TableLayoutPanel = New TableLayoutPanel
        populateNetworkView(networkView, networkState.network, False, networkState.isWPS)
        Dim oldIndex As Integer = NetworksList.Controls.GetChildIndex(connectBtn.Parent.Parent)
        NetworksList.SuspendLayout()
        NetworksList.Controls.Remove(connectBtn.Parent.Parent)
        NetworksList.Controls.Add(networkView)
        NetworksList.Controls.SetChildIndex(networkView, oldIndex)
        NetworksList.ResumeLayout()
        statusLbl.Text = "Not Connected."
        result = Nothing
        resultNetworkView = Nothing
    End Sub


    Private Sub update_status_after_wps()

        '        you use the BeginInvoke method
        '7:08 PM
        'which gives the control
        '7:09 PM
        'a subroutine
        '7:09 PM
        'to be executed by the control thread itself
        '7:09 PM
        'Not the currently running thread

        update_status()
        If Not IsNothing(lastPushWps) Then
            lastPushWps.BeginInvoke(Sub()
                                        lastPushWps.Enabled = True
                                    End Sub)
        End If

    End Sub

    Private Sub update_status()

        Select Case result.GetResults().ConnectionStatus
            Case WiFiConnectionStatus.Success
                statusLbl.BeginInvoke(Sub()
                                          resultNetworkView.SuspendLayout()
                                          statusLbl.Text = "Successfully Connected."
                                          resultNetworkView.Controls.RemoveByKey("reconnectRow")
                                          resultNetworkView.Controls.RemoveByKey("secretRow")
                                          resultNetworkView.Controls.RemoveByKey("nameRow")
                                          resultNetworkView.Controls.Find("buttonsRow", True)(0).Controls.RemoveByKey("wps")
                                          Dim connectBtn As Button = CType(resultNetworkView.Controls.Find("buttonsRow", True)(0).Controls.Find("connect", True)(0), Button)
                                          Dim networkState As NetworkState = CType(connectBtn.Tag, NetworkState)
                                          connectBtn.Text = "Disconnect"
                                          If (String.IsNullOrEmpty(networkState.network.Ssid)) Then
                                              resultNetworkView.Controls.Find("ssidVal", True)(0).Text = networkState.name.Text
                                          End If
                                          resultNetworkView.ResumeLayout()
                                      End Sub)

            Case WiFiConnectionStatus.Timeout
                statusLbl.BeginInvoke(Sub()
                                          statusLbl.Text = "Time out! Failed to connect."
                                      End Sub)
            Case WiFiConnectionStatus.NetworkNotAvailable
                statusLbl.BeginInvoke(Sub()
                                          statusLbl.Text = "Network Not Available! Failed to connect."
                                      End Sub)
            Case WiFiConnectionStatus.InvalidCredential
                statusLbl.BeginInvoke(Sub()
                                          statusLbl.Text = "Invalid Credential! Failed to connect."
                                      End Sub)
            Case WiFiConnectionStatus.AccessRevoked
                statusLbl.BeginInvoke(Sub()
                                          statusLbl.Text = "Access Revoked! Failed to connect."
                                      End Sub)
            Case WiFiConnectionStatus.UnsupportedAuthenticationProtocol
                statusLbl.BeginInvoke(Sub()
                                          statusLbl.Text = "Unsupported Authen. Protocol! Failed to connect."
                                      End Sub)
            Case WiFiConnectionStatus.UnspecifiedFailure
                statusLbl.BeginInvoke(Sub()
                                          statusLbl.Text = "Failed to connect."
                                      End Sub)

        End Select
    End Sub


End Class



Public Class NetworkState
    Public Sub New(ByRef net As WiFiAvailableNetwork, ByRef sec As TextBox, ByRef auto As CheckBox, ByVal wps As Boolean, ByRef ssid As TextBox)
        network = net
        secret = sec
        autoConnect = auto
        isWPS = wps
        name = ssid
    End Sub
    Public network As WiFiAvailableNetwork
    Public secret As TextBox
    Public name As TextBox
    Public autoConnect As CheckBox
    Public isWPS As Boolean
End Class
