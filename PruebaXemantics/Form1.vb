Imports System.Data.SqlClient
Public Class Form1
    Public conn As New SqlConnection("Data Source=.\SQLEXPRESS;Initial Catalog=Prueba;Integrated Security=True")
    Dim cmd As New SqlCommand
    Dim leer As SqlDataReader
    Dim dt As New DataTable
    Dim dt2 As New DataTable
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        aramardatatable1()
        aramardatatable2()

        'llenarGrid()
        llenarGrid2()
    End Sub
    Public Sub aramardatatable1()
        'Armamaos el datatable
        dt.Columns.Add("ID")
        dt.Columns.Add("NOMBRE")
        dt.Columns.Add("APELLIDO")
        dt.Columns.Add("DIRECCION")
        dt.Columns.Add("NRO FACTURA")

    End Sub
    Public Sub aramardatatable2()
        'Armamaos el datatable
        dt2.Columns.Add("ID")
        dt2.Columns.Add("NOMBRE")
        dt2.Columns.Add("APELLIDO")
        dt2.Columns.Add("DIRECCION")


    End Sub
    'Public Sub llenarGrid()


    '    Try
    '        'Preguntamos si la conexion esta cerrada y la abrimos
    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If
    '        ' limpiamos el datatable 
    '        dt.Rows.Clear()
    '        'Creamos la consulta a la BBDD
    '        cmd.CommandText = "SELECT U.IDusuario, nombre, apellido, direccion, IDfactura FROM Usuario U INNER JOIN factura F ON U.IDusuario = F.IDusuario WHERE U.IDusuario = F.IDusuario"


    '        cmd.Connection = (conn)

    '        'Ejecutamos el datareader y el ciclo while llena el datatable
    '        leer = cmd.ExecuteReader
    '        While leer.Read
    '            dt.Rows.Add(leer.Item("IDusuario"), leer.Item("nombre"), leer.Item("apellido"), leer.Item("Direccion"), leer.Item("IDfactura"))
    '        End While
    '        leer.Close()
    '        'Llenamos el gridview
    '        DataGridView1.DataSource = dt


    '        'Liberamos memoria del servidor
    '        dt.Dispose()

    '    Catch ex As Exception

    '        'Cerramos la conexion
    '        If conn.State = ConnectionState.Open Then
    '            conn.Close()

    '        End If
    '    End Try
    'End Sub
    Public Sub llenarGrid2()

        dt2.Rows.Clear()
        cmd.CommandText = "select * from Usuario"
        cmd.Connection = (conn)
        conn.Open()
        leer = cmd.ExecuteReader
        While leer.Read
            dt2.Rows.Add(leer.Item("IDusuario"), leer.Item("nombre"), leer.Item("apellido"), leer.Item("direccion"))
        End While
        leer.Close()
        conn.Close()
        Me.DataGridView2.DataSource = dt2


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'boton guaradr con sp 
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Debe llenar todos los campos para realizar un registro nuevo ", +MsgBoxStyle.Critical)
        Else
            Try
                If conn.State = ConnectionState.Closed Then
                    cmd.Connection = conn
                    conn.Open()
                End If
                cmd.CommandType = Data.CommandType.StoredProcedure
                cmd.CommandText = "SP_inserta"


                cmd.Parameters.Add(New SqlParameter("@IDusuario", TextBox1.Text))
                cmd.Parameters.Add(New SqlParameter("@nombre", TextBox2.Text))
                cmd.Parameters.Add(New SqlParameter("@apellido", TextBox3.Text))
                cmd.Parameters.Add(New SqlParameter("@direccion", TextBox4.Text))



                If cmd.ExecuteNonQuery() Then


                    MsgBox("REGISTRO GUARADADO CORRECTAMENTE", vbExclamation)


                Else
                    MsgBox("no se guardo el formulario deseado" + vbCritical)
                End If

                llenarGrid2()
            Catch ex As Exception




                If conn.State = ConnectionState.Open Then
                    cmd.Connection = conn
                    conn.Close()
                End If

            End Try


        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        llenarGrid2()

    End Sub

    Private Sub Btn_Eliminar_Click(sender As Object, e As EventArgs) Handles Btn_Eliminar.Click
        'directo sin relaciones
        Dim res As String
        res = MsgBox("Seguro que desea eliminar este registro de la base de datos", MsgBoxStyle.YesNo, "Eliminar registro")
        If res = vbYes And DataGridView2.RowCount > 1 Then
            Dim sqlquery As String = "delete Usuario where IDusuario= '" & DataGridView2.SelectedRows(0).Cells(0).Value.ToString & "'"

            cmd.CommandText = sqlquery

            cmd.Connection = (conn)
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
            MsgBox("Registro eliminado con exito!!!", MsgBoxStyle.ApplicationModal)

            llenarGrid2()
        End If
    End Sub

    Private Sub Btn_Editar_Click(sender As Object, e As EventArgs) Handles Btn_Editar.Click
        Dim res As String
        res = MsgBox("Seguro que desea editar este registro ", MsgBoxStyle.YesNo, "Editar registro")
        If res = vbYes And DataGridView2.RowCount > 1 Then
            Form2.Show()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from Usuario where IDusuario= '" & DataGridView2.SelectedRows(0).Cells(0).Value.ToString & "'"
            cmd.Connection = (conn)
            conn.Open()
            leer = cmd.ExecuteReader()

            If leer.Read = True Then

                Form2.TextBox1.Text = leer.Item("IDusuario")
                Form2.TextBox2.Text = leer.Item("nombre")
                Form2.TextBox3.Text = leer.Item("apellido")
                Form2.TextBox4.Text = leer.Item("direccion")
                conn.Close()


            End If
        End If

        llenarGrid2()

    End Sub
End Class
