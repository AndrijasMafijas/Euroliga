Imports System.Data.SqlClient

Public Class katalog
    Private Sub katalog_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub PogledajKatalog_Click(sender As Object, e As EventArgs) Handles PogledajKatalog.Click


        Dim connectionString As String = "Data Source=DESKTOP-9KSLFAT\SQLEXPRESS;Initial Catalog=projekat;Integrated Security=True"
        Dim query As String = "SELECT * FROM Proizvod"
        Dim con As New SqlConnection(connectionString)
        Dim a As New SqlDataAdapter(query, con)
        Dim tabela As New DataSet()
        con.Open()
        a.Fill(tabela, "Proizvod")
        DataGridView1.DataSource = tabela
        DataGridView1.DataMember = "Proizvod"

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim Naziv As String = TextBox1.Text.ToLower()
        Dim Cena As Decimal = NumericUpDown1.Value
        Dim Brend As String = TextBox2.Text.ToLower()
        Using connection As New SqlConnection("Data Source=DESKTOP-9KSLFAT\SQLEXPRESS;Initial Catalog=projekat;Integrated Security=True")
            connection.Open()


            Dim query As String = "INSERT INTO Proizvod (Naziv, Cena, Brend) VALUES (@Naziv, @Cena, @Brend )"
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@Naziv", Naziv)
            command.Parameters.AddWithValue("@Cena", Cena)
            command.Parameters.AddWithValue("@Brend", Brend)
            command.ExecuteNonQuery()
            connection.Close()

        End Using

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim idproizvoda As Integer = NumericUpDown2.Value
        Dim Naziv As String = TextBox1.Text.ToLower()
        Dim Cena As Decimal = NumericUpDown1.Value
        Dim Brend As String = TextBox2.Text.ToLower()

        Using connection As New SqlConnection("Data Source=DESKTOP-9KSLFAT\SQLEXPRESS;Initial Catalog=projekat;Integrated Security=True")
            connection.Open()


            Dim query As String = "Update Proizvod set Naziv=@Naziv, Cena=@Cena, Brend=@Brend where idproizvoda=@idproizvoda"
            Dim command As New SqlCommand(query, connection)

            command.Parameters.AddWithValue("@idproizvoda", Int(idproizvoda))
            command.Parameters.AddWithValue("@Naziv", Naziv)
            command.Parameters.AddWithValue("@Cena", Cena)
            command.Parameters.AddWithValue("@Brend", Brend)
            command.ExecuteNonQuery()
            connection.Close()

        End Using

    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim idproizvoda As Integer = NumericUpDown2.Value


        Using connection As New SqlConnection("Data Source=DESKTOP-9KSLFAT\SQLEXPRESS;Initial Catalog=projekat;Integrated Security=True")
            connection.Open()


            Dim query As String = "Delete Proizvod where idproizvoda=@idproizvoda"
            Dim command As New SqlCommand(query, connection)

            command.Parameters.AddWithValue("@idproizvoda", Int(idproizvoda))
            command.ExecuteNonQuery()
            connection.Close()

        End Using
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
End Class