Imports System.Text
Imports MySql.Data.MySqlClient

Public Class Form2

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load suitable examples into the ComboBoxes
        LoadGenderComboBox()
        LoadMedicalConditionComboBox()
        Guna2GroupBox2.Visible = False
        Guna2GroupBox3.Visible = False
        Guna2GroupBox4.Visible = False
    End Sub

    Private Sub LoadGenderComboBox()
        ' Assuming Guna2ComboBox1 is used for gender selection
        Guna2ComboBox1.Items.AddRange({"Male", "Female", "Other"})
    End Sub

    Private Sub LoadMedicalConditionComboBox()
        ' Assuming Guna2ComboBox2 is used for medical condition selection
        Guna2ComboBox2.Items.AddRange({"Diabetes", "Hypertension", "Asthma", "Heart Disease", "None"})
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim connectionString As String = "server=localhost;user=root;password=admin;database=dms;"
        Dim query As String = "INSERT INTO user (username, age, weight, height, gender, medical_condition, medical_history) VALUES (@username, @age, @weight, @height, @gender, @medical_condition, @medical_history)"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                connection.Open()
                command.Parameters.AddWithValue("@username", Guna2TextBox1.Text)
                command.Parameters.AddWithValue("@age", Integer.Parse(Guna2TextBox2.Text))
                command.Parameters.AddWithValue("@weight", Decimal.Parse(Guna2TextBox3.Text))
                command.Parameters.AddWithValue("@height", Decimal.Parse(Guna2TextBox4.Text))
                command.Parameters.AddWithValue("@gender", Guna2ComboBox1.SelectedItem.ToString())
                command.Parameters.AddWithValue("@medical_condition", Guna2ComboBox2.SelectedItem.ToString())
                command.Parameters.AddWithValue("@medical_history", Guna2TextBox5.Text)
                command.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("User added successfully.")
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Guna2GroupBox2.Visible = True
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Guna2GroupBox3.Visible = True

        ' Call a method to load data into Guna2DataGridView1
        LoadUserData()
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

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Guna2GroupBox4.Visible = True
    End Sub

    Private Sub Guna2Button7_Click(sender As Object, e As EventArgs) Handles Guna2Button7.Click
        Dim connectionString As String = "server=localhost;user=root;password=admin;database=dms;"
        Dim query As String = "INSERT INTO diet_plan (Planname, Description) VALUES (@Planname, @Description)"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@Planname", Guna2TextBox10.Text)
                command.Parameters.AddWithValue("@Description", Guna2TextBox9.Text)

                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Diet plan added successfully.")
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        Me.Hide()
        Form3.Show()
    End Sub

    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        ' Retrieve data from the "userdp" table
        Dim connectionString As String = "server=localhost;user=root;password=admin;database=dms;"
        Dim query As String = "SELECT * FROM userdp"

        Dim userData As New StringBuilder()

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                connection.Open()
                Dim reader As MySqlDataReader = command.ExecuteReader()

                While reader.Read()
                    ' Append userdp data to StringBuilder
                    userData.AppendLine($"userdpid: {reader("userdpid")}, userid: {reader("userid")}, planid: {reader("planid")}, startdate: {reader("startdate")}, enddate: {If(reader("enddate") IsNot DBNull.Value, reader("enddate"), "NULL")}")
                End While

                reader.Close()
            End Using
        End Using

        ' Display userdp data in MessageBox
        MessageBox.Show(userData.ToString(), "User Diet Plans")
    End Sub

End Class
