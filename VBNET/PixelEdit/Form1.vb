Imports TatukGIS_XDK11

Public Class Form1

    Dim GisUtils As New TGIS_Utils

    Private Sub btnProfile_Click(sender As Object, e As EventArgs) Handles btnProfile.Click
        Dim lp As TGIS_LayerPixel
        Dim lv As TGIS_LayerVector
        Dim shp As TGIS_Shape

        Memo.Clear()

        GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\Samples\PixelEdit\grid.ttkproject")

        lp = CType(GIS.Get("elevation"), TGIS_LayerPixel)
        lv = CType(GIS.Get("line"), TGIS_LayerVector)
        shp = lv.GetShape(1).MakeEditable()
        shp.IsSelected = True

        For Each px As TGIS_PixelItem In lp.Loop(0, shp, False)
            Memo.AppendText(String.Format("Distance: {0}, Height:{1}", px.Distance, px.Value) & Chr(10))
        Next px

    End Sub

    Private Sub btnMinMax_Click(sender As Object, e As EventArgs) Handles btnMinMax.Click
        Dim lp As TGIS_LayerPixel
        Dim lv As TGIS_LayerVector
        Dim ltmp As TGIS_LayerVector
        Dim shp As TGIS_Shape
        Dim shptmp As TGIS_Shape
        Dim dmin, dmax As Double
        Dim ptmin, ptmax As TGIS_Point

        Memo.Clear()

        GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\Samples\PixelEdit\grid.ttkproject")

        lp = CType(GIS.Get("elevation"), TGIS_LayerPixel)
        lv = CType(GIS.Get("polygon"), TGIS_LayerVector)
        shp = lv.GetShape(1).MakeEditable()
        shp.IsSelected = True

        dmax = -1.0E+38
        dmin = 1.0E+38
        ptmin = GisUtils.GisPoint(0, 0)
        ptmax = GisUtils.GisPoint(0, 0)

        For Each px As TGIS_PixelItem In lp.Loop_4(shp.Extent, 0, shp, "T", False)
            If px.Value < dmin Then
                dmin = px.Value
                ptmin = px.Center
            End If

            If px.Value > dmax Then
                dmax = px.Value
                ptmax = px.Center
            End If
        Next px

        ltmp = New TGIS_LayerVector()
        ltmp.CS = lp.CS
        GIS.Add(ltmp)

        ltmp.Params.Marker.Style = TGIS_MarkerStyle.Cross
        ltmp.Params.Marker.Size = -10
        ltmp.Params.Marker.Color = (New TGIS_Color).Black

        shptmp = ltmp.CreateShape(TGIS_ShapeType.Point)
        shptmp.AddPart()
        shptmp.AddPoint(ptmin)

        shptmp = ltmp.CreateShape(TGIS_ShapeType.Point)
        shptmp.AddPart()
        shptmp.AddPoint(ptmax)

        GIS.InvalidateWholeMap()

        Memo.AppendText(String.Format("Min: {0}, Location: {1} {2}", dmin, ptmin.X, ptmin.Y) & Chr(10))
        Memo.AppendText(String.Format("Max: {0}, Location: {1} {2}", dmax, ptmax.X, ptmax.Y) & Chr(10))
    End Sub

    Private Sub btnAverageColor_Click(sender As Object, e As EventArgs) Handles btnAverageColor.Click
        Dim lp As TGIS_LayerPixel
        Dim lv As TGIS_LayerVector
        Dim shp As TGIS_Shape
        Dim r, g, b As Double
        Dim rr, gg, bb As Byte
        Dim cnt As Integer
        Dim cl As TGIS_Color

        Memo.Clear()

        GIS.Open(GisUtils.GisSamplesDataDirDownload() & "\Samples\PixelEdit\bitmap.ttkproject")

        lp = CType(GIS.Get("bluemarble"), TGIS_LayerPixel)
        lv = CType(GIS.Get("countries"), TGIS_LayerVector)

        shp = lv.GetShape(679).MakeEditable() ' Spain
        GIS.Lock()
        GIS.VisibleExtent = shp.ProjectedExtent
        GIS.Zoom = GIS.Zoom / 2.0
        GIS.Unlock()

        cnt = 0
        r = 0
        g = 0
        b = 0

        For Each px As TGIS_PixelItem In lp.Loop_4(shp.Extent, 0, shp, "T", False)
            r = r + px.Color.R
            g = g + px.Color.G
            b = b + px.Color.B
            cnt = cnt + 1
        Next px

        If cnt > 0 Then
            rr = Convert.ToByte(Math.Truncate(r / cnt))
            gg = Convert.ToByte(Math.Truncate(g / cnt))
            bb = Convert.ToByte(Math.Truncate(b / cnt))
            cl = (New TGIS_Color).FromRGB(rr, gg, bb)
            For Each px As TGIS_PixelItem In lp.Loop_4(shp.Extent, 0, shp, "T", True)
                px.Color = cl
            Next px
        End If

        GIS.InvalidateWholeMap()
    End Sub

    Private Sub btnCreateBitmap_Click(sender As Object, e As EventArgs) Handles btnCreateBitmap.Click
        Dim lp As TGIS_LayerJPG
        Dim lck As TGIS_LayerPixelLock
        Dim x, y As Integer
        Dim c As New TGIS_Color
        Dim idx As Integer

        Memo.Clear()

        lp = New TGIS_LayerJPG()
        Try
            lp.Build_3("test.jpg", (New TGIS_CSFactory).ByEPSG(4326),
               GisUtils.GisExtent(-180, -90, 180, 90), 360, 180)

            ' direct access to pixels
            lck = lp.LockPixels(GisUtils.GisExtent(-45, -45, 45, 45), lp.CS, True)
            Try
                For x = lck.Bounds.Left To lck.Bounds.Left + lck.Bounds.Width
                    For y = lck.Bounds.Top To lck.Bounds.Top + lck.Bounds.Height
                        idx = lck.BitmapPos(x, y)
                        lck.Bitmap.Value(idx) = BitConverter.ToInt32(BitConverter.GetBytes(c.Red.ARGB), 0)
                    Next
                Next
            Finally
                lp.UnlockPixels(lck)
            End Try

            lp.SaveData()
        Finally
            lp = Nothing
        End Try

        GIS.Open("test.jpg")
    End Sub

    Private Sub btnCreateGrid_Click(sender As Object, e As EventArgs) Handles btnCreateGrid.Click
        Dim lp As TGIS_LayerGRD
        Dim lck As TGIS_LayerPixelLock
        Dim x, y As Integer
        Dim Rnd As Random

        Memo.Clear()

        lp = New TGIS_LayerGRD()
        Try
            lp.Build_3("test.grd", (New TGIS_CSFactory).ByEPSG(4326), GisUtils.GisExtent(-180, -90, 180, 90), 360, 180)

            ' direct access to pixels
            Rnd = New Random()
            lck = lp.LockPixels(GisUtils.GisExtent(-45, -45, 45, 45), lp.CS, True)
            Try
                For x = lck.Bounds.Left To lck.Bounds.Right
                    For y = lck.Bounds.Top To lck.Bounds.Bottom
                        lck.Grid.Value(y, x) = Rnd.Next(100)
                    Next
                Next
            Finally
                lp.UnlockPixels(lck)
            End Try

            lp.SaveData()
        Finally
            lp = Nothing
        End Try

        GIS.Open("test.grd")
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GIS_Legend.GIS_Viewer = GIS.GetOcx()
    End Sub
End Class
