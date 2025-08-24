Imports System.Web.Optimization

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Se desencadena al iniciar la aplicación
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        RegisterRoutes(RouteTable.Routes)
    End Sub

    Private Sub RegisterRoutes(routes As RouteCollection)
        routes.MapPageRoute("ProductosRoute", "productos", "~/productos.aspx")
        routes.MapPageRoute("DefaultRoute", "", "~/Default.aspx")

        ' Ruta para páginas eliminadas o no existen (No eliminar)
        routes.MapPageRoute("NotFound", "{*url}", "~/NotFound.aspx")
    End Sub
End Class