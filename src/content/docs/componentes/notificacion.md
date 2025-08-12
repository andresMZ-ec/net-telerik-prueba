---
title: Notificación
description: Componente personalizado y global de DomusData
---
 Este componente se encarga de mostrar mensajes de notificación al usuario.
 Permite personalizar el tipo de notificación (éxito, error, advertencia, información),
 el contenido del mensaje y la duración de la visualización.

## Introducción

Este componente se encuentra de forma global en la aplicación y esta incluido dentro de la libreria personalizada de DomusData realizadas por los desarrolladores internos.

Su función es emitir un mensaje al usuario en el lado superior/inferior derecho de la pantalla, pero sin la necesidad de colocar repetidamente el componente en cada página hija del `Site.Master`. Si no que a su vez, solo sea consumida por medio del code-behind.

## ¿Cómo usarlo?

Lo único que se requiere es acceder al componente por medio del `Site.Master`. Por lo tanto, si tenemos la página `admin_unidades_medida.aspx` abrimos el code-behind y en la parte inicial del código colocaremos la siguiente línea:

`Private _Notificacion As Notificacion`

Y dentro del método **Page_Load**, agregamos la siguiente línea de código:

`_Notificacion = CType(Master, SiteMaster).Notification`

De esta forma nos aseguramos que una vez renderizado el Site.Master los métodos se cargarán para ser utilizados en cualquier otro método.

### Ejemplo de uso

```
  Inherits Page

  Private _Notificacion As Notificacion
  ...

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
    _Notificacion = CType(Master, SiteMaster).Notification
    ...
  End Sub

  Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
    ...
    _Notificacion.Mostrar(
      "Registro de Unidad de Medida",
      "La unidad de medida ha sido registrada con éxito",
      TipoMensaje.Exito
    )
  End Sub
```

## Propiedades
|Propiedad| Descripción | Tipo de Dato | Valor Predeterminado |
|--|--|--|--|
|**Título**|Título de notificación| `String` o `Nullable`<br> (Opcional) | `Nullable` |
|**Descripción**| Texto del mensaje a mostrar | `String` |  |
|**Tipo**| Tipo de notificación | `Exito` <br> `Error` <br> `Advertencia` <br> `Info` <br> | `Info` |
|**Duración**| Tiempo en milisegundos que la notificación estará visible. | `Integer`| 300 |
