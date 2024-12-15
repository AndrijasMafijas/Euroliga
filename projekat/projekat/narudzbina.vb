Imports System.Data
Imports System.Data.SqlClient
Public Class narudzbina
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub


    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim ime As String = TextBox1.Text.ToLower()
        Dim prezime As String = TextBox2.Text.ToLower()
        Dim adresa As String = TextBox3.Text.ToLower()
        Dim naziv_proizvoda As String = ComboBox1.Text.ToLower()
        Dim naziv_brenda As String = ComboBox2.Text.ToLower()

        Using connection As New SqlConnection("Data Source=DESKTOP-9KSLFAT\SQLEXPRESS;Initial Catalog=projekat;Integrated Security=True")
            connection.Open()


            Dim query As String = "INSERT INTO Narudzbina (ime, prezime, adresa, naziv_proizvoda, naziv_brenda ) VALUES (@ime, @prezime, @adresa, @naziv_proizvoda, @naziv_brenda )"
            Dim command As New SqlCommand(query, connection)

            command.Parameters.AddWithValue("@ime", ime)
            command.Parameters.AddWithValue("@prezime", prezime)
            command.Parameters.AddWithValue("@adresa", adresa)
            command.Parameters.AddWithValue("@naziv_proizvoda", naziv_proizvoda)
            command.Parameters.AddWithValue("@naziv_brenda", naziv_brenda)


            command.ExecuteNonQuery()
            connection.Close()

        End Using
    End Sub

    Private Sub narudzbina_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private Sub PopuniComboBox()
        Dim connectionString As String = "Data Source=DESKTOP-9KSLFAT\SQLEXPRESS;Initial Catalog=projekat;Integrated Security=True"
        Dim query As String = "SELECT Naziv, Brend FROM Proizvod"

        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As New SqlCommand(query, connection)
            Dim reader As SqlDataReader = command.ExecuteReader()

            While reader.Read()
                ComboBox1.Items.Add(reader("Naziv").ToString())
                ComboBox2.Items.Add(reader("Brend").ToString())
            End While

            reader.Close()
        End Using
    End Sub


    Public Sub New()
        InitializeComponent()
        PopuniComboBox()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Me.Close()
    End Sub


End Class
