<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HomePage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HomePage))
        Me.ScanBtn = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NetworksList = New System.Windows.Forms.TableLayoutPanel()
        Me.statusPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.statusLbl = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.statusPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'ScanBtn
        '
        Me.ScanBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.ScanBtn.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ScanBtn.ForeColor = System.Drawing.Color.White
        Me.ScanBtn.Location = New System.Drawing.Point(46, 77)
        Me.ScanBtn.Name = "ScanBtn"
        Me.ScanBtn.Size = New System.Drawing.Size(400, 46)
        Me.ScanBtn.TabIndex = 0
        Me.ScanBtn.Text = "Scan available networks"
        Me.ScanBtn.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(111, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(251, 45)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "WiFi Connector"
        '
        'NetworksList
        '
        Me.NetworksList.AutoScroll = True
        Me.NetworksList.AutoSize = True
        Me.NetworksList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.NetworksList.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble
        Me.NetworksList.ColumnCount = 1
        Me.NetworksList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.NetworksList.Font = New System.Drawing.Font("Segoe UI", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NetworksList.Location = New System.Drawing.Point(46, 138)
        Me.NetworksList.MaximumSize = New System.Drawing.Size(399, 545)
        Me.NetworksList.MinimumSize = New System.Drawing.Size(399, 545)
        Me.NetworksList.Name = "NetworksList"
        Me.NetworksList.RowCount = 1
        Me.NetworksList.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.NetworksList.Size = New System.Drawing.Size(399, 545)
        Me.NetworksList.TabIndex = 2
        '
        'statusPanel
        '
        Me.statusPanel.AutoScroll = True
        Me.statusPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble
        Me.statusPanel.ColumnCount = 2
        Me.statusPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.statusPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.statusPanel.Controls.Add(Me.statusLbl, 1, 0)
        Me.statusPanel.Controls.Add(Me.Label2, 0, 0)
        Me.statusPanel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.statusPanel.Location = New System.Drawing.Point(46, 706)
        Me.statusPanel.Name = "statusPanel"
        Me.statusPanel.RowCount = 1
        Me.statusPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.statusPanel.Size = New System.Drawing.Size(399, 43)
        Me.statusPanel.TabIndex = 3
        '
        'statusLbl
        '
        Me.statusLbl.AutoSize = True
        Me.statusLbl.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.statusLbl.ForeColor = System.Drawing.Color.White
        Me.statusLbl.Location = New System.Drawing.Point(88, 10)
        Me.statusLbl.Margin = New System.Windows.Forms.Padding(10, 7, 0, 0)
        Me.statusLbl.Name = "statusLbl"
        Me.statusLbl.Size = New System.Drawing.Size(0, 21)
        Me.statusLbl.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(13, 10)
        Me.Label2.Margin = New System.Windows.Forms.Padding(10, 7, 10, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 21)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Status"
        '
        'HomePage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(494, 772)
        Me.Controls.Add(Me.statusPanel)
        Me.Controls.Add(Me.NetworksList)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ScanBtn)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(510, 811)
        Me.MinimumSize = New System.Drawing.Size(510, 811)
        Me.Name = "HomePage"
        Me.Text = "WiFi Connector"
        Me.statusPanel.ResumeLayout(False)
        Me.statusPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ScanBtn As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents NetworksList As TableLayoutPanel
    Friend WithEvents statusPanel As TableLayoutPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents statusLbl As Label
End Class
