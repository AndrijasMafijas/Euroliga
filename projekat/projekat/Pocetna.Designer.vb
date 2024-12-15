<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class pocetna
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
        Me.KatalogSatova = New System.Windows.Forms.Button()
        Me.Narudzbina = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'KatalogSatova
        '
        Me.KatalogSatova.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KatalogSatova.Location = New System.Drawing.Point(219, 36)
        Me.KatalogSatova.Name = "KatalogSatova"
        Me.KatalogSatova.Size = New System.Drawing.Size(154, 85)
        Me.KatalogSatova.TabIndex = 0
        Me.KatalogSatova.Text = "Katalog Satova"
        Me.KatalogSatova.UseVisualStyleBackColor = True
        '
        'Narudzbina
        '
        Me.Narudzbina.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Narudzbina.Location = New System.Drawing.Point(33, 36)
        Me.Narudzbina.Name = "Narudzbina"
        Me.Narudzbina.Size = New System.Drawing.Size(154, 85)
        Me.Narudzbina.TabIndex = 1
        Me.Narudzbina.Text = "Narudzbina"
        Me.Narudzbina.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(406, 36)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(154, 85)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Izlaz"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'pocetna
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gold
        Me.ClientSize = New System.Drawing.Size(584, 158)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Narudzbina)
        Me.Controls.Add(Me.KatalogSatova)
        Me.Name = "pocetna"
        Me.Text = "Pocetna"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents KatalogSatova As Button
    Friend WithEvents Narudzbina As Button
    Friend WithEvents Button1 As Button
End Class
