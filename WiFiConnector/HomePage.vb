Imports Windows.Devices.WiFi
Imports Windows.Security.Credentials


Public Class HomePage
    Dim adapter As WiFiAdapter
    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result = Await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector())
        adapter = Await WiFiAdapter.FromIdAsync(result.ElementAt(0).Id)
        Await adapter.ScanAsync()
        Dim report = adapter.NetworkReport
        NetworksList.Controls.Clear()
        For Each network In report.AvailableNetworks
            Dim networkView As TableLayoutPanel = New TableLayoutPanel
            networkView.AutoSize = True
            networkView.AutoSizeMode = AutoSizeMode.GrowAndShrink
            Dim ssid As New Label

            ssid.AutoSize = True
            ssid.Text = "Ssid: " & If(network.Ssid.Length = 0, "Hidden Network", network.Ssid)
            networkView.Controls.Add(ssid)
            Dim bssid As New Label
            bssid.AutoSize = True
            bssid.Text = "Bssid: " & network.Bssid
            networkView.Controls.Add(bssid)
            Dim rssi As New Label
            rssi.AutoSize = True
            rssi.Text = "Rssi: " & network.NetworkRssiInDecibelMilliwatts & " dBm"
            networkView.Controls.Add(rssi)
            Dim ch As New Label
            ch.AutoSize = True
            ch.Text = "Ch. Freq.: " & network.ChannelCenterFrequencyInKilohertz & " kHz"
            networkView.Controls.Add(ch)
            Dim auth As New Label
            auth.AutoSize = True
            auth.Text = "Authen.: " & network.SecuritySettings.NetworkAuthenticationType.ToString()
            networkView.Controls.Add(auth)
            Dim encr As New Label
            encr.AutoSize = True
            encr.Text = "Encryption: " & network.SecuritySettings.NetworkEncryptionType.ToString()
            networkView.Controls.Add(encr)


            Dim reconnectRow As New FlowLayoutPanel
            reconnectRow.AutoSize = True
            Dim reconnectBox As New CheckBox
            reconnectBox.AutoSize = True
            Dim reconnect As New Label
            reconnect.Text = "Connect Automatically"
            reconnect.AutoSize = True
            reconnectRow.Controls.Add(reconnectBox)
            reconnectRow.Controls.Add(reconnect)

            networkView.Controls.Add(reconnectRow)

            Dim secretVal As New TextBox
            If Not network.SecuritySettings.NetworkAuthenticationType = Windows.Networking.Connectivity.NetworkAuthenticationType.None And Not network.SecuritySettings.NetworkAuthenticationType = Windows.Networking.Connectivity.NetworkAuthenticationType.Open80211 Then
                Dim secretRow As New FlowLayoutPanel
                secretRow.AutoSize = True
                Dim secret As New Label
                secret.Text = "Secret Key:"
                secret.AutoSize = True
                secretVal.MinimumSize = New Size(130, 25)
                secretRow.Controls.Add(secret)
                secretRow.Controls.Add(secretVal)
                networkView.Controls.Add(secretRow)
            End If


            Dim connect As New Button
            connect.Text = "Connect"
            connect.MinimumSize = New Size(100, 30)
            networkView.Controls.Add(connect)
            connect.Anchor = AnchorStyles.None
            connect.BackColor = Color.DimGray
            connect.Tag = New NetworkState(network, secretVal, reconnectBox)
            AddHandler connect.Click, AddressOf Me.connect_click

            networkView.MinimumSize = New Size(250, 100)
            networkView.BackColor = Color.FromArgb(45, 45, 45)
            networkView.ForeColor = Color.White
            NetworksList.Controls.Add(networkView)
        Next


    End Sub

    Private Async Sub connect_click(sender As Object, e As EventArgs)
        Dim connectBtn As Button = CType(sender, Button)
        Dim networkState As NetworkState = CType(connectBtn.Tag, NetworkState)
        Dim credential As PasswordCredential = New PasswordCredential()
        If Not String.IsNullOrEmpty(networkState.secret.Text) Then
            credential.Password = networkState.secret.Text
        End If

        Dim reconnect As WiFiReconnectionKind = If(networkState.autoConnect.Checked, WiFiReconnectionKind.Automatic, WiFiReconnectionKind.Manual)
        Console.WriteLine(reconnect)
        Await adapter.ConnectAsync(networkState.network, reconnect, credential)

    End Sub
End Class


Public Class NetworkState
    Public Sub New(ByRef net As WiFiAvailableNetwork, ByRef sec As TextBox, ByRef auto As CheckBox)
        network = net
        secret = sec
        autoConnect = auto
    End Sub
    Public network As WiFiAvailableNetwork
    Public secret As TextBox
    Public autoConnect As CheckBox
End Class
