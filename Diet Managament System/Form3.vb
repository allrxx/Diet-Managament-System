Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient

Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2GroupBox3.Visible = False
        Guna2GroupBox2.Visible = False
    End Sub
    Private Sub LoadUserData()
        Dim connectionString As String = "server=localhost;user=root;password=admin;database=dms;"
        Dim query As String = "SELECT * FROM user"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                Dim dataTable As New DataTable()
                Dim dataAdapter As New MySqlDataAdapter(command)

                ' Fill the DataTable with data from the database
                dataAdapter.Fill(dataTable)

                ' Bind the DataTable to the Guna2DataGridView1
                Guna2DataGridView1.DataSource = dataTable
            End Using
        End Using
    End Sub
    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Guna2GroupBox3.Visible = True
        LoadUserData()
    End Sub
    Private Sub LoadplanData()
        Dim connectionString As String = "server=localhost;user=root;password=admin;database=dms;"
        Dim query As String = "SELECT * FROM diet_plan"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                Dim dataTable As New DataTable()
                Dim dataAdapter As New MySqlDataAdapter(command)

                ' Fill the DataTable with data from the database
                dataAdapter.Fill(dataTable)

                ' Bind the DataTable to the Guna2DataGridView1
                Guna2DataGridView2.DataSource = dataTable
            End Using
        End Using
    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Guna2GroupBox2.Visible = True
        LoadplanData()
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        ' Get the selected userid and planid from DataGridViews
        Dim selectedUserId As Integer = Convert.ToInt32(Guna2DataGridView1.CurrentRow.Cells("userid").Value)
        Dim selectedPlanId As Integer = Convert.ToInt32(Guna2DataGridView2.CurrentRow.Cells("plan_id").Value)

        ' Get the start date and end date from DateTimePickers
        Dim startDate As Date = Guna2DateTimePicker1.Value.Date
        Dim endDate As Date? = If(Guna2DateTimePicker2.Checked, Guna2DateTimePicker2.Value.Date, Nothing)

        ' Insert data into the "userdp" table
        Dim connectionString As String = "server=localhost;user=root;password=admin;database=dms;"
        Dim query As String = "INSERT INTO userdp (userid, planid, startdate, enddate) VALUES (@userid, @planid, @startdate, @enddate)"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@userid", selectedUserId)
                command.Parameters.AddWithValue("@planid", selectedPlanId)
                command.Parameters.AddWithValue("@startdate", startDate)
                command.Parameters.AddWithValue("@enddate", If(endDate IsNot Nothing, endDate, DBNull.Value))

                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("User diet plan added successfully.")
    End Sub

End Class