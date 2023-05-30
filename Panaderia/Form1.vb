Imports System.ComponentModel.Design.ObjectSelectorEditor
Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Imports Raw


Public Class Form1
    Dim ventas_lista As New List(Of Ventas)()

    Dim ventas_lista_safe As New List(Of Ventas)()
    Dim productos_lista As New List(Of Producto)()

    Public Class Producto
        Public Property nombre_producto
        Public Property precio
        Public Property id

    End Class
    Public Class Ventas
        Public Property nombre_producto As String
        Public Property unidades
        Public Property precio
        Public Property id



    End Class

    Private Sub ListaCompras_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub DUDLista_SelectedItemChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'BOTONES POR UN CAMBIO DE PRECIO NO CORRESPONDE AL PRECIO REAL
        'bolleria 2.20 pasa a 2.50
        'pan de 0.80 a 0.6
        'pan de 1.10 a 0.90
        Dim total_inicio As Double
        cargarlista()
        calcular_total()
    End Sub


    Private Sub btnventa_Click(sender As Object, e As EventArgs) Handles btnventa.Click
        guardar()
        Dim streamToPrint As StreamReader
        Dim printFont As Font

        Dim pd As New PrintDocument()

        pd.PrinterSettings.PrinterName = "Brother MFC-L2710DW series"
        pd.PrinterSettings.PrintToFile = False
        pd.PrinterSettings.PrintRange = PrintRange.AllPages
        pd.PrinterSettings.Copies = 1
        pd.DocumentName = "Panaderia_Ticket"
        AddHandler pd.PrintPage, AddressOf print_PrintPage

        pd.Print()
        System.IO.File.WriteAllText(Application.StartupPath + "\log_temp.txt", "")
    End Sub
    Private Sub print_PrintPage(ByVal sender As Object,
                            ByVal e As PrintPageEventArgs)
        Dim fecha_actual As String
        fecha_actual = CDate(Now).ToShortDateString
        Dim lector As StreamReader
        Dim linea As String
        Dim ventas_safe As New Ventas
        lector = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath + "\log.txt")

        Do
            linea = lector.ReadLine()
            If linea IsNot Nothing Then
                Dim lineaArray() As String = Split(linea, ";")

                If lineaArray(0) = fecha_actual Then

                    ventas_safe = New Ventas With {.nombre_producto = lineaArray(1).ToString, .precio = 0, .unidades = lineaArray(2).ToString, .id = lineaArray(3)}
                    ventas_lista.Add(ventas_safe)
                End If


            Else
                Exit Do

            End If
        Loop
        lector.Close()
        ' Este evento se producirá cada vez que se imprima una nueva página


        ' imprimimos la cadena en el margen izquierdo
        Dim xPos As Single = e.MarginBounds.Left
        ' La fuente a usar
        Dim prFont As New Font("Arial", 12, FontStyle.Bold)
        ' la posición superior
        Dim yPos As Single = prFont.GetHeight(e.Graphics)

        ' imprimimos la cadena
        ' e.Graphics.DrawString(ventas_lista(ventas_lista.Count).unidades, prFont, Brushes.Black, xPos, yPos)
        e.Graphics.DrawString(ventas_lista(ventas_lista.Count - 1).nombre_producto + ": " +
                             ventas_lista(ventas_lista.Count - 1).unidades & vbCrLf +
                             ventas_lista(ventas_lista.Count - 2).nombre_producto + ": " +
                             ventas_lista(ventas_lista.Count - 2).unidades & vbCrLf +
                             ventas_lista(ventas_lista.Count - 3).nombre_producto + ": " +
                             ventas_lista(ventas_lista.Count - 3).unidades & vbCrLf +
                             ventas_lista(ventas_lista.Count - 4).nombre_producto + ": " +
                             ventas_lista(ventas_lista.Count - 4).unidades & vbCrLf +
                             ventas_lista(ventas_lista.Count - 5).nombre_producto + ": " +
                             ventas_lista(ventas_lista.Count - 5).unidades & vbCrLf +
                             ventas_lista(ventas_lista.Count - 6).nombre_producto + ": " +
                             ventas_lista(ventas_lista.Count - 6).unidades & vbCrLf +
                             ventas_lista(ventas_lista.Count - 7).nombre_producto + ": " +
                             ventas_lista(ventas_lista.Count - 7).unidades & vbCrLf +
                             ventas_lista(ventas_lista.Count - 8).nombre_producto + ": " +
                             ventas_lista(ventas_lista.Count - 8).unidades & vbCrLf +
                             ventas_lista(ventas_lista.Count - 9).nombre_producto + ": " +
                             ventas_lista(ventas_lista.Count - 9).unidades & vbCrLf +
                             ventas_lista(ventas_lista.Count - 10).nombre_producto + ": " +
                             ventas_lista(ventas_lista.Count - 10).unidades & vbCrLf +
                             ventas_lista(ventas_lista.Count - 11).nombre_producto + ": " +
                             ventas_lista(ventas_lista.Count - 11).unidades & vbCrLf +
                             ventas_lista(ventas_lista.Count - 12).nombre_producto + ": " +
                             ventas_lista(ventas_lista.Count - 12).unidades & vbCrLf +
                             ventas_lista(ventas_lista.Count - 13).nombre_producto + ": " +
                             ventas_lista(ventas_lista.Count - 13).unidades & vbCrLf +
                             ventas_lista(ventas_lista.Count - 14).nombre_producto + ": " +
                             ventas_lista(ventas_lista.Count - 14).unidades & vbCrLf, prFont, Brushes.Black, xPos, yPos)

        e.HasMorePages = False

    End Sub

    Public Sub cargarlista()
        Dim fecha_actual As String
        fecha_actual = CDate(Now).ToShortDateString
        Dim lector As StreamReader
        Dim linea As String
        Dim ventas_safe As New Ventas
        lector = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath + "\log.txt")

        Do

            linea = lector.ReadLine()
            If linea IsNot Nothing Then
                Dim lineaArray() As String = Split(linea, ";")

                If lineaArray(0) = fecha_actual Then

                    ventas_safe = New Ventas With {.nombre_producto = lineaArray(1).ToString, .precio = 0, .unidades = lineaArray(2).ToString, .id = lineaArray(3)}
                    ventas_lista.Add(ventas_safe)
                End If


            Else
                Exit Do

            End If
        Loop
        lector.Close()
        Dim contador As Integer = 0
        Dim unidades As Integer = 0
        If ventas_lista.Count <> 0 Then


            Do
                If ventas_lista(contador).id = 0 Then
                    unidades = ventas_lista(contador).unidades
                    txtpan110.Text = unidades.ToString
                End If
                If ventas_lista(contador).id = 2 Then
                    unidades = Convert.ToInt32(txtpan1.Text) + ventas_lista(contador).unidades
                    txtpan1.Text = unidades.ToString
                End If
                If ventas_lista(contador).id = 1 Then
                    unidades = Convert.ToInt32(txtpan08.Text) + ventas_lista(contador).unidades
                    txtpan08.Text = unidades.ToString
                End If
                If ventas_lista(contador).id = 3 Then
                    unidades = Convert.ToInt32(txtbollo120.Text) + ventas_lista(contador).unidades
                    txtbollo120.Text = unidades.ToString
                End If
                If ventas_lista(contador).id = 4 Then
                    unidades = Convert.ToInt32(txtbollo150.Text) + ventas_lista(contador).unidades
                    txtbollo150.Text = unidades.ToString
                End If
                If ventas_lista(contador).id = 5 Then
                    unidades = Convert.ToInt32(txtbollo170.Text) + ventas_lista(contador).unidades
                    txtbollo170.Text = unidades.ToString
                End If
                If ventas_lista(contador).id = 6 Then
                    unidades = Convert.ToInt32(txtbollo2.Text) + ventas_lista(contador).unidades
                    txtbollo2.Text = unidades.ToString
                End If
                If ventas_lista(contador).id = 7 Then
                    unidades = Convert.ToInt32(txtbollo220.Text) + ventas_lista(contador).unidades
                    txtbollo220.Text = unidades.ToString
                End If
                If ventas_lista(contador).id = 8 Then
                    unidades = Convert.ToInt32(txtpan180.Text) + ventas_lista(contador).unidades
                    txtpan180.Text = unidades.ToString
                End If
                If ventas_lista(contador).id = 9 Then
                    unidades = Convert.ToInt32(txtpatata.Text) + ventas_lista(contador).unidades
                    txtpatata.Text = unidades.ToString
                End If
                If ventas_lista(contador).id = 10 Then
                    unidades = Convert.ToInt32(txtfrutos.Text) + ventas_lista(contador).unidades
                    txtfrutos.Text = unidades.ToString
                End If
                If ventas_lista(contador).id = 11 Then
                    unidades = Convert.ToInt32(txtrefresco.Text) + ventas_lista(contador).unidades
                    txtrefresco.Text = unidades.ToString
                End If
                If ventas_lista(contador).id = 12 Then
                    unidades = Convert.ToInt32(txtcerveza.Text) + ventas_lista(contador).unidades
                    txtcerveza.Text = unidades.ToString
                End If
                If ventas_lista(contador).id = 13 Then
                    unidades = Convert.ToInt32(txtlitro.Text) + ventas_lista(contador).unidades
                    txtlitro.Text = unidades.ToString
                End If

                contador = contador + 1
            Loop Until contador = ventas_lista.Count

        End If

    End Sub


    Private Sub guardar()
        Dim fechamod As String
        fechamod = CDate(Now).ToShortDateString

        Dim objWriter As StreamWriter
        Dim txtlog As String
        Dim contador As Integer
        contador = 0
        Dim i As Integer
        i = 13
        Try
            objWriter = New StreamWriter(Application.StartupPath + "\log.txt", True)

            Do

                Select Case contador < 14
                    Case contador = 0

                        txtlog = fechamod + ";Barra Pan 0.9€ ;" + txtpan110.Text + ";" + "0"
                        objWriter.WriteLine(txtlog)

                        contador = contador + 1
                    Case contador = 2
                        txtlog = fechamod + ";Barra Pan 1€ ;" + txtpan1.Text + ";" + "2"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1

                    Case contador = 1

                        txtlog = fechamod + ";Barra Pan 0.6€ ;" + txtpan08.Text + ";" + "1"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1

                    Case contador = 3
                        txtlog = fechamod + ";Bolleria 1.20€ ;" + txtbollo120.Text + ";" + "3"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 4
                        txtlog = fechamod + ";Bolleria 1.50€ ;" + txtbollo150.Text + ";" + "4"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 5
                        txtlog = fechamod + ";Bolleria 1.70€ ;" + txtbollo170.Text + ";" + "5"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 6
                        txtlog = fechamod + ";Bolleria 2€ ;" + txtbollo2.Text + ";" + "6"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 7
                        txtlog = fechamod + ";Bolleria 2.50€ ;" + txtbollo220.Text + ";" + "7"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 8

                        txtlog = fechamod + ";Barra Pan 1.80€ ;" + txtpan180.Text + ";" + "8"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 9

                        txtlog = fechamod + ";Patatas fritas 2€ ;" + txtpatata.Text + ";" + "9"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 10

                        txtlog = fechamod + ";Frutos Secos 2.5€ ;" + txtfrutos.Text + ";" + "10"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 11

                        txtlog = fechamod + ";Lata Refresco 1€ ;" + txtrefresco.Text + ";" + "11"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 12

                        txtlog = fechamod + ";Lata Cerveza 1€ ;" + txtcerveza.Text + ";" + "12"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1

                    Case contador = 13

                        txtlog = fechamod + ";Litro Cerveza€ ;" + txtlitro.Text + ";" + "13"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1

                End Select
            Loop Until contador = 14
            objWriter.Close()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub guardar_temporal()
        System.IO.File.WriteAllText(Application.StartupPath + "\log_temp.txt", "")
        Dim objWriter As StreamWriter
        Dim txtlog As String
        Dim contador As Integer
        contador = 0
        Dim i As Integer
        i = 7
        Try
            objWriter = New StreamWriter(Application.StartupPath + "\log_temp.txt", True)

            Do

                Select Case contador < 14
                    Case contador = 0

                        txtlog = "Barra Pan 0.9€ ;" + txtpan110.Text + ";"
                        objWriter.WriteLine(txtlog)

                        contador = contador + 1
                    Case contador = 2
                        txtlog = "Barra Pan 1€ ;" + txtpan1.Text + ";"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1

                    Case contador = 1

                        txtlog = "Barra Pan 0.6€ ;" + txtpan08.Text + ";"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1

                    Case contador = 3
                        txtlog = "Bolleria 1.20€ ;" + txtbollo120.Text + ";"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 4
                        txtlog = "Bolleria 1.50€ ;" + txtbollo150.Text + ";"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 5
                        txtlog = "Bolleria 1.70€ ;" + txtbollo170.Text + ";"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 6
                        txtlog = "Bolleria 2€ ;" + txtbollo2.Text + ";"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 7
                        txtlog = "Bolleria 2.50€ ;" + txtbollo220.Text + ";"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 8

                        txtlog = "Barra Pan 1.8€ ;" + txtpan180.Text + ";"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 9

                        txtlog = "Patatas Fritas 2€ ;" + txtpatata.Text + ";"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 10

                        txtlog = "Frutos Secos 2.5€ ;" + txtfrutos.Text + ";"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 11

                        txtlog = "Latas Refresco 1€ ;" + txtrefresco.Text + ";"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 12

                        txtlog = "Lata Cerveza 1€ ;" + txtcerveza.Text + ";"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                    Case contador = 13

                        txtlog = "Litro Cerveza 2€ ;" + txtlitro.Text + ";"
                        objWriter.WriteLine(txtlog)
                        contador = contador + 1
                End Select
            Loop Until contador = 14
            objWriter.Close()


        Catch ex As Exception

        End Try
    End Sub
    Private Sub calcular_total()
        Dim total

        total = Convert.ToInt16(txtpan110.Text) * 0.9 + Convert.ToInt16(txtpan1.Text) * 1 + Convert.ToInt16(txtpan08.Text) * 0.6 + Convert.ToInt16(txtbollo120.Text) * 1.2 + Convert.ToInt16(txtbollo150.Text) * 1.5 + Convert.ToInt16(txtbollo170.Text) * 1.7 + Convert.ToInt16(txtbollo2.Text) * 2 + Convert.ToInt16(txtbollo220.Text) * 2.5 +
            Convert.ToInt16(txtpan180.Text) * 1.8 + Convert.ToInt16(txtpatata.Text) * 2 + Convert.ToInt16(txtfrutos.Text) * 2.5 + Convert.ToInt16(txtrefresco.Text) * 1 + Convert.ToInt16(txtcerveza.Text) * 1 + Convert.ToInt16(txtlitro.Text) * 2


        txttotal.Text = Format(total, "0.00")
    End Sub
    Private Sub btnpan110_Click(sender As Object, e As EventArgs) Handles btnpan110.Click
        Dim unidades As Integer
        Int32.TryParse(txtpan110.Text, unidades)
        txtpan110.Text = unidades + 1
        calcular_total()
        guardar_temporal()
    End Sub

    Private Sub btnpan1_Click(sender As Object, e As EventArgs) Handles btnpan1.Click
        Dim unidades As Integer
        Int32.TryParse(txtpan1.Text, unidades)
        txtpan1.Text = unidades + 1
        calcular_total()
        guardar()
        guardar_temporal()

    End Sub

    Private Sub btnpan08_Click(sender As Object, e As EventArgs) Handles btnpan08.Click
        Dim unidades As Integer
        Int32.TryParse(txtpan08.Text, unidades)
        txtpan08.Text = unidades + 1
        calcular_total()
        guardar_temporal()
    End Sub

    Private Sub btnbollo120_Click(sender As Object, e As EventArgs) Handles btnbollo120.Click
        Dim unidades As Integer
        Int32.TryParse(txtbollo120.Text, unidades)
        txtbollo120.Text = unidades + 1
        calcular_total()
        guardar_temporal()
    End Sub

    Private Sub btnbollo150_Click(sender As Object, e As EventArgs) Handles btnbollo150.Click
        Dim unidades As Integer
        Int32.TryParse(txtbollo150.Text, unidades)
        txtbollo150.Text = unidades + 1
        calcular_total()
        guardar_temporal()
    End Sub

    Private Sub btnbollo170_Click(sender As Object, e As EventArgs) Handles btnbollo170.Click
        Dim unidades As Integer
        Int32.TryParse(txtbollo170.Text, unidades)
        txtbollo170.Text = unidades + 1
        calcular_total()
        guardar_temporal()
    End Sub

    Private Sub btnbollo2_Click(sender As Object, e As EventArgs) Handles btnbollo2.Click
        Dim unidades As Integer
        Int32.TryParse(txtbollo2.Text, unidades)
        txtbollo2.Text = unidades + 1
        calcular_total()
        guardar_temporal()
    End Sub

    Private Sub btnbollo220_Click(sender As Object, e As EventArgs) Handles btnbollo220.Click
        Dim unidades As Integer
        Int32.TryParse(txtbollo220.Text, unidades)
        txtbollo220.Text = unidades + 1
        calcular_total()
        guardar_temporal()
    End Sub

    Private Sub btnpan180_Click(sender As Object, e As EventArgs) Handles btnpan180.Click
        Dim unidades As Integer
        Int32.TryParse(txtpan180.Text, unidades)
        txtpan180.Text = unidades + 1
        calcular_total()
        guardar_temporal()
    End Sub

    Private Sub btncerve1_Click(sender As Object, e As EventArgs) Handles btncerve1.Click
        Dim unidades As Integer
        Int32.TryParse(txtcerveza.Text, unidades)
        txtcerveza.Text = unidades + 1
        calcular_total()
        guardar_temporal()
    End Sub

    Private Sub btnrefre1_Click(sender As Object, e As EventArgs) Handles btnrefre1.Click
        Dim unidades As Integer
        Int32.TryParse(txtrefresco.Text, unidades)
        txtrefresco.Text = unidades + 1
        calcular_total()
        guardar_temporal()
    End Sub

    Private Sub btnlitro_Click(sender As Object, e As EventArgs) Handles btnlitro.Click
        Dim unidades As Integer
        Int32.TryParse(txtlitro.Text, unidades)
        txtlitro.Text = unidades + 1
        calcular_total()
        guardar_temporal()
    End Sub

    Private Sub btnpatata_Click(sender As Object, e As EventArgs) Handles btnpatata.Click
        Dim unidades As Integer
        Int32.TryParse(txtpatata.Text, unidades)
        txtpatata.Text = unidades + 1
        calcular_total()
        guardar_temporal()
    End Sub

    Private Sub btnfrutos_Click(sender As Object, e As EventArgs) Handles btnfrutos.Click
        Dim unidades As Integer
        Int32.TryParse(txtfrutos.Text, unidades)
        txtfrutos.Text = unidades + 1
        calcular_total()
        guardar_temporal()
    End Sub

    Private Sub btnrestar90_Click(sender As Object, e As EventArgs) Handles btnrestar90.Click
        Dim unidades As Integer
        Int32.TryParse(txtpan110.Text, unidades)
        txtpan110.Text = unidades - 1
        calcular_total()
        guardar_temporal()
    End Sub
    Private Sub btnrestarpan1_Click(sender As Object, e As EventArgs) Handles btnrestarpan1.Click
        Dim unidades As Integer
        Int32.TryParse(txtpan1.Text, unidades)
        txtpan1.Text = unidades - 1
        calcular_total()
        guardar_temporal()
    End Sub
    Private Sub btnrestarpan60_Click(sender As Object, e As EventArgs) Handles btnrestarpan60.Click
        Dim unidades As Integer
        Int32.TryParse(txtpan08.Text, unidades)
        txtpan08.Text = unidades - 1
        calcular_total()
        guardar_temporal()
    End Sub
    Private Sub btnrestarpan180_Click(sender As Object, e As EventArgs) Handles btnrestar180.Click
        Dim unidades As Integer
        Int32.TryParse(txtpan180.Text, unidades)
        txtpan180.Text = unidades - 1
        calcular_total()
        guardar_temporal()
    End Sub
    Private Sub btnrestarbollo120_Click(sender As Object, e As EventArgs) Handles btnrestarbollo120.Click
        Dim unidades As Integer
        Int32.TryParse(txtbollo120.Text, unidades)
        txtbollo120.Text = unidades - 1
        calcular_total()
        guardar_temporal()
    End Sub
    Private Sub btnrestarbollo150_Click(sender As Object, e As EventArgs) Handles btnrestarbollo150.Click
        Dim unidades As Integer
        Int32.TryParse(txtbollo150.Text, unidades)
        txtbollo150.Text = unidades - 1
        calcular_total()
        guardar_temporal()
    End Sub
    Private Sub btnrestarbollo170_Click(sender As Object, e As EventArgs) Handles btnrestarbollo170.Click
        Dim unidades As Integer
        Int32.TryParse(txtbollo170.Text, unidades)
        txtbollo170.Text = unidades - 1
        calcular_total()
        guardar_temporal()
    End Sub
    Private Sub btnrestarbollo2_Click(sender As Object, e As EventArgs) Handles btnrestarbollo2.Click
        Dim unidades As Integer
        Int32.TryParse(txtbollo2.Text, unidades)
        txtbollo2.Text = unidades - 1
        calcular_total()
        guardar_temporal()
    End Sub
    Private Sub btnrestarbollo250_Click(sender As Object, e As EventArgs) Handles btnrestarbollo250.Click
        Dim unidades As Integer
        Int32.TryParse(txtbollo220.Text, unidades)
        txtbollo220.Text = unidades - 1
        calcular_total()
        guardar_temporal()
    End Sub
    Private Sub btnrestarpatata_Click(sender As Object, e As EventArgs) Handles btnrestarpatata.Click
        Dim unidades As Integer
        Int32.TryParse(txtpatata.Text, unidades)
        txtpatata.Text = unidades - 1
        calcular_total()
        guardar_temporal()
    End Sub
    Private Sub btnrestarfrutos_Click(sender As Object, e As EventArgs) Handles btnrestarfrutos.Click
        Dim unidades As Integer
        Int32.TryParse(txtfrutos.Text, unidades)
        txtfrutos.Text = unidades - 1
        calcular_total()
        guardar_temporal()
    End Sub
    Private Sub btnrestarrefres_Click(sender As Object, e As EventArgs) Handles btnrestarrefre.Click
        Dim unidades As Integer
        Int32.TryParse(txtrefresco.Text, unidades)
        txtrefresco.Text = unidades - 1
        calcular_total()
        guardar_temporal()
    End Sub
    Private Sub btnrestarcerve_Click(sender As Object, e As EventArgs) Handles btnrestarcerve.Click
        Dim unidades As Integer
        Int32.TryParse(txtcerveza.Text, unidades)
        txtcerveza.Text = unidades - 1
        calcular_total()
        guardar_temporal()
    End Sub
    Private Sub btnrestarlitro_Click(sender As Object, e As EventArgs) Handles btnrestarlitro.Click
        Dim unidades As Integer
        Int32.TryParse(txtlitro.Text, unidades)
        txtlitro.Text = unidades - 1
        calcular_total()
        guardar_temporal()
    End Sub
End Class
