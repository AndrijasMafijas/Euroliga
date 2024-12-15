Public Class pocetna
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub KatalogSatova_Click(ByVal sender As Object, ByVal e As EventArgs) Handles KatalogSatova.Click
        Dim katalog As New katalog()
        katalog.Show()

    End Sub

    Private Sub Narudzbina_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Narudzbina.Click

        Dim narudzbina As New narudzbina()
        narudzbina.Show()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class
