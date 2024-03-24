Public Class Form1
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        ' Check if both text boxes contain "admin"
        If Guna2TextBox1.Text.Trim() = "admin" AndAlso Guna2TextBox2.Text.Trim() = "admin" Then
            MessageBox.Show("Login successful")
            Form2.Show()
            Me.Hide()
        Else
            MessageBox.Show("Invalid credentials. Please try again.")
            ' You can handle unsuccessful login attempt here
        End If
    End Sub

End Class
