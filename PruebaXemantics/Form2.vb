Imports System.Data.SqlClient
Public Class Form2
    Public conn As New SqlConnection("Data Source=.\SQLEXPRESS;Initial Catalog=Prueba;Integrated Security=True")
    Dim cmd As New SqlCommand
    Dim leer As SqlDataReader
    Dim dt As New DataTable
    Dim dt2 As New DataTable
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Debe llenar todos los campos para realizar la modificacion ", +MsgBoxStyle.Critical)
        Else
            Try
                If conn.State = ConnectionState.Closed Then
                    cmd.Connection = conn
                    conn.Open()
                End If
                cmd.CommandType = Data.CommandType.StoredProcedure
                cmd.CommandText = "SP_Actualizar_Usuario"


                cmd.Parameters.Add(New SqlParameter("@IDusuario", TextBox1.Text))
                cmd.Parameters.Add(New SqlParameter("@nombre", TextBox2.Text))
                cmd.Parameters.Add(New SqlParameter("@apellido", TextBox3.Text))
                cmd.Parameters.Add(New SqlParameter("@direccion", TextBox4.Text))



                If cmd.ExecuteNonQuery() Then


                    MsgBox("REGISTRO ACTUALIZADO CORRECTAMENTE", vbExclamation)


                Else
                    MsgBox("no se guardo el formulario deseado" + vbCritical)
                End If


            Catch ex As Exception




                If conn.State = ConnectionState.Open Then
                    cmd.Connection = conn
                    conn.Close()



                End If

            End Try


        End If
        Me.Close()
    End Sub
End Class